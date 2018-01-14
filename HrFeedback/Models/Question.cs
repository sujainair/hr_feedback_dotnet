using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrFeedback.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public Type AnswerType { get; set; }

        public enum Type { Multi, Single }
    }
}