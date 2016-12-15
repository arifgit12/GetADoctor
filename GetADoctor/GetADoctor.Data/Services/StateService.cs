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
    }
    public class StateService : IStateService
    {
        private readonly IStateRepository stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            this.stateRepository = stateRepository;
        }
        public IEnumerable<State> GetStates()
        {
            return stateRepository.GetAll();
        }
    }
}
