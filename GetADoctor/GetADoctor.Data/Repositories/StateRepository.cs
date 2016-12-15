using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GetADoctor.Data.Repositories
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(DbContext context) : base(context)
        {

        }
    }

    public interface IStateRepository : IRepository<State>
    {

    }
}
