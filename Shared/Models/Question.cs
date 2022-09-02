using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPOC2.Shared.Models
{
    public enum QuestionType
    {
        CheckBox,
        MultiCheckBox,
        DropDown,
        SelectOne,
        Text,
        Textarea,
        File,
        Date,
        MonthYear
    }
    public enum FrontendAction
    {
        Proceed,
        Block,
        Advise,
        AdviseTickbox,
        FollowupQuestion,
        OfflineTreatment
    }

    public enum BackendAction
    {
        None,
        Block,
        AdvisePrompt,
        PresciberAdvise
    }

    public class Question 
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string SubText { get; set; }
        public string PlaceholderText { get; set; }
        public bool IsReviewQuestion { get; set; }
        public QuestionType Type { get; set; }
        public List<QuestionOption> Options { get; set; }
        public string SelectedOption { get; set; }
        public string Answer { get; set; }
        public int SelectedOptionId { get; set; }
        public DateTime SeletedDate { get; set; }

    }

    public class QuestionOption
    {
        public int OptionId { get; set; }
        public string Text { get; set; }
        public int Sequance { get; set; }

        public FrontendAction FrontendAction { get; set; }
        public BackendAction BackendAction { get; set; }

        public string FrontendActionTitle { get; set; }
        public string FrontendActionText { get; set; }

        public string BackEndActionTitle { get; set; }
        public string BackEndActionText { get; set; }

        public bool UncheckRestOptions { get; set; }

        public int FollowUpQuestionId { get; set; }

        public  Question FollowUpQuestion { get; set; }

    }
}
