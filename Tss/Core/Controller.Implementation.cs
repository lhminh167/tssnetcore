using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tss.Models;
using Tss.Core;
using Sieve.Models;

namespace Tss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller<TEntity> : ControllerBase, IController<TEntity> where TEntity : class
    {        
        private readonly IService<TEntity> _service;
        private readonly string _idType;

        public Controller(
            IService<TEntity> service, 
            string idType
        )
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _idType = idType ?? throw new ArgumentNullException(nameof(idType));
        }

        // GET: api/Products
        [HttpGet]
        public virtual List<TEntity> Get(
            [FromQuery] SieveModel sieveModel
        )
        {
            return _service.Get(sieveModel);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TEntity entity = await _service.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != EntityId(entity))
            {
                return BadRequest();
            }

            try
            {
                await _service.Put(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                TEntity entityInDb = await _service.Get(id);

                if (entityInDb == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.Post(entity);

            return CreatedAtAction(
                "GetProduct",
                new { id = EntityId(entity) },
                entity
            );
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TEntity entity = await _service.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return Ok(entity);
        }

        private int EntityId(TEntity entity)
        {
            return (int)entity
            .GetType()
            .GetProperty(_idType)
            .GetValue(entity, null);
        }
    }
}