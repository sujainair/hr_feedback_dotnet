using System;
using System.Collections.Generic;
using System.Web.Http;
using HrFeedback.Controllers;
using HrFeedback.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace HrFeedbackUnitTest
{
    [TestClass]
    public class ResponsesTest
    {
        //Tests both post and get methods for Response
        [TestMethod]
        public void PostAndGetCorrectResponse()
        {
            ResponseController controller = new ResponseController();
            JArray correctResponse = TestData.CorrectResponse;
            controller.Post(correctResponse);
            List<ResponseQuestionOverview> responseOverview = (List<ResponseQuestionOverview>)controller.Get();
            //On 1 POST overview should show 1 respondent
            Assert.AreEqual(1, responseOverview[0].ResponseCount);
            controller.Post(correctResponse);
            List<ResponseQuestionOverview> newResponseOverview = (List<ResponseQuestionOverview>)controller.Get();
            //On 2 POSTs overview should show 2 respondents
            Assert.AreEqual(2, newResponseOverview[0].ResponseCount);
        }

        [TestMethod]
        [ExpectedException (typeof(HttpResponseException))]
        public void PostIncompleteResponse()
        {
            ResponseController controller = new ResponseController();
            JArray wrongResponse = TestData.IncompleteResponse;
            controller.Post(wrongResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostResponseWithoutQID()
        {
            ResponseController controller = new ResponseController();
            JArray wrongResponse = TestData.ResponseWithoutQID;
            controller.Post(wrongResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostResponseWithoutAID()
        {
            ResponseController controller = new ResponseController();
            JArray wrongResponse = TestData.ResponseWithoutAID;
            controller.Post(wrongResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostResponseWithWrongQID()
        {
            ResponseController controller = new ResponseController();
            JArray wrongResponse = TestData.ResponseWithWrongQID;
            controller.Post(wrongResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostResponseWithWrongAID()
        {
            ResponseController controller = new ResponseController();
            JArray wrongResponse = TestData.ResponseWithWrongAID;
            controller.Post(wrongResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostResponseWithExtraAID()
        {
            ResponseController controller = new ResponseController();
            JArray wrongResponse = TestData.ResponseWithExtraAID;
            controller.Post(wrongResponse);
        }
    }
}
