using System.Threading.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Tss.Core
{
    public interface IController
    {
    }

    public interface IController<T> : IController where T : class
    {
        List<T> Get(SieveModel sieveModel);

        Task<IActionResult> Get(int id);
        
        Task<IActionResult> Put(int id, T entity);
        
        Task<IActionResult> Post(T entity);

        Task<IActionResult> Delete(int id);
    }
}