namespace MultiplexServices.Interfaces
{
    public interface IModelService<TModel> : IServiceBase
        where TModel : class
    {
        void Add(TModel model);
    }
}
