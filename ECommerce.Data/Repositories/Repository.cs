using ECommerce.Data.IRepositories;
using Newtonsoft.Json;

namespace ECommerce.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        private string? dbFile;
        private long lastId;
        private List<TEntity> entities = new List<TEntity>();

        public Repository()
        {
            if (typeof(TEntity) == typeof(int))
            {
            }
            else if (typeof(TEntity) == typeof(int))
            {

            }
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await SelectAllAsync();
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
            TEntity food = await SelectAsync(x => x.Id == id);

            if (food is null)
            {
                return false;
            }

            entities.Remove(food);

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
            await SelectAllAsync();
            return entities.Find(x => predicate(x));
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            TEntity objectToUpdate = await SelectAsync(x => x.Id == entity.Id);

            objectToUpdate.LastUpdatedAt = DateTime.UtcNow;
            int index = entities.IndexOf(objectToUpdate);
            entities.RemoveAt(index);
            entities.Insert(index, entity);

            string jsonEdition = JsonConvert.SerializeObject(entities);
            File.WriteAllText(dbFile, jsonEdition);

            return entity;
        }
    }
}
