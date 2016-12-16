using GetADoctor.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Services
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly LocationRepository _addressRepository;
    }
}
