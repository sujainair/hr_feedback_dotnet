using HrFeedback.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HrFeedback.Controllers
{
    public class ResponseController : ApiController
    {
        // GET api/response
        public IEnumerable<ResponseQuestionOverview> Get()
        {
            return DataSingleton.Instance.ResponsesOverview;
        }

        // POST api/response
        // Verifies if post data is valid and updates the Responses Data
        // Sends bad request response in case of bad data,
        // with appropriate message, hence so many try-catches
        // Also updates the Overview 
        public void Post([FromBody]JArray userResponses)
        {
            if(userResponses.Count() != DataSingleton.Questions.Count())
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "All questions must be answered"
                });
            }
            ICollection<Response> finalResponses = new List<Response>();
            ICollection<int> responseQIDs = new List<int>();
            foreach(JObject userResponse in userResponses)
            {
                string userQuestion;
                try
                {
                    userQuestion = userResponse.GetValue("qID").ToString();
                }
                catch
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        ReasonPhrase = "Missing qID value"
                    });
                }
                if (Int32.TryParse(userQuestion, out int qID))
                {
                    if (responseQIDs.Contains(qID))
                    {
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            ReasonPhrase = string.Concat("qID: ", qID, ", allowed only once per response")
                        });
                    } else
                    {
                        responseQIDs.Add(qID);
                    }

                    Question responseQuestion;
                    try
                    {
                        responseQuestion = DataSingleton.Questions.First((q) => q.Id == qID);
                    }
                    catch
                    {
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            ReasonPhrase = string.Concat("qID: ", qID, ", does not exist")
                        });
                    }
                    ICollection<Answer> responseAnswers = new List<Answer>();
                    JArray userAnswers;
                    try
                    {
                        userAnswers = JArray.Parse(userResponse.GetValue("aIDs").ToString());
                        if(userAnswers.Count() < 1)
                        {
                            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                            {
                                ReasonPhrase = string.Concat("qID: ", qID, ", expected to have at least one answer")
                            });
                        }
                    }
                    catch
                    {
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            ReasonPhrase = string.Concat("qID: ", qID, ", expected to have at least one answer")
                        });
                    }
                    if (responseQuestion.AnswerType == Question.Type.Single && userAnswers.Count() != 1) 
                    {
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            ReasonPhrase = string.Concat("qID: ", qID, ", expected to have only one answer")
                        });
                    }
                    foreach (JValue userAnswer in userAnswers)
                    {
                        if (Int32.TryParse(userAnswer.ToString(), out int ansID))
                        {
                            try
                            {
                                Answer responseAnswer = responseQuestion.Answers.First((a) => a.Id == ansID);
                                responseAnswers.Add(responseAnswer);
                            }
                            catch (InvalidOperationException)
                            {
                                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                                {
                                    ReasonPhrase = string.Concat("aID: ", ansID, ", is not part of the Question with qID: ", qID)
                                });
                            }
                        }
                        else
                        {
                            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                            {
                                ReasonPhrase = string.Concat("aID: ", userAnswer.ToString(), ", should be a valid integer")
                            });
                        }
                    }
                    Response response = new Response()
                    {
                        Question = responseQuestion,
                        Answers = responseAnswers
                    };
                    finalResponses.Add(response);
                }
                else
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        ReasonPhrase = string.Concat("qID: ", userQuestion,", should be a valid integer")
                    });
                }

            }
            DataSingleton.Instance.Responses = DataSingleton.Instance.Responses.Concat(finalResponses).ToList();
        }
    }
}