using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrFeedback.Models
{
    public class DataSingleton
    {
        //Since we are not using a database for this project,
        //we can store the data in a singleton class
        private static DataSingleton instance;
        private DataSingleton() { }

        public static DataSingleton Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataSingleton();
                }
                return instance;
            }
        }

        public static ICollection<Question> Questions {
            get
            {
                Question[] questions = new Question[]
                {
                    new Question
                    {
                        Id = 1,
                        QuestionString = "How did you find out about this job opportunity?",
                        AnswerType =Question.Type.Single,
                        Answers = new Answer[]
                        {
                            new Answer {Id = 1, AnswerString = "StackOverflow"},
                            new Answer {Id = 2, AnswerString = "Indeed"},
                            new Answer {Id = 3, AnswerString = "Other"}
                        }
                    },
                    new Question
                    {
                        Id = 2,
                        QuestionString = "How do you find the company’s location?",
                        AnswerType = Question.Type.Multi,
                        Answers = new Answer[]
                        {
                            new Answer {Id = 1, AnswerString = "Easy to access by public transport"},
                            new Answer {Id = 2, AnswerString = "Easy to access by car"},
                            new Answer {Id = 3, AnswerString = "In a pleasant area"},
                            new Answer {Id = 4, AnswerString = "None of the above"}
                        }
                    },
                    new Question
                    {
                        Id = 3,
                        QuestionString = "What was your impression of the office where you had the interview?",
                        AnswerType = Question.Type.Single,
                        Answers = new Answer[]
                        {
                            new Answer {Id = 1, AnswerString = "Tidy"},
                            new Answer {Id = 2, AnswerString = "Sloppy"},
                            new Answer {Id = 3, AnswerString = "Did not notice"}
                        }
                    },
                    new Question
                    {
                        Id = 4,
                        QuestionString = "How technically challenging was the interview?",
                        AnswerType = Question.Type.Single,
                        Answers = new Answer[]
                        {
                            new Answer {Id = 1, AnswerString = "Very difficult"},
                            new Answer {Id = 2, AnswerString = "Difficult"},
                            new Answer {Id = 3, AnswerString = "Moderate"},
                            new Answer {Id = 4, AnswerString = "Easy"}
                        }
                    },
                    new Question
                    {
                        Id = 5,
                        QuestionString = "How can you describe the manager that interviewed you?",
                        AnswerType = Question.Type.Multi,
                        Answers = new Answer[]
                        {
                            new Answer {Id = 1, AnswerString = "Enthusiastic"},
                            new Answer {Id = 2, AnswerString = "Polite"},
                            new Answer {Id = 3, AnswerString = "Organized"},
                            new Answer {Id = 4, AnswerString = "Could not tell"}
                        }
                    }
                };
                return questions;
            }
        }
        private ICollection<Response> responses = new List<Response>();
        //When setting a response also update the response overview
        public ICollection<Response> Responses {
            get
            {
                return responses;
            }
            set {
                responses = value;
                UpdateResponseOverview();
            }
        }
        private ICollection<ResponseQuestionOverview> responsesOverview = new List<ResponseQuestionOverview>();
        public ICollection<ResponseQuestionOverview> ResponsesOverview
        {
            get
            {
                return responsesOverview;
            }
        } 

        private void UpdateResponseOverview()
        {
            ICollection<ResponseQuestionOverview> responseQuestionOverview = new List<ResponseQuestionOverview>();
            foreach (Question question in Questions)
            {
                IEnumerable<Response> responseByQuestion = responses.Where((r) => r.Question.Id == question.Id);
                ICollection<ResponseAnswerOverview> apiAnswers = new List<ResponseAnswerOverview>();
                foreach (Answer answer in question.Answers)
                {
                    float ansCount = 0;
                    foreach (Response response in responseByQuestion)
                    {
                        ansCount += response.Answers.Where((a) => a.Id == answer.Id).Count();
                    }
                    ResponseAnswerOverview apiAnswer = new ResponseAnswerOverview()
                    {
                        AnswerString = answer.AnswerString,
                        ResponseCount = Convert.ToInt32(ansCount),
                        ResponsePercentage = ((ansCount/ (float)responseByQuestion.Count()) * 100.0F).ToString("0.00")
                    };
                    apiAnswers.Add(apiAnswer);
                }
                ResponseQuestionOverview apiResponse = new ResponseQuestionOverview()
                {
                    QuestionString = question.QuestionString,
                    ResponseCount = responseByQuestion.Count(),
                    ResponseAnswerOverviews = apiAnswers
                };
                responseQuestionOverview.Add(apiResponse);
            }
            responsesOverview = responseQuestionOverview;
        }
    }
}