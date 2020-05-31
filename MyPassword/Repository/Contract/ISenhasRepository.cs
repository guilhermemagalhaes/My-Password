using MyPassword.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Repository.Contract
{
    public interface ISenhasRepository
    {
        IList<Senha> Get();
        void Post(Senha senha);        
    }
}
