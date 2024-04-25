using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IBlockchainRepository : IRepositoryBase<Blockchain>
    {
        Task<Blockchain> GetByIdAsync(long id);
        Task<Blockchain> GetByUuidAsync(Guid uuid);
        Task<Blockchain> GetBlockchainByChainIdAsync(int chainId);
        Task<Blockchain> GetByChainIdAsync(long chainId);
        Task<Blockchain> GetByNameAsync(string name);
        Task<bool> AnyBlockchainByChainIdAsync(int chainId);
        List<Blockchain> GetAllBlockChainsList();
        Blockchain GetById(long id);
        Task<Blockchain> GetByNameAndChainId(long Chainid, string Name);
    }
}
