using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Tss.Core.Generics;

namespace Tss.Infrastructure.Generics
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
        public virtual async Task<List<TEntity>> Get(
            [FromQuery] SieveModel sieveModel
        )
        {
            return await _service.Get(sieveModel);
        }

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

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.Post(entity);

            return CreatedAtAction(
                nameof(Get),
                new { id = EntityId(entity) },
                entity
            );
        }

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

            await _service.Delete(entity);

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
