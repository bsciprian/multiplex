using MultiplexData;

namespace MultiplexServices.Interfaces
{
    public interface IServiceBase
    {
        MultiplexDbContext DbContext { get; set; }
    }


}
