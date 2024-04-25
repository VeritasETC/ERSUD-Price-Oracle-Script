using AutoMapper;
using EthereumSdk.Interfaces;
using DTO.Models;
//using BridgeRlc.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ViewModel.MasterWallet;
using DTO.Enums;
using Logger.Interfaces;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Models;

namespace Service.Implementations
{
    public class MasterWalletServices : IMasterWalletServices
    {
        private readonly IRepositoryUnit _repository;
        private readonly IEventLogger _eventLogger;
        private readonly IEtherService _ether;
        private readonly IMapper mapper;

        public MasterWalletServices(IRepositoryUnit repository, IEventLogger eventLogger, IEtherService ether, IMapper mapper)
        {
            _repository = repository;
            _eventLogger = eventLogger;
            _ether = ether;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<MasterWalletResponseModel>> AddMasterWallet(MasterWalletRequestModel model)
        {
            var wallet = new MasterWallet()
            {
                Address = model.Address,
                PendingBalance = 0,
                Balance = 0,
                Name = model.Name,
                PrivateKey = model.PrivateKey,
                PublicKey = model.PublicKey,
                BlockchainType = EBlockchainType.Tron
            };

            _repository.MasterWallet.Create(wallet);
            await _repository.SaveAsync();

            return ServiceResults.GetSuccessfully(mapper.Map<MasterWalletResponseModel>(wallet));
        }
        public async Task<ServiceResult<MasterWalletResponseModel>> AddMasterWalletEth(MasterWalletRequestModel model)
        {

            var wallet1 = _ether.CreateWallet();
            var wallet = new MasterWallet()
            {
                Address = wallet1.Address,
                PendingBalance = model.PendingBalance,
                Balance = model.Balance,
                Name = model.Name,
                PrivateKey = wallet1.PrivateKey,
                PublicKey = wallet1.PublicKey,
                BlockchainType = EBlockchainType.BinanceSmartChain,
            };

            _repository.MasterWallet.Create(wallet);
            await _repository.SaveAsync();

            return ServiceResults.GetSuccessfully(mapper.Map<MasterWalletResponseModel>(wallet));
        }
        public async Task<ServiceResult<List<MasterWalletResponseModel>>> GetMasterWallets()
        {
            var wallets = _repository.MasterWallet.GetAllMasterWallets();
            if (wallets.Count == 0)
            {
                return ServiceResults.Errors.NotFound<List<MasterWalletResponseModel>>("Wallets", null);
            }
            return ServiceResults.GetSuccessfully(mapper.Map<List<MasterWalletResponseModel>>(wallets));
        }
        public async Task<ServiceResult<MasterWalletResponseModel>> GetMasterWalletbyUuid(Guid uuid)
        {
            var wallet = await _repository.MasterWallet.GetByUuidAsync(uuid);
            if (wallet == null)
            {
                return ServiceResults.Errors.NotFound<MasterWalletResponseModel>("Wallet", null);
            }
            return ServiceResults.GetSuccessfully(mapper.Map<MasterWalletResponseModel>(wallet));
        }
        public async Task<ServiceResult<List<MasterWalletResponseModel>>> GetMasterWalletbyCurrencyId(long id)
        {
            var wallets = _repository.MasterWallet.GetMasterWalletsByCurrencyId(id);
            if (wallets.Count == 0)
            {
                return ServiceResults.Errors.NotFound<List<MasterWalletResponseModel>>("Wallets", null);
            }
            return ServiceResults.GetSuccessfully(mapper.Map<List<MasterWalletResponseModel>>(wallets));
        }
        public async Task<ServiceResult<MasterWalletResponseModel>> GetMasterWalletByAddressAndCurrencyId(long id, string address)
        {
            var wallet = _repository.MasterWallet.GetMasterWalletByCurrencyId(id, address);
            if (wallet == null)
            {
                return ServiceResults.Errors.NotFound<MasterWalletResponseModel>("Wallet", null);
            }
            return ServiceResults.GetSuccessfully(mapper.Map<MasterWalletResponseModel>(wallet));
        }
        public async Task<ServiceResult<MasterWalletResponseModel>> GetMasterWalletByprivateKey(string key)
        {
            var wallet = await _repository.MasterWallet.GetMasterWalletByPrivatekey(key);
            if (wallet == null)
            {
                return ServiceResults.Errors.NotFound<MasterWalletResponseModel>("Wallet", null);
            }
            return ServiceResults.GetSuccessfully(mapper.Map<MasterWalletResponseModel>(wallet));
        }
        public async Task<ServiceResult<MasterWalletResponseModel>> GetMasterWalletByPublicKey(string key)
        {
            var wallet = await _repository.MasterWallet.GetMasterWalletByPublicKey(key);
            if (wallet == null)
            {
                return ServiceResults.Errors.NotFound<MasterWalletResponseModel>("Wallet", null);
            }
            return ServiceResults.GetSuccessfully(mapper.Map<MasterWalletResponseModel>(wallet));
        }
        public async Task<ServiceResult<MasterWalletResponseModel>> EditMasterWallet(Guid uuid, MasterWalletRequestModel model)
        {
            var wallets = await _repository.MasterWallet.GetByUuidAsync(uuid);

            return ServiceResults.GetSuccessfully(mapper.Map<MasterWalletResponseModel>(wallets));
        }
    }
}