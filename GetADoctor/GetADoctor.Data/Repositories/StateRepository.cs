using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GetADoctor.Data.Infrastructure;

namespace GetADoctor.Data.Repositories
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext context) : base(context)
        {

        }
    }

    public interface IStateRepository : IRepository<State>
    {

    }
}
