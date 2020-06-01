using MyPassword.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Repository.Contract
{
    public interface ISenhasRepository
    {
        IList<Senha> GetAll();

        Senha GetById(int SenhaId);

        int InsertOrUpdate(Senha senha);

        void Delete(int SenhaId);
    }
}
