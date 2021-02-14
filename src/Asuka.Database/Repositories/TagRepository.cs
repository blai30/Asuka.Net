using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Asuka.Database.Models;
using Dommel;

namespace Asuka.Database.Repositories
{
    internal class TagRepository : RepositoryBase, ITagRepository
    {
        public TagRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<Tag> GetAsync(int id)
        {
            var result = await Connection.GetAsync<Tag>(id, Transaction);
            return result;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var result = await Connection.GetAllAsync<Tag>(Transaction);
            return result;
        }

        public async Task<object> AddAsync(Tag entity)
        {
            var id = await Connection.InsertAsync(entity, Transaction);
            return id;
        }

        public async Task<int> UpdateAsync(Tag entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteAsync(Tag entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Tag> GetAsync(string tagName)
        {
            var result = await Connection.FirstOrDefaultAsync<Tag>(e => e.Name == tagName, Transaction);
            return result;
        }
    }
}
