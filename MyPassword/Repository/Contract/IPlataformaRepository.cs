using MyPassword.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Repository.Contract
{
    public interface IPlataformaRepository
    {
        IList<Plataforma> Get();

        void Post(Plataforma plataforma);
    }
}
