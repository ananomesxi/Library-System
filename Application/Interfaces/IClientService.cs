using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IClientService
    {
        void UpdateClientFine(ClientUser client);
        void ViewFine(ClientUser client);

    }
}
