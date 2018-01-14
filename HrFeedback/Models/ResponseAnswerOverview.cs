using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrFeedback.Models
{
    public class ResponseAnswerOverview
    {
        public string AnswerString { get; set; }
        public int ResponseCount { get; set; }
        public string ResponsePercentage { get; set; }
    }
}