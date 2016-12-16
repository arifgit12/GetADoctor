using GetADoctor.Data.Infrastructure;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Repositories
{
    public interface ICityRepository : IRepository<City>
    { 

    }
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
