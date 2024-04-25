using AutoMapper;
using Common.Helpers;
using DTO.Enums;
using DTO.Models;
using DTO.ViewModel.Account;
using DTO.ViewModel.Admin;
using DTO.ViewModel.Admin.Request;
using DTO.ViewModel.Admin.Role;
using DTO.ViewModel.Admin.Screen;
using Logger.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Interfaces.Admin;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Implementations.Admin
{
    public class AccountService : IAccountService
    {
        private IRepositoryUnit _repository;
        private IMapper _mapper;
        private IEventLogger _eventLogger;
        private IFileManagementService _fileManagement;
        private IConfiguration _configuration;
        private IEmailServices _email;

        public AccountService(IRepositoryUnit repository, IMapper mapper, IEventLogger eventLogger, IFileManagementService fileManagement, IConfiguration configuration, IEmailServices email)
        {
            _repository = repository;
            _mapper = mapper;
            _mapper = mapper;
            _eventLogger = eventLogger;
            _fileManagement = fileManagement;
            _configuration = configuration;
            _email = email;
        }



        #region AdminAccount
        public async Task<ServiceResult<bool>> AddSuperAdmin(CreateUserRequest createUser)
        {
            try
            {
                bool response = false;
                var encryptedPassword = new Encryption().Encrypt(createUser.Password, _configuration.GetValue<string>("EncryptionKey"));
                var account = await _repository.Account.GetAccountByEmail(createUser.Email);
                if (account != null) return ServiceResults.Errors.AlreadyExist("Email", false);

                string accountProfile = "";
                if (createUser.ProfileImage != null)
                {
                    var file = await _fileManagement.UploadProductImageFile(createUser.ProfileImage, "AdminAccount", new string[] { "image/png", "image/jpeg", "image/jpg" });
                    if (file.isSuccess)
                    {
                        accountProfile = file.response;
                    }
                }
                try
                {
                    var role = new Role();
                    var getRoleByName = await _repository.Role.GetRoleByNameAsync(AppSettingHelper.SuperAdmin);
                    if (getRoleByName == null)
                    {
                        //add role
                        role = new Role()
                        {
                            Name = AppSettingHelper.SuperAdmin,
                            IsEnabled = true,
                            CreatedAt = DateTime.UtcNow,
                            IsDeleted = false,
                        };
                        _repository.Role.Create(role);
                    }
                    else
                        role = getRoleByName;


                    var newAccount = new Account()
                    {
                        FirstName = createUser.FirstName,
                        LastName = createUser.LastName,
                        IsDeleted = false,
                        AccountStatus = AccountStatusEnum.Active,
                        Email = createUser.Email,
                        Password = encryptedPassword,
                        IsBlocked = false,
                        AccountType = createUser.AccountType,
                        IsEmailVerfied = true,
                        IsVerfiedAccount = true,
                        ProfileImage = accountProfile,
                        CreatedAt = DateTime.UtcNow,
                    };
                    newAccount.AccountRoles.Add(new AccountRole()
                    {
                        RoleId = role.Id,
                        IsEnabled = true,
                        CreatedAt = DateTime.UtcNow,
                    });
                    _repository.Account.Create(newAccount);
                    var screenlist = await _repository.Screen.GetScreenListAsync();
                    List<RoleRights> roleRightsList = new List<RoleRights>();
                    foreach (var item2 in screenlist)
                    {
                        roleRightsList.Add(new RoleRights()
                        {
                            RoleId = role.Id,
                            IsEnabled = true,
                            CreatedAt = DateTime.UtcNow,
                            ScreenId = item2.Id,
                            IsView = true,
                            IsAdd = true,
                            IsUpdate = true,
                            IsDelete = true,
                        });

                    }
                    _repository.RoleRights.AddRange(roleRightsList);
                    await _repository.SaveAsync();
                    response = true;
                }
                catch (Exception ex)
                {
                    return ServiceResults.Errors.UnhandledError(ex.Message, false);
                }


                return ServiceResults.AddedSuccessfully(response);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, false);
            }
        }
        public async Task<ServiceResult<AccountLoginResponse>> AdminLogin(AccountLoginRequest request)
        {
            try
            {
                string encryptionKey = _configuration.GetValue<string>("EncryptionKey");
                var encryptedPassword = new Encryption().Encrypt(request.Password, encryptionKey);
                var response = new AccountLoginResponse();
                var user = await _repository.Account.GetAccountByEmailAndPasswordAsync(request.UserInfo, encryptedPassword);
                if (user == null) return ServiceResults.Errors.NotFound<AccountLoginResponse>("Incorrect username or password", null);
                if (user.AccountRoles.Where(x => !x.IsDeleted && x.IsEnabled).FirstOrDefault() == null)
                    return ServiceResults.Errors.NotFound<AccountLoginResponse>("you have no role", null);

                var Rolerights = await _repository.RoleRights.GetRoleRightsByRoleId(user.AccountRoles.Where(x => !x.IsDeleted && x.IsEnabled).FirstOrDefault().RoleId);

                if (user.IsBlocked) return ServiceResults.Errors.Blocked<AccountLoginResponse>("Account is Block", null);
                if (!user.IsEmailVerfied) return ServiceResults.Errors.Blocked<AccountLoginResponse>("Email not verified", null);

                var authToken = new SystemGlobal().GenerateTokenApp(user.Id.ToString(), (int)user.AccountType);


                var viewModel = _mapper.Map<AccountViewModel>(user);
                var rightsList = _mapper.Map<List<RoleRightsModel>>(Rolerights);

                response = new AccountLoginResponse
                {
                    AccessToken = authToken,
                    UserInfo = viewModel,
                    Rights = rightsList,
                };
                var authTokenObj = new AuthToken()
                {
                    AccountId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    ExpiresAt = DateTime.UtcNow.AddMonths(1),
                    AccountType = user.AccountType,
                    IsLogout = false,
                    IssuedAt = DateTime.UtcNow,
                    Token = authToken,
                };
                _repository.AuthToken.Create(authTokenObj);
                await _repository.SaveAsync();

                return ServiceResults.GetSuccessfully(response);

            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError<AccountLoginResponse>(ex.Message, null);
            }
        }
        public async Task<ServiceResult<bool>> SaveUser(CreateUserRequest createUser, long accountId)
        {
            try
            {
                bool response = false;
                if (createUser.Id == 0)
                {
                    var encryptedPassword = new Encryption().Encrypt(createUser.Password, _configuration.GetValue<string>("EncryptionKey"));
                    var account = await _repository.Account.GetAccountByEmail(createUser.Email);
                    if (account != null) return ServiceResults.Errors.AlreadyExist("Email", false);
                    string accountProfile = "";
                    if (createUser.ProfileImage != null)
                    {
                        var file = await _fileManagement.UploadProductImageFile(createUser.ProfileImage, "AdminAccount", new string[] { "image/png", "image/jpeg", "image/jpg" });
                        if (file.isSuccess)
                        {
                            accountProfile = file.response;
                        }
                    }
                    try
                    {
                        var roleCheck = await _repository.Role.GetRoleById(createUser.RoleId);
                        if (roleCheck == null) return ServiceResults.Errors.NotFound("Role", false);
                        var newAccount = new Account()
                        {
                            FirstName = createUser.FirstName,
                            LastName = createUser.LastName,
                            IsDeleted = false,
                            AccountStatus = AccountStatusEnum.Active,
                            Email = createUser.Email,
                            Password = encryptedPassword,
                            IsBlocked = false,
                            AccountType = createUser.AccountType,
                            IsEmailVerfied = true,
                            IsVerfiedAccount = true,
                            ProfileImage = accountProfile,
                            CreatedAt = DateTime.UtcNow,
                        };
                        newAccount.AccountRoles.Add(new AccountRole()
                        {
                            RoleId = createUser.RoleId,
                            IsEnabled = true,
                            CreatedAt = DateTime.UtcNow,
                        });
                        _repository.Account.Create(newAccount);
                        await _repository.SaveAsync();
                        response = true;
                    }
                    catch (Exception ex)
                    {
                        return ServiceResults.Errors.UnhandledError(ex.Message, false);
                    }
                }
                else
                {
                    try
                    {
                        var account = await _repository.Account.GetAccountById(createUser.Id);
                        if (account == null) return ServiceResults.Errors.NotFound("Account", false);

                        var emailAccount = await _repository.Account.GetAccountByEmail(createUser.Email);
                        if (emailAccount != null) return ServiceResults.Errors.AlreadyExist("Email", false);

                        var role = await _repository.Role.GetRoleById(createUser.RoleId);
                        if (role == null) return ServiceResults.Errors.NotFound("Role", false);

                        var roleAdmin = await _repository.AccountRole.GetAllAccountRoleByAccountIdAsync(accountId);
                        roleAdmin.ForEach((x) =>
                        {
                            x.IsEnabled = false;
                            x.IsDeleted = true;
                            x.DeletedAt = DateTime.UtcNow;
                        });

                        _repository.AccountRole.UpdateRange(roleAdmin);
                        await _repository.SaveAsync();

                        string accountProfile = "";
                        if (createUser.ProfileImage != null)
                        {
                            var file = await _fileManagement.UploadProductImageFile(createUser.ProfileImage, "AdminAccount", new string[] { "image/png", "image/jpeg", "image/jpg" });
                            if (file.isSuccess)
                            {
                                accountProfile = file.response;
                            }
                        }
                        var encryptedPassword = new Encryption().Encrypt(createUser.Password, _configuration.GetValue<string>("EncryptionKey"));

                        if (!string.IsNullOrEmpty(createUser.FirstName)) { account.FirstName = createUser.FirstName; }
                        if (!string.IsNullOrEmpty(createUser.LastName)) { account.LastName = createUser.LastName; }
                        if (!string.IsNullOrEmpty(createUser.FirstName)) { account.FirstName = createUser.FirstName; }
                        account.IsDeleted = false;
                        account.AccountStatus = AccountStatusEnum.Active;
                        account.Email = createUser.Email;
                        account.Password = encryptedPassword;
                        account.IsBlocked = account.IsBlocked;
                        account.UpdatedAt = DateTime.UtcNow;
                        account.AccountType = createUser.AccountType;
                        account.IsEmailVerfied = true;
                        account.IsVerfiedAccount = true;
                        account.AccountRoles.Add(new AccountRole()
                        {
                            RoleId = createUser.RoleId,
                            IsEnabled = true,
                            CreatedAt = DateTime.UtcNow,
                        });

                        _repository.Account.Update(account);
                        await _repository.SaveAsync();

                        response = true;
                    }
                    catch (Exception ex)
                    {
                        return ServiceResults.Errors.UnhandledError(ex.Message, false);
                    }
                }

                return ServiceResults.AddedSuccessfully(response);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, false);
            }
        }

        public async Task<ServiceResult<string>> SignUp(AccountSignUpRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                return ServiceResults.Errors.Required<string>("Email", null);

            if (string.IsNullOrWhiteSpace(model.Password))
                return ServiceResults.Errors.Required<string>("Password", null);

            if (!DataValidationHelper.IsValidEmail(model.Email))
                return ServiceResults.Errors.Invalid<string>("Email", null);

            if (_repository.Account.AnyAccountByEmail(model.Email))
                return ServiceResults.Errors.HasEmail<string>(model.Email, null);

            var roleCheck = await _repository.Role.GetRoleByNameAsync(AppSettingHelper.User);
            if (roleCheck == null) return ServiceResults.Errors.NotFound<string>("Role", null);

            var encryptedPassword = new Encryption().Encrypt(model.Password, _configuration.GetValue<string>("EncryptionKey"));
            Account account = new Account()
            {
                NickName = "User#" + Guid.NewGuid().ToString().Replace("-", ""),
                Email = model.Email,
                Password = encryptedPassword,
                AccountType = AccountType.User,
                IsDeleted = false,
                IsEmailVerfied = false,
                IsVerfiedAccount = false,
                TwoFactorAuthEnabled = false,
                RefferalId = Guid.NewGuid().ToString().Replace("-", ""),
                ProfileStatus = EProfileStatus.None,
                CreatedAt = DateTime.UtcNow,

            };
            // Add Email Verification
            var tempAC = new AccountConfirmation()
            {
                Code = Guid.NewGuid().ToString().Replace("-", ""),
                IsUsed = false,
            };
            account.AccountConfirmation.Add(tempAC);

            account.AccountRoles.Add(new AccountRole()
            {
                RoleId = roleCheck.Id,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
            });


            _repository.Account.Create(account);
            await _repository.SaveAsync();


            if (!string.IsNullOrEmpty(model.RefferalId))
            {
                var refferalAcccount = _repository.Account.GetAccountByRefferalId(model.RefferalId);
                if (refferalAcccount != null)
                {
                    _repository.RefferalBonus.Create(new RefferalBonus()
                    {
                        FormAccountId = refferalAcccount.Id,
                        ToAccountId = account.Id,
                        RefferalId = model.RefferalId,
                        IsBonusDeliverd = false,
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false,

                    });

                    await _repository.SaveAsync();
                }


            }
            await _email.SendConfirmEmail(account.Email, account.Email, Token: tempAC.Code);



            return ServiceResults.AddedSuccessfully<string>("Please verify your email");
        }




        #endregion

        #region Screen
        public async Task<ServiceResult<bool>> AddScreen(AddScreenRequest request)
        {
            try
            {
                bool response = false;

                var screenExist = await _repository.Screen.GetScreenByNameAsync(request.ScreenName);
                if (screenExist != null) return ServiceResults.Errors.AlreadyExist("Screen", false);
                _repository.Screen.Create(new Screen()
                {
                    Name = request.ScreenName,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                });
                await _repository.SaveAsync();
                response = true;

                return ServiceResults.GetSuccessfully(response);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, false);
            }
        }


        public async Task<ServiceResult<List<ScreenResponseModel>>> GetAllScreens()
        {
            try
            {
                var screenList = await _repository.Screen.GetScreenListAsync();

                return ServiceResults.GetSuccessfully<List<ScreenResponseModel>>(_mapper.Map<List<ScreenResponseModel>>(screenList));


            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.Invalid<List<ScreenResponseModel>>(ex.Message, null);
            }
        }

        #endregion

        #region Role

        public async Task<ServiceResult<bool>> AddUpdateRole(AddRoleRequest request, long accountId)
        {
            try
            {
                bool response = false;

                var account = await _repository.Account.GetAccountById(accountId);
                if (account == null) return ServiceResults.Errors.NotFound("Account", false);

                var roleExist = await _repository.Role.GetRoleByNameAsync(request.RoleName);
                if (roleExist != null) return ServiceResults.Errors.AlreadyExist("Role", false);
                //add
                if (request.Id == 0)
                {
                    _repository.Role.Create(new Role()
                    {
                        Name = request.RoleName,
                        IsEnabled = true,
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false,

                    });

                }
                //update
                else
                {
                    var roleCheck = await _repository.Role.GetRoleById(request.Id);
                    if (roleCheck == null) return ServiceResults.Errors.NotFound("Role", false);

                    if (!string.IsNullOrEmpty(request.RoleName))
                    {
                        roleCheck.Name = request.RoleName;
                        roleCheck.UpdatedAt = DateTime.UtcNow;
                    }
                    _repository.Role.Update(roleCheck);
                }

                await _repository.SaveAsync();
                response = true;

                return ServiceResults.GetSuccessfully(response);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, false);
            }
        }

        public async Task<ServiceResult<List<General<long>>>> GetAllRoles()
        {
            try
            {

                var roleList = await _repository.Role.GetRoleListAsync();

                var result = roleList.Select(x => new General<long>
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                return ServiceResults.GetSuccessfully<List<General<long>>>(result.OrderByDescending(x => x.Id).ToList());

            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.Invalid<List<General<long>>>(ex.Message, null);
            }
        }

        public async Task<ServiceResult<bool>> RoleActivation(RoleActivateRequest request)
        {
            try
            {
                bool response = false;

                var roleCheck = await _repository.Role.GetRoleById(request.RoleId);
                if (roleCheck != null) return ServiceResults.Errors.NotFound("Role", false);
                roleCheck.IsEnabled = request.IsEnable;
                roleCheck.UpdatedAt = DateTime.UtcNow;
                _repository.Role.Update(roleCheck);
                await _repository.SaveAsync();
                response = true;
                return ServiceResults.GetSuccessfully(response);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, false);
            }
        }
        #endregion

        #region RoleRights

        public async Task<ServiceResult<AccountLoginResponse>> GetAssignRights(long roleId)
        {
            try
            {
                var response = new AccountLoginResponse();
                var rolerights = await _repository.RoleRights.GetRoleRightsByRoleId(roleId);
                response = new AccountLoginResponse
                {
                    AccessToken = null,
                    Rights = rolerights.Select(x => new RoleRightsModel
                    {
                        ScreenId = x.ScreenId,
                        ScreenName = x.Screen.Name,
                        IsView = x.IsView,
                        IsAdd = x.IsAdd,
                        IsUpdate = x.IsUpdate,
                        IsDelete = x.IsDelete,
                        IsEnabled = x.IsEnabled,
                        RoleName = x.Role.Name,
                    }).ToList(),
                };
                return ServiceResults.GetSuccessfully(response);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError<AccountLoginResponse>(ex.Message, null);
            }
        }


        public async Task<ServiceResult<bool>> AssignRights(AssignRight assignRight, long accountId)
        {
            try
            {
                List<RoleRights> rights = new List<RoleRights>();
                bool response = false;
                //need after
                //var role = await _repository.Role.GetRoleById(assignRight.RoleId);
                //if (role == null) return ServiceResults.Errors.Invalid<bool>("role not Exists", false);

                var account = await _repository.Account.GetAccountById(accountId);
                if (account == null) return ServiceResults.Errors.NotFound<bool>("Account", false);

                //Delete Old

                var roleright = await _repository.RoleRights.GetRoleRightsByRoleIdUpdate(assignRight.RoleId);
                if (roleright.Count > 0)
                {
                    roleright.ForEach((x) =>
                    {
                        x.IsEnabled = false;
                        x.DeletedAt = DateTime.UtcNow;
                        x.IsDeleted = true;
                    });
                    _repository.RoleRights.UpdateRange(roleright);
                    await _repository.SaveAsync();
                }

                //add new
                foreach (var item in assignRight.RightList)
                {
                    rights.Add(new RoleRights()
                    {
                        RoleId = assignRight.RoleId,
                        IsEnabled = true,
                        CreatedAt = DateTime.UtcNow,
                        ScreenId = item.ScreenId,
                        IsView = item.IsView == true ? true : false,
                        IsAdd = item.IsAdd == true ? true : false,
                        IsUpdate = item.IsUpdate == true ? true : false,
                        IsDelete = item.IsDelete == true ? true : false,

                    });
                }
                _repository.RoleRights.AddRange(rights);
                await _repository.SaveAsync();

                return ServiceResults.GetSuccessfully(response);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError<bool>(ex.Message, false);
            }
        }
        #endregion

        #region AccountSetting
        public async Task<ServiceResult<bool>> ChangePassword(AccountChangePasswordRequest changePassword)
        {
            try
            {
                string encryptionKey = _configuration.GetValue<string>("EncryptionKey");
                bool response = false;

                var encryptedPassword = new Encryption().Encrypt(changePassword.OldPassword, encryptionKey);

                var user = await _repository.Account.GetAccountByEmailAndPasswordForLoginAsync(changePassword.Email, encryptedPassword);
                if (user == null) return ServiceResults.Errors.NotFound<bool>("Incorrect username or password", false);
                user.Password = new Encryption().Encrypt(changePassword.NewPassword, encryptionKey);
                user.UpdatedAt = DateTime.UtcNow;
                _repository.Account.Update(user);
                response = true;
                return ServiceResults.GetSuccessfully(response);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, false);
            }
        }

        public async Task<ServiceResult<string>> SendForgotEmail(ForgotPasswordRequestModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Email))
                    return ServiceResults.Errors.Required<string>("Email", null);

                if (!DataValidationHelper.IsValidEmail(model.Email))
                    return ServiceResults.Errors.Invalid<string>("Email", null);
                var account = await _repository.Account.GetAccountByEmail(model.Email);
                if (account == null || account.IsDeleted == true || account.IsBlocked)
                    return ServiceResults.Errors.NotFound<string>("User with this email", null);


                var Ac = new AccountConfirmation()
                {
                    Type = AccountConfirmationType.ForgetPassword,
                    AccountId = account.Id,
                    Code = Guid.NewGuid().ToString().Replace("-", ""),
                    IsUsed = false
                };
                _repository.AccountConfirmation.Create(Ac);
                await _repository.SaveAsync();

                _ = _email.SendForgotEmailAsync(account.Email, account.Email, Token: Ac.Code);


                return ServiceResults.AddedSuccessfully<string>("Please check your email for change password link");

            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, "");
            }

        }

        public async Task<ServiceResult<string>> ChangePasswordByToken(ChangeForgotPasswordRequestModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Token))
                    return ServiceResults.Errors.Required<string>("Token", null);
                var accountConfirmation = await _repository.AccountConfirmation.GetAccountConfirmationWithAccountByToken(model.Token);
                if (accountConfirmation == null)
                    return ServiceResults.Errors.Invalid<string>("Activation token", null);

                string encryptionKey = _configuration.GetValue<string>("EncryptionKey");
                accountConfirmation.Account.Password = new Encryption().Encrypt(model.NewPassword, encryptionKey);

                accountConfirmation.IsUsed = true;
                await _repository.AuthToken.DisableAllSessions(accountConfirmation.AccountId ?? 0);
                await _repository.SaveAsync();
                return ServiceResults.UpdatedSuccessfully<string>("Password changed");
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, "");
            }
        }

        public async Task<ServiceResult<string>> VerifyEmailToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    return ServiceResults.Errors.Required<string>("Token", null);
                var accountConfirmation = await _repository.AccountConfirmation.GetAccountConfirmationWithAccountByToken(token);
                if (accountConfirmation == null)
                    return ServiceResults.Errors.Invalid<string>("Activation token", null);

                accountConfirmation.Account.IsEmailVerfied = true;
                accountConfirmation.IsUsed = true;
                await _repository.SaveAsync();
                return ServiceResults.UpdatedSuccessfully<string>("Account is activated");
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, "");
            }
        }

        public async Task<ServiceResult<AccountProfileViewModel>> GetUserProfileAsync(long accountId)
        {
            try
            {
                var account = await _repository.Account.GetAccountById(accountId);

                if (account == null)
                {
                    return ServiceResults.Errors.NotFound<AccountProfileViewModel>("Account", null);
                }

                return ServiceResults.GetSuccessfully<AccountProfileViewModel>(_mapper.Map<AccountProfileViewModel>(account));
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError<AccountProfileViewModel>(ex.Message, null);
            }


        }
        public async Task<ServiceResult<bool>> DeleteUser(long loginId, long deleteUserId)
        {
            try
            {
                bool response = false;
                var account = await _repository.Account.GetAccountById(loginId);
                if (account == null) return ServiceResults.Errors.NotFound<bool>("Account", false);


                var deleteAccount = await _repository.Account.GetAccountByIdWithRole(deleteUserId);
                if (deleteAccount == null) return ServiceResults.Errors.NotFound<bool>("Account", false);


                deleteAccount.AccountRoles.Where(x => !x.IsDeleted).ToList().ForEach((x) =>
                {

                    x.IsEnabled = false;
                    x.DeletedAt = DateTime.Now;
                    x.IsDeleted = true;
                });
                deleteAccount.DeletedAt = DateTime.Now;
                deleteAccount.IsDeleted = true;
                _repository.Account.Update(deleteAccount);
                await _repository.SaveAsync();


                response = true;
                return ServiceResults.UpdatedSuccessfully(response);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError(ex.Message, false);
            }
        }

        public async Task<ServiceResult<AccountProfileViewModel>> UpdateAccountAsync(long accountId, UpdateAccountRequestModel model)
        {
            try
            {

                var account = await _repository.Account.GetAccountById(accountId);
                if (account == null)
                {
                    return ServiceResults.Errors.NotFound<AccountProfileViewModel>("Account", null);
                }
                if (!string.IsNullOrEmpty(model.FirstName)) account.FirstName = model.FirstName;

                if (!string.IsNullOrEmpty(model.FirstName) && !string.IsNullOrEmpty(model.MiddleName) && !string.IsNullOrEmpty(model.LastName)
                    && !string.IsNullOrEmpty(model.Nationality) && model.DateOfBirth != null)
                {
                    account.FirstName = model.FirstName;
                    account.MiddleName = model.MiddleName;
                    account.LastName = model.LastName;
                    account.Nationality = model.Nationality;
                    account.DateOfBirth = model.DateOfBirth;
                    account.ProfileStatus = EProfileStatus.PersonalInformation;

                }
                if (!string.IsNullOrEmpty(model.ResidentialAddress) && !string.IsNullOrEmpty(model.PostalCode) && !string.IsNullOrEmpty(model.City)
                && !string.IsNullOrEmpty(model.Country))
                {
                    account.ResidentialAddress = model.ResidentialAddress;
                    account.PostalCode = model.PostalCode;
                    account.City = model.City;
                    account.Country = model.Country;
                    account.ProfileStatus = EProfileStatus.ResidentialInformation;

                }

                _repository.Account.Update(account);
                await _repository.SaveAsync();


                return ServiceResults.UpdatedSuccessfully<AccountProfileViewModel>(_mapper.Map<AccountProfileViewModel>(account));
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.UnhandledError<AccountProfileViewModel>(ex.Message, null);

            }

        }
        #endregion

        #region UserList
        public async Task<ServiceListResult<List<UserResponseModel>>> GetAllUsers(long accountId, PaginationModel page)
        {
            try
            {

                var userList = _repository.Account.FindAll().Where(x => !x.IsDeleted)
                                                                    .Include(x => x.AccountRoles)
                                                                    .ThenInclude(x => x.Roles)
                                                                    .AsQueryable();

                var loginUser = await _repository.AccountRole.GetAccountRoleByAccountId(accountId);
                var roleName = loginUser.Roles.Name;
                var accountType = loginUser.Account.AccountType;
                if (accountType == AccountType.SuperAdmin)
                    userList = userList;
                else if (accountType == AccountType.SubAdmin)
                    userList = userList.Where(x => x.Id == accountId ||
                                                  (x.AccountType != AccountType.SuperAdmin
                                                   && x.AccountType != AccountType.SubAdmin));

                else if (accountType == AccountType.User)
                    userList = userList.Where(x => x.Id == accountId ||
                                                (x.AccountType != AccountType.SuperAdmin
                                                 && x.AccountType != AccountType.SubAdmin));

                if (!string.IsNullOrEmpty(page.Search))
                {
                    userList = userList.Where(x => (x.FirstName + ' ' + x.LastName).ToLower().Contains(page.Search.ToLower()));
                }
                var total = userList.Count();
                userList = userList.OrderByDescending(x => x.CreatedAt);
                userList = userList.Page(page.CurrentPage.Value, page.PageSize.Value);

                var queryResult = userList.ToList();

                var responses = queryResult.Select(x => new UserResponseModel
                {
                    Id = x.Id,
                    RoleId = x.AccountRoles.FirstOrDefault() != null ? x.AccountRoles.FirstOrDefault().RoleId : 0,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    RoleName = x.AccountRoles.FirstOrDefault().Roles?.Name,
                    ProfilImage = x.ProfileImage,
                    Email = x.Email,
                    AccountStatus = x.AccountStatus.ToString(),
                    Password = new Encryption().Decrypt(x.Password, _configuration.GetValue<string>("EncryptionKey")),
                    Created = x.CreatedAt,
                    IsBlock = x.IsBlocked,
                }).ToList();

                return ServiceResults.GetListSuccessfully<List<UserResponseModel>>(responses, total);
            }
            catch (Exception ex)
            {
                //return ServiceResults.Errors.Invalid<List<UserResponseModel>>(ex.Message, null);
                return null;
            }
        }

        #endregion


    }
}

