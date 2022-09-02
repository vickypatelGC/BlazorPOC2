using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using BlazorPOC2.Shared.Models;

namespace BlazorPOC2.Client.Pages
{
    public partial class Questionaries
    {
        int questionIndex = 0;
        string displayIndex = string.Empty;
        Type componentType = null;
        private List<Question>? questions;
        private Question question;
        Dictionary<string, object> componentParams;
        bool isDisplayNext = false;
        bool isDisplayBack = false;
        bool isDisplayCountinue = false;
        bool IsDisabled = true;
        bool isFollowupQuestion = false;
        protected override async Task OnInitializedAsync()
        {
            questions = await Http.GetFromJsonAsync<List<Question>>("Question");
            if (questions != null && questions.Count > 0)
            {
                LoadQuestion(false);
            }
        }

        public void OnSelectOneChanged(int optionId, string optionText)
        {
            if (!string.IsNullOrEmpty(optionText))
            {
                question.Answer = optionText;
                question.SelectedOptionId = optionId;

                OnNextOrBack(1);
            }
        }

        public void OnInputTextChanged(ChangeEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value?.ToString()))
            {
                IsDisabled = false;
                question.Answer = e.Value?.ToString();
            }
            else
            {
                IsDisabled = true;
            }
        }

        public void OnNextOrBack(int moveTo)
        {
            if (moveTo == 1)
                questionIndex += moveTo;
            else if (moveTo == -1 && !isFollowupQuestion)
                questionIndex += moveTo;
            Question questionTobeload = moveTo == 1 ? question : questions[questionIndex];
            if (questionTobeload.Options != null && !isFollowupQuestion)
            {
                var selectOption = questionTobeload.Options.FirstOrDefault(x => x.OptionId == questionTobeload.SelectedOptionId);
                if (selectOption != null && selectOption.FollowUpQuestion != null)
                {
                    question = selectOption.FollowUpQuestion;
                    isFollowupQuestion = true;
                    if (moveTo == 1)
                        questionIndex -= moveTo;
                }
            }
            else
                isFollowupQuestion = false;
            
            if (questionIndex > questions.Count)
                return;

            LoadQuestion(isFollowupQuestion);

            isDisplayBack = questionIndex != 0;
            isDisplayCountinue = questionIndex == questions.Count - 1;

            if (questionIndex == questions.Count - 1)
                isDisplayNext = false;
            else if (componentType == typeof(Components.SelectOne))
            {
                if (!string.IsNullOrEmpty(question.Answer))
                    isDisplayNext = true;
                else
                    isDisplayNext = false;
            }
            else
                isDisplayNext = true;
        }

        public void LoadQuestion(bool hasFollowupQuestion)
        {
            if (!hasFollowupQuestion)
            {
                question = questions[questionIndex];
                displayIndex = (questionIndex + 1).ToString();
            }
            else
                displayIndex = string.Format("{0}.1", questionIndex + 1);

            componentParams = new Dictionary<string, object>() { { "question", question } };
            switch (question.Type)
            {
                case QuestionType.SelectOne:
                    componentType = typeof(Components.SelectOne);
                    componentParams.Add("OnSelectOneChanged", OnSelectOneChanged);
                    break;
                case QuestionType.MultiCheckBox:
                    componentType = typeof(Components.MultiCheckBox);
                    IsDisabled = false;
                    break;
                case QuestionType.CheckBox:
                    componentType = typeof(Components.CheckBox);
                    IsDisabled = false;
                    break;
                case QuestionType.Text:
                    componentType = typeof(Components.TextBox);
                    componentParams.Add("OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged));

                    if (string.IsNullOrEmpty(question.Answer))
                        IsDisabled = true;
                    break;
                case QuestionType.Textarea:
                    componentType = typeof(Components.TextArea);
                    componentParams.Add("OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged));

                    if (string.IsNullOrEmpty(question.Answer))
                        IsDisabled = true;
                    break;
                case QuestionType.DropDown:
                    componentType = typeof(Components.DropDownList);
                    componentParams.Add("OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged));

                    if (string.IsNullOrEmpty(question.Answer))
                        IsDisabled = true;
                    break;
                case QuestionType.Date:
                    componentType = typeof(Components.DatePicker);
                    componentParams.Add("OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged));

                    if (string.IsNullOrEmpty(question.Answer))
                        IsDisabled = true;
                    break;
            }

            StateHasChanged();
        }
    }
}