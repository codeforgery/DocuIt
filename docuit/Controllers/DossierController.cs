﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DocuItService.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class DossierController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public DossierController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        // GET: api/values
        [HttpGet("{GetAll}")]
        public IEnumerable<Dossier> Get([FromBody] Project projectParameters)
        {
            IEnumerable<Dossier> dossiers = MyDBContext.Dossier.Where(x => x.CompanyId == projectParameters.CompanyId && x.ProjectId == projectParameters.ProjectId);

            if (dossiers == null)
            {
                return null;
            }
            return dossiers;
        }

        // GET api/values/5
        [HttpGet]
        public Dossier Get([FromBody] Dossier dossierParameters)
        {
            Dossier dossier = (Dossier)MyDBContext.Dossier.FirstOrDefault(d => d.CompanyId == dossierParameters.CompanyId && d.ProjectId == dossierParameters.ProjectId && d.DossierId == dossierParameters.DossierId);

            if (dossier == null)
            {
                return null;
            }
            return dossier;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dossier dossierParameters)
        {
            if (dossierParameters == null)
            {
                return NotFound();
            }
            MyDBContext.Dossier.Add(dossierParameters);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Client Object Not Valid.");
            }
            return Ok(dossierParameters.DossierId );
        }

        // PUT api/values/5 (FULL UPDATE)
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Dossier dossierParameters)
        {
            MyDBContext.Update(dossierParameters);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }

        // PATCH api/values (PARTIAL UPDATE)
        [HttpPatch]
        public async Task<IActionResult> Patch(int CompanyId, [FromBody] JsonPatchDocument<Dossier> dossierToPatch)
        {
            Dossier dossier;

            if (dossierToPatch == null)
            {
                return BadRequest();
            }
            if (CompanyId < 1)
            {
                return BadRequest();
            }
            dossier = await MyDBContext.Dossier.FindAsync(CompanyId);
            dossierToPatch.ApplyTo(dossier);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Dossier dossierToDelete)
        {
            Dossier dossier;

            if (dossierToDelete == null)
            {
                return BadRequest("Parameters Object not valid.");
            }
            dossier = MyDBContext.Dossier.FirstOrDefault(d => d.CompanyId == dossierToDelete.CompanyId && d.ProjectId == dossierToDelete.ProjectId && d.DossierId == dossierToDelete.DossierId);
            try
            {
                MyDBContext.Dossier.Remove(dossier);
                await MyDBContext.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return BadRequest("UserId not valid.");
            }
            return Ok();
        }
    }
} 