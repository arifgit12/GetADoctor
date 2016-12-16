using GetADoctor.Data.Infrastructure;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Repositories
{
    public class WaitingRepository : Repository<Waiting>, IWaitingRepository
    {
        public WaitingRepository(ApplicationDbContext context) : base(context)
        {

        }
    }

    public interface IWaitingRepository : IRepository<Waiting>
    {

    }
}
