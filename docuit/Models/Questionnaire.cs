﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class Questionnaire
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public string QuestionnaireReportId { get; set; }
        public string QuestionnaireTypeId { get; set; }
        public string QuestionnaireTypeName { get; set; }
        public int ParagraphId { get; set; }
        public string ParagraphName { get; set; }
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
    }
}
