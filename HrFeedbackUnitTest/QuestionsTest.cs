using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using HrFeedback.Controllers;
using HrFeedback.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HrFeedbackUnitTest
{
    [TestClass]
    public class QuestionsTest
    {
        [TestMethod]
        public void GetQuestions()
        {
            ICollection<Question> questions = DataSingleton.Questions;
            QuestionController controller = new QuestionController();
            ICollection<Question> result = (ICollection<Question>)controller.GetAllQuestions();
            Assert.AreEqual(questions.Count, result.Count);
        }
    }
}
