using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrFeedback.Models
{
    public class Response
    {
        public Question Question { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}