using ECommerce.Data.IRepositories;
using Newtonsoft.Json;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities;
using ECommerce.Data.Configurations;
using Microsoft.VisualBasic;

namespace ECommerce.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        private string dbFile;
        private long lastId;
        private List<TEntity> entities = new List<TEntity>();
        private long id;

        public Repository()
        {
            if (typeof(TEntity) == typeof(Product))
            {
                dbFile = Constants.PRODUCT_PATH;
            }
            else if (typeof(TEntity) == typeof(User))
            {
                dbFile = Constants.USER_PATH;
            }
            else if (typeof(TEntity) == typeof(Payment))
            {
                dbFile = Constants.PAYMENT_PATH;
            }
            else if (typeof(TEntity) == typeof(Order))
            {
                dbFile = Constants.ORDER_PATH;
            }
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            // in this place : you missed give list information as a entities variable
            var entities = await SelectAllAsync();
            if (entities.Count == 0)
            {
                entity.Id = 1;
            }
            else
            {
                entity.Id = entities[entities.Count - 1].Id + 1;
            }

            entities.Add(entity);
            string jsonEdition = JsonConvert.SerializeObject(entities);
            File.WriteAllText(dbFile, jsonEdition);

            return entity;
        }

        public async Task<bool> DeleteAsync(Predicate<TEntity> predicate)
        {
            TEntity entity = await SelectAsync(x => x.Id == id);

            if (entity is null)
            {
                return false;
            }

            entities.Remove(entity);

            string jsonEdition = JsonConvert.SerializeObject(entities);
            File.WriteAllText(dbFile, jsonEdition);

            return true;
        }

        public async Task<List<TEntity>> SelectAllAsync(Predicate<TEntity> predicate = null)
        {
            string content = await File.ReadAllTextAsync(dbFile);
            if (content.Length == 0)
            {
                await File.WriteAllTextAsync(dbFile, "[]");
                content = "[]";
            }

            entities = JsonConvert.DeserializeObject<List<TEntity>>(content);

            return entities;
        }

        public async Task<TEntity> SelectAsync(Predicate<TEntity> predicate)
        {
            // in this place too, you forgot to intialize new "entities" variable. Ok
            var entities = await SelectAllAsync();
            // And last "Find" method don't return null . But "FirstOrDefault" null return. Ok
            return entities.FirstOrDefault(x => predicate(x));
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            TEntity objectToUpdate = await SelectAsync(x => x.Id == entity.Id);

            objectToUpdate.UpdatedAt = DateTime.UtcNow;
            int index = entities.IndexOf(objectToUpdate);
            entities.RemoveAt(index);
            entities.Insert(index, entity);

            string jsonEdition = JsonConvert.SerializeObject(entities);
            File.WriteAllText(dbFile, jsonEdition);

            return entity;
        }
    }
}
