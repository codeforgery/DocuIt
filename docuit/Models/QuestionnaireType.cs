﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class QuestionnaireType
    {
        public QuestionnaireType()
        {
            QuestionnaireParagraph = new HashSet<QuestionnaireParagraph>();
            QuestionnaireQuestions = new HashSet<QuestionnaireQuestions>();
        }

        public int CompanyId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<QuestionnaireParagraph> QuestionnaireParagraph { get; set; }
        public virtual ICollection<QuestionnaireQuestions> QuestionnaireQuestions { get; set; }
    }
}