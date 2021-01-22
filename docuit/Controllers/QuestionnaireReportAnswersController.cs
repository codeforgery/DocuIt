﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace DocuItService.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    //[Microsoft.AspNetCore.Authorization.Authorize]

    public class QuestionnaireReportAnswersController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public QuestionnaireReportAnswersController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        //[HttpGet("GetQuestionnaire")]
        public IEnumerable<QuestionnaireReportAnswers> Get([FromBody] QuestionnaireParams param)
        {
            IEnumerable<QuestionnaireReportAnswers> questionnaire;

            questionnaire = (IEnumerable<QuestionnaireReportAnswers>)MyDBContext.Questionnaire.ToList(); ;
            return questionnaire;
        }

        [HttpPut]
        public async Task<IActionResult> Put ([FromBody] IEnumerable<QuestionnaireReportAnswers> questionnaire)
        {
            IDbContextTransaction transaction = MyDBContext.Database.BeginTransaction();

            try
            {
                MyDBContext.QuestionnaireReportAnswers.RemoveRange(questionnaire);
                await MyDBContext.SaveChangesAsync();
                MyDBContext.QuestionnaireReportAnswers.AddRange(questionnaire);
                await MyDBContext.SaveChangesAsync();
            }
            catch
            {
                transaction.Rollback();
            }
            transaction.Commit();
            return Ok();
        }
    }
}

