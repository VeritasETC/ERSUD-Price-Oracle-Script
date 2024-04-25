using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Models;
using Context;
using Repository.Implementations.Base;
using Repository.Interfaces;

namespace Repository.Implementations
{
    internal class BlockchainRepository : RepositoryBase<Blockchain>, IBlockchainRepository
    {
        private readonly ExchnageContext _db;

        public BlockchainRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<bool> AnyBlockchainByChainIdAsync(int chainId)
        {
            return await FindByCondition(f => f.ChainID == chainId).AnyAsync();
        }
        public async Task<Blockchain> GetBlockchainByChainIdAsync(int chainId)
        {
            return await FindByCondition(f => f.ChainID == chainId).FirstOrDefaultAsync();
        }
        public async Task<Blockchain> GetByChainIdAsync(long chainId)
        {
            return await FindByCondition(f => f.ChainID == chainId).FirstOrDefaultAsync();
        }
        public async Task<Blockchain> GetByNameAsync(string name)
        {
            return await FindByCondition(f => f.Name == name && f.IsDeleted == false).FirstOrDefaultAsync();
        }
        public async Task<Blockchain> GetByIdAsync(long id)
        {
            return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Blockchain> GetByUuidAsync(Guid uuid)
        {
            return await FindByCondition(f => f.Uuid == uuid).FirstOrDefaultAsync();
        }

        public List<Blockchain> GetAllBlockChainsList()
        {
            return FindAll().ToList();
        }
        public Blockchain GetById(long id)
        {
            return FindByCondition(x => x.Id == id).FirstOrDefault();
        }
        public async Task<Blockchain> GetByNameAndChainId(long Chainid, string Name)
        {
            return await FindByCondition(x => x.Name == Name && x.ChainID == Chainid).FirstOrDefaultAsync();
        }
    }
}
