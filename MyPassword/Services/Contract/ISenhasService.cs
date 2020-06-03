using MyPassword.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services.Contract
{
    public interface ISenhasService
    {
        IList<Senha> GetAll();

        Senha GetById(int SenhaId);

        int InsertOrUpdate(PlataformaSenha plataformaSenha);

        void Delete(int SenhaId);
    }
}
