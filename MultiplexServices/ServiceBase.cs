using MultiplexData;
using MultiplexServices.Interfaces;

namespace MultiplexServices
{
    public abstract class ServiceBase<TModel, TEntity> : IModelService<TModel>
        where TEntity:class
        where TModel:class
    {
        public MultiplexDbContext DbContext { get; set; }

        public ServiceBase(MultiplexDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public abstract TEntity FromModel(TModel model);


        public void Add(TModel model)
        {
            var entity = FromModel(model);

            DbContext.Add(entity);
            DbContext.SaveChanges();
        }

        public void Update(TModel model)
        {
            var entity = FromModel(model);

            DbContext.Update(entity);
            DbContext.SaveChanges();
        }

        public void Delete(TModel model)
        {
            var entity = FromModel(model);

            DbContext.Remove(entity);
            DbContext.SaveChanges();
        }

    }
}
