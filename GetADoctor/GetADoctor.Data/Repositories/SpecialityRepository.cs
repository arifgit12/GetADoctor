using GetADoctor.Data.Infrastructure;
using GetADoctor.Models;
using System;

namespace GetADoctor.Data.Repositories
{
    public class SpecialityRepository : Repository<Speciality>, ISpecialityRepository
    {
        public SpecialityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public interface ISpecialityRepository : IRepository<Speciality>
    {

    }
}
