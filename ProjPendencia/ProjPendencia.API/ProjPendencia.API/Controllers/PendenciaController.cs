using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjPendencia.API.Data;
using ProjPendencia.API.Model;

namespace ProjPendencia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendenciaController : ControllerBase
    {
        private readonly PendenciaContext _context;

        public PendenciaController(PendenciaContext context)
        {
            _context = context;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pendencia>>> GetPendencia()
        {
            return await _context.Pendencia.ToListAsync();
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pendencia>> GetPendencia(int id)
        {
            var client = await _context.Pendencia.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Pendencia>> PutPendencia(int id, Pendencia pendencia)
        {
            if (id != pendencia.Id)
            {
                return BadRequest();
            }

            _context.Entry(pendencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PendenciaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetPendencia", new { id = pendencia.Id }, pendencia);
            return await _context.Pendencia.FindAsync(pendencia.Id);
        }

        // POST: api/Client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pendencia>> PostPendencia(Pendencia pendencia)
        {
            _context.Pendencia.Add(pendencia);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetClient", new { id = client.Id }, client);
            var pend = await _context.Pendencia.FindAsync(pendencia.Id);
            return pend;
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pendencia>> DeletePendencia(int id)
        {
            var client = await _context.Pendencia.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Pendencia.Remove(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        private bool PendenciaExists(int id)
        {
            return _context.Pendencia.Any(e => e.Id == id);
        }

        private int getMaxId()
        {
            return _context.Pendencia
                .OrderByDescending(u => u.Id).FirstOrDefault().Id;
        }
    }
}
