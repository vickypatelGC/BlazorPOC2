using BlazorPOC2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPOC2.Shared.Common
{
    public static class QuestionData
    {

        public static List<Question> GetQuestions()
        {
            List<Question> Questions = new List<Question> {

              new Question { QuestionId = 1 ,  Text = "How often do you get erection problems during sex?" , Type = QuestionType.SelectOne , Options =new List<QuestionOption> { new QuestionOption { Text = "Most or all of the time" , Sequance = 1 , OptionId = 1 } , new QuestionOption { Text = "Sometimes", Sequance = 2, OptionId = 2 }, new QuestionOption { Text = "Rarely or never", Sequance = 3, OptionId = 3 } } },
              //new Question { QuestionId = 2 ,  Text = "Have you seen a doctor about your symptoms?" , Type = QuestionType.SelectOne , Options =new List<QuestionOption> { new QuestionOption { Text = "Yes I have", Sequance = 1 , OptionId = 1 } , new QuestionOption { Text = "No I haven’t", Sequance = 2, OptionId = 2 } } },
              new Question { QuestionId = 3 ,  Text = "Which treatment(s) have you used before?" , Type = QuestionType.MultiCheckBox , Options =new List<QuestionOption> { new QuestionOption { Text = "Viagra or Sildenafil", Sequance = 1 , OptionId = 1 } , new QuestionOption { Text = "Cialis or Tadalafil", Sequance = 2, OptionId = 2 }, new QuestionOption { Text = "Spedra", Sequance = 3, OptionId = 3 }, new QuestionOption { Text = "I've never used any before", Sequance = 4, OptionId = 4 } } },
              //new Question { QuestionId = 4 ,  Text = "Which type of treatment are you looking for?" , Type = QuestionType.SelectOne , Options =new List<QuestionOption> { new QuestionOption { Text = "Tablets or cream", Sequance = 1 , OptionId = 1 } , new QuestionOption { Text = "Injectable or insertable treatment", Sequance = 2, OptionId = 2 },  new QuestionOption { Text = "I don't know" , Sequance = 3 , OptionId = 3 } } },
              new Question { QuestionId = 5 ,  Text = "What's your blood pressure?" , Type = QuestionType.SelectOne , Options =new List<QuestionOption> { new QuestionOption { Text = "Normal (Between 90/60 - 140/90) ", Sequance = 1 , OptionId = 1 , FollowUpQuestion = new Question { QuestionId = 15, Text = "Please enter information here", Type = QuestionType.Text } } , new QuestionOption { Text = "Low (Below 90/60) ", Sequance = 2, OptionId = 2 },  new QuestionOption { Text = "High (Above 140/90) ", Sequance = 3 , OptionId = 3 }, new QuestionOption { Text = "I don’t know", Sequance = 4, OptionId = 4 } } },
              //new Question { QuestionId = 6 ,  Text = "Was your blood pressure checked in the last year?" , Type = QuestionType.SelectOne , Options =new List<QuestionOption> { new QuestionOption { Text = "Yes", Sequance = 1 , OptionId = 1 } , new QuestionOption { Text = "No", Sequance = 2, OptionId = 2 } } },
              //new Question { QuestionId = 7 ,  Text = "Do you have chest pain or tightness?" , Type = QuestionType.SelectOne , Options =new List<QuestionOption> { new QuestionOption { Text = "Yes", Sequance = 1 , OptionId = 1 } , new QuestionOption { Text = "No", Sequance = 2, OptionId = 2 } } },
              //new Question { QuestionId = 8 ,  Text = "Do you take angina medicines, nitrates, poppers or recreational drugs?" , Type = QuestionType.SelectOne , Options =new List<QuestionOption> { new QuestionOption { Text = "Yes", Sequance = 1 , OptionId = 1 } , new QuestionOption { Text = "No", Sequance = 2, OptionId = 2 } } },
              //new Question { QuestionId = 9 ,  Text = "Is there anything else you want to add?" , Type = QuestionType.SelectOne , Options =new List<QuestionOption> { new QuestionOption { Text = "Yes", Sequance = 1 , OptionId = 1 } , new QuestionOption { Text = "No", Sequance = 2, OptionId = 2 } } },
              new Question { QuestionId = 10 , Text = "Please enter any additional information here" , Type = QuestionType.Text },
              new Question { QuestionId = 11 , Text = "Additional information" , Type = QuestionType.Textarea },
              new Question { QuestionId = 12 ,  Text = "Are you sure?" , Type = QuestionType.CheckBox , Options =new List<QuestionOption> { new QuestionOption { Text = "Yes", Sequance = 1 , OptionId = 1 }  } },
              new Question { QuestionId = 13 ,  Text = "Which treatments have you used before?" , Type = QuestionType.DropDown , Options =new List<QuestionOption> { new QuestionOption { Text = "Viagra or Sildenafil", Sequance = 1 , OptionId = 1 } , new QuestionOption { Text = "Cialis or Tadalafil", Sequance = 2, OptionId = 2 }, new QuestionOption { Text = "Spedra", Sequance = 3, OptionId = 3 }, new QuestionOption { Text = "I've never used any before", Sequance = 4, OptionId = 4 } } },
              new Question { QuestionId = 14 ,  Text = "Date of birth" , Type = QuestionType.Date },

            };

            return Questions;

        }

    }
}
