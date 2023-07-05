using Cost.IRepository;
using Cost.Repository.DBContext;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cost.Repository
{
    public  class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IMongoDBContext _mongoContext;
        private readonly IMongoCollection<TEntity> _dbCollection;

        protected BaseRepository(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<TEntity> Get(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            var result = await _dbCollection.FindAsync(filter);
            return result.FirstOrDefault();
        }


        public async Task<IEnumerable<TEntity>> Get()
        {
            var allCollection = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await allCollection.ToListAsync();
        }

        public async Task Create(TEntity obj)
        {
            if (obj == null)
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            
            await _dbCollection.InsertOneAsync(obj);
        }

        public virtual void Update(string id, TEntity obj)
        {
            var objectId = new ObjectId(id);
            _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId), obj);
        }

        public void Delete(string id)
        {

            var objectId = new ObjectId(id);
            _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));
        }
    }
}
