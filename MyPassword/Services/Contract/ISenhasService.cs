using MyPassword.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services.Contract
{
    public interface ISenhasService 
    {
        IList<Senha> GetSenhas();
        void AdicionarSenha(Senha senha);
    }
}
