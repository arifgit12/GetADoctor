using GetADoctor.Data.Repositories;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Services
{
    public interface ILocationservice
    {
        Location GetAddressByUserId(string userId);
        Location GetAddress(int id);
        int SaveAddress(Location address);
        int UpdateAddress(Location address);

        // Get All States
        IEnumerable<State> GetStates();
        State GetState(int id);
    }
    public class LocationService : ILocationservice
    {
        private readonly ILocationRepository _addressRepository;
        private readonly IStateRepository _stateRepository;

        public LocationService(ILocationRepository addressRepository, IStateRepository stateRepository)
        {
            this._addressRepository = addressRepository;
            this._stateRepository = stateRepository;
        }
        public Location GetAddressByUserId(string userId)
        {
            return this._addressRepository.SearchFor(s => s.UserId == userId).FirstOrDefault();
        }

        public Location GetAddress(int id)
        {
            return this._addressRepository.Get(id);
        }

        public int SaveAddress(Location address)
        {
            this._addressRepository.Add(address);
            return this._addressRepository.SaveChanges();
        }

        public int UpdateAddress(Location address)
        {
            this._addressRepository.Update(address);
            return this._addressRepository.SaveChanges();
        }

        public IEnumerable<State> GetStates()
        {
            return this._stateRepository.GetAll();
        }

        public State GetState(int id)
        {
            return this._stateRepository.Get(id);
        }
    }
}
