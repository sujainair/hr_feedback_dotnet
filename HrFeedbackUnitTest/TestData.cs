using HrFeedback.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrFeedbackUnitTest
{
    //Contains some static data for testing
    class TestData
    {
        private TestData() { }
        
        //An example of a correct response from client
        private static string correctResponse = "[{\"qID\" : 1,\"aIDs\" : [2]}," +
            "{\"qID\" : 2,\"aIDs\" : [1,3]}," +
            "{\"qID\" : 3,\"aIDs\" : [1]}," +
            "{\"qID\" : 4,\"aIDs\" : [1]}," +
            "{\"qID\" : 5,\"aIDs\" : [1,2]}]";
        public static JArray CorrectResponse { get; } = JArray.Parse(correctResponse);

        //Response does not answer all questions
        private static string incompleteResponse = "[{\"qID\" : 1,\"aIDs\" : [2]}," +
            "{\"qID\" : 2,\"aIDs\" : [1,3]}]";
        public static JArray IncompleteResponse { get; } = JArray.Parse(incompleteResponse);

        //Response is missing qID 2
        private static string responseWithoutQID = "[{\"qID\" : 1,\"aIDs\" : [2]}," +
            "{\"aIDs\" : [1,3]}," +
            "{\"qID\" : 3,\"aIDs\" : [1]}," +
            "{\"qID\" : 4,\"aIDs\" : [1]}," +
            "{\"qID\" : 5,\"aIDs\" : [1,2]}]";
        public static JArray ResponseWithoutQID { get; } = JArray.Parse(responseWithoutQID);

        //Response contains no answers for qID 2
        private static string responseWithoutAID = "[{\"qID\" : 1,\"aIDs\" : [2]}," +
            "{\"qID\" : 2, \"aIDs\" : []}," +
            "{\"qID\" : 3,\"aIDs\" : [1]}," +
            "{\"qID\" : 4,\"aIDs\" : [1]}," +
            "{\"qID\" : 5,\"aIDs\" : [1,2]}]";
        public static JArray ResponseWithoutAID { get; } = JArray.Parse(responseWithoutAID);

        //Response contains qID 0, which does not exist
        private static string responseWithWrongQID = "[{\"qID\" : 0,\"aIDs\" : [1]}," +
            "{\"qID\" : 2,\"aIDs\" : [1,3]}," +
            "{\"qID\" : 3,\"aIDs\" : [1]}," +
            "{\"qID\" : 4,\"aIDs\" : [1]}," +
            "{\"qID\" : 5,\"aIDs\" : [1,2]}]";
        public static JArray ResponseWithWrongQID { get; } = JArray.Parse(responseWithWrongQID);

        //Response contains aID 5 for qID 1, which does not exist
        private static string responseWithWrongAID = "[{\"qID\" : 1,\"aIDs\" : [5]}," +
            "{\"qID\" : 2,\"aIDs\" : [1,3]}," +
            "{\"qID\" : 3,\"aIDs\" : [1]}," +
            "{\"qID\" : 4,\"aIDs\" : [1]}," +
            "{\"qID\" : 5,\"aIDs\" : [1,2]}]";
        public static JArray ResponseWithWrongAID { get; } = JArray.Parse(responseWithWrongAID);

        //Response contains 2 aIDs for a Single choice question, qID 1
        private static string responseWithExtraAID = "[{\"qID\" : 1,\"aIDs\" : [1,2]}," +
            "{\"qID\" : 2,\"aIDs\" : [1,3]}," +
            "{\"qID\" : 3,\"aIDs\" : [1]}," +
            "{\"qID\" : 4,\"aIDs\" : [1]}," +
            "{\"qID\" : 5,\"aIDs\" : [1,2]}]";
        public static JArray ResponseWithExtraAID { get; } = JArray.Parse(responseWithExtraAID);
    }
}
