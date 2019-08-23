using MultiplexData.Models;
using System.Collections.Generic;

namespace MultiplexData
{
    public interface IMultiplexRun
    {
        IEnumerable<Run> GetAll();
        Run GetById(int id);
        void Add(Run run);
    }
}
