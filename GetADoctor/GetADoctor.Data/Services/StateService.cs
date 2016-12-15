using GetADoctor.Data.Repositories;
using GetADoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetADoctor.Data.Services
{
    public interface IStateService
    {
        IEnumerable<State> GetStates();
        State GetState(int id);
        void SaveState(State state);
    }
    public class StateService : IStateService
    {
        private readonly IStateRepository stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            this.stateRepository = stateRepository;
        }

        public State GetState(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<State> GetStates()
        {
            return stateRepository.GetAll();
        }

        public void SaveState(State state)
        {
            this.stateRepository.Add(state);
            this.stateRepository.SaveChanges();
        }
    }
}
