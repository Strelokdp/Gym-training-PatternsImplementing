using Gym_sports_training.Repository.Models;
using System.Collections.Generic;

namespace Gym_sports_training.Repository.DAL
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClientByID(int? clientId);
        void InsertClient(Client client);
        void DeleteClient(int clientId);
        void UpdateClient(Client client);
        void Save();
        void Dispose();
    }
}
