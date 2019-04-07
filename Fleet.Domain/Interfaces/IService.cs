using Fleet.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Domain.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        T Post<V>(T obj) where V : AbstractValidator<T>;

        T Post(T obj);

        T Put<V>(T obj) where V : AbstractValidator<T>;

        T Put(T obj);

        void Delete(int id);

        T Get(int id);

        IList<T> Get();
    }
}
