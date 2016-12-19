using GetADoctor.Data.Infrastructure;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private ApplicationDbContext _databaseContext;
        public DoctorRepository(ApplicationDbContext context)
            : base(context)
        {
            this._databaseContext = context;
        }

        public Doctor DGetByUser(string id)
        {
            return _databaseContext.Doctors.FirstOrDefault(u => u.UserId == id);
        }
    }

    public interface IDoctorRepository : IRepository<Doctor>
    {
        Doctor DGetByUser(string id);
    }
}
