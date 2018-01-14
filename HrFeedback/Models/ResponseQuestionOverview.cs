using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrFeedback.Models
{
    public class ResponseQuestionOverview
    {
        public string QuestionString { get; set; }
        public int ResponseCount { get; set; }
        public IEnumerable<ResponseAnswerOverview> ResponseAnswerOverviews { get; set; }
    }
}