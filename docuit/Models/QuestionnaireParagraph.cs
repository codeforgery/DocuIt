﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class QuestionnaireParagraph
    {
        public int CompanyId { get; set; }
        public string QuestionnaireTypeId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortIndex { get; set; }

        public virtual QuestionnaireType QuestionnaireType { get; set; }
    }
}
