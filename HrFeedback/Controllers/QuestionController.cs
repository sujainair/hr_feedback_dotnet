using HrFeedback.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HrFeedback.Controllers
{
    public class QuestionController : ApiController
    {
        public IEnumerable<Question> GetAllQuestions()
        {            
            return DataSingleton.Questions;
        }        
    }
}
