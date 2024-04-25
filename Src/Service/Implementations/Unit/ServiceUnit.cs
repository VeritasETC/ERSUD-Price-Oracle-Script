using AutoMapper;
using Ethereum.Contract.Interfaces;
using EthereumSdk.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger.Interfaces;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Implementations;
using Service.Interfaces.Unit;
using Microsoft.Extensions.Configuration;
using Service.Interfaces.Admin;
using Service.Implementations.Admin;

namespace Service.Implementations.Unit
{
    internal class ServiceUnit : IServiceUnit
    {
        private readonly IRepositoryUnit _repository;
        private readonly IMapper _mapper;
        private readonly IEventLogger _eventLogger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEtherService _ether;
        private readonly IFileManagementService _fileManagement;
        private readonly IConfiguration _configuration;
        private IEmailServices _email;
        private ICurrencyService _currency;
        private IMasterWalletServices _masterWallet;
        private IWalletServices _Wallet;
        private IContractHandler _ContractHandler;
        private IAccountService _Account;
        private ITokenService _token;
        private IRateService _Rate;

        public ServiceUnit(IRepositoryUnit repository, IFileManagementService fileManagementService, IMapper mapper, IEventLogger eventLogger, IHostingEnvironment hostingEnvironment,
            IEtherService ether, IContractHandler ContractHandler, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _eventLogger = eventLogger;
            _hostingEnvironment = hostingEnvironment;
            _ether = ether;
            _fileManagement = fileManagementService;
            _ContractHandler = ContractHandler;
            _configuration = configuration;
        }
        public IEmailServices Email =>
       _email ??= new EmailServices(_hostingEnvironment, _eventLogger);

        public ICurrencyService Currency =>
            _currency ??= new CurrencyService(_repository, _eventLogger, _mapper, _ether, _fileManagement);

        public IMasterWalletServices MasterWallet =>
            _masterWallet ??= new MasterWalletServices(_repository, _eventLogger, _ether, _mapper);

        public IWalletServices Wallet =>
            _Wallet ??= new WalletServices(_repository, _eventLogger, _ether, _ContractHandler);

        public IAccountService Account =>
            _Account ??= new AccountService(_repository, _mapper, _eventLogger, _fileManagement,_configuration, Email);

        public ITokenService Token =>
             _token ??= new TokenService(_repository, _mapper, _eventLogger);

        public IRateService Rate => _Rate ??= new RateService(_repository, _ContractHandler);
    }
}
