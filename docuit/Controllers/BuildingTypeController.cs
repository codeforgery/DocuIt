﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DocuItService.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    //[Microsoft.AspNetCore.Authorization.Authorize]

    public class BuildingTypeController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public BuildingTypeController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        // GET: api/values
        [HttpGet("GetAll")]
        public IEnumerable<BuildingType> GetAll([FromBody] BuildingType objParams)
        {
            IEnumerable<BuildingType> objReturn = MyDBContext.BuildingTypes.Where(x=>x.CompanyId==objParams.CompanyId);

            if (objReturn == null)
            {
                return null;
            }
            return (objReturn);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] BuildingType objParams)
        {
            BuildingType objReturn;

            if (objParams == null)
            {
                return BadRequest();
            }
            objReturn = await MyDBContext.BuildingTypes.FindAsync(objParams.CompanyId, objParams.Id);
            if (objReturn == null)
            {
                return NotFound();
            }
            return Ok(objReturn);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete([FromBody] BuildingType objParams)
        {
            BuildingType objReturn;

            if (objParams == null)
            {
                return BadRequest();
            }
            objReturn = await MyDBContext.BuildingTypes.FindAsync(objParams.CompanyId, objParams.Id);
            if (objReturn == null)
            {
                return NotFound();
            }
            else
            {
                MyDBContext.BuildingTypes.Remove(objReturn);
                await MyDBContext.SaveChangesAsync();
                return Ok();
            }
        }

        // POST api/values (ADD)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BuildingType objParams)
        {
            if (objParams == null)
            {
                return NotFound();
            }
            MyDBContext.BuildingTypes.Add(objParams);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Client Object Not Valid.");
            }
            return Ok(objParams.Id);
        }

        // PUT api/values/5 (FULL UPDATE)
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BuildingType objParams)
        {
            MyDBContext.BuildingTypes.Update(objParams);
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
        //[HttpPatch]
        //public async Task<IActionResult> Patch(int CompanyId, [FromBody] JsonPatchDocument<Company> companyToPatch)
        //{
        //    Company company;

        //    if (companyToPatch == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (CompanyId < 1)
        //    {
        //        return BadRequest();
        //    }
        //    company = await MyDBContext.Company.FindAsync(CompanyId);
        //    companyToPatch.ApplyTo(company);
        //    if (ModelState.IsValid)
        //    {
        //        await MyDBContext.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //    return Ok();
        //}

        //[HttpPatch]
        //public async Task<IActionResult> Patch([FromBody] JToken jsonDocument)
        //{
        //    Company company;
        //    JsonPatchDocument<Company> jsonPatchDocument;

        //    IEnumerable MyToken = jsonDocument.First ;

        //    company = (Company)MyToken;

        //    if (jsonDocument == null)
        //    {
        //        return BadRequest();
        //    }
        //    //if (CompanyId < 1)
        //    //{
        //    //    return BadRequest();
        //    //}
        //    //company = await MyDBContext.Company.FindAsync(CompanyId);
        //    //companyToPatch.ApplyTo(company);
        //    //if (ModelState.IsValid)
        //    //{
        //    //    await MyDBContext.SaveChangesAsync();
        //    //}
        //    //else
        //    //{
        //    //    return BadRequest();
        //    //}
        //    return Ok(MyToken);
        //}

        //// DELETE api/values/5
        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromBody] BuildingType CompanyParams)
        //{
        //    Company company;

        //    if (CompanyParams == null)
        //    {
        //        return BadRequest("Parameters Object not valid.");
        //    }
        //    company = await MyDBContext.Company.FindAsync(CompanyParams.CompanyId);
        //    try
        //    {
        //        MyDBContext.Company.Remove(company);
        //        await MyDBContext.SaveChangesAsync();
        //    }
        //    catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        //    {
        //        return BadRequest("CompanyID not valid.");
        //    }
        //    return Ok();
        //}
    }
}