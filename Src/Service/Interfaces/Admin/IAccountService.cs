using Common.Helpers;
using DTO.ViewModel.Account;
using DTO.ViewModel.Admin;
using DTO.ViewModel.Admin.Request;
using DTO.ViewModel.Admin.Role;
using DTO.ViewModel.Admin.Screen;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Admin
{
    public interface IAccountService
    {
        #region Account
        Task<ServiceResult<bool>> AddSuperAdmin(CreateUserRequest createUser);
        Task<ServiceResult<bool>> SaveUser(CreateUserRequest createUser, long accountId);
        Task<ServiceResult<AccountLoginResponse>> AdminLogin(AccountLoginRequest request);
        Task<ServiceResult<string>> SignUp(AccountSignUpRequestModel model);
        #endregion


        #region Screen

        Task<ServiceResult<bool>> AddScreen(AddScreenRequest request);
        Task<ServiceResult<List<ScreenResponseModel>>> GetAllScreens();
        #endregion

        #region Role

        Task<ServiceResult<bool>> AddUpdateRole(AddRoleRequest request, long accountId);
        Task<ServiceResult<List<General<long>>>> GetAllRoles();
        Task<ServiceResult<bool>> RoleActivation(RoleActivateRequest request);
        #endregion

        #region RoleRights
        Task<ServiceResult<AccountLoginResponse>> GetAssignRights(long roleId);
        Task<ServiceResult<bool>> AssignRights(AssignRight assignRight, long accountId);
        #endregion

        #region Account Settings
        Task<ServiceResult<bool>> ChangePassword(AccountChangePasswordRequest changePassword);
        Task<ServiceResult<string>> SendForgotEmail(ForgotPasswordRequestModel model);
        Task<ServiceResult<string>> ChangePasswordByToken(ChangeForgotPasswordRequestModel model);
        Task<ServiceResult<string>> VerifyEmailToken(string token);
        Task<ServiceResult<AccountProfileViewModel>> GetUserProfileAsync(long accountId);
        Task<ServiceResult<bool>> DeleteUser(long loginId, long deleteUserId);
        Task<ServiceResult<AccountProfileViewModel>> UpdateAccountAsync(long accountId, UpdateAccountRequestModel model);
        #endregion

        #region GetUserList
        Task<ServiceListResult<List<UserResponseModel>>> GetAllUsers(long accountId, PaginationModel page);
        #endregion

      
    }
}
