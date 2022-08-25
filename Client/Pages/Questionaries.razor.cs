using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using BlazorPOC2.Client;
using BlazorPOC2.Client.Shared;
using BlazorPOC2.Shared.Models;

namespace BlazorPOC2.Client.Pages
{
    public partial class Questionaries
    {
        int questionIndex = 0;
        Type componentType = null;
        private List<Question>? questions;
        private Question question;
        Dictionary<string, object> componentParams;
        bool isDisplayNext = false;
        bool isDisplayBack = false;
        bool isDisplayCountinue = false;
        bool IsDisabled = true;
        protected override async Task OnInitializedAsync()
        {
            questions = await Http.GetFromJsonAsync<List<Question>>("Question");
            if (questions != null && questions.Count > 0)
            {
                LoadCompenent();
            }
        }

        public void OnSelectOneChanged(ChangeEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value?.ToString()))
            {
                question.Answer = e.Value?.ToString();
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
            questionIndex += moveTo;

            if (questionIndex > questions.Count)
                return;

            isDisplayBack = questionIndex != 0;
            isDisplayCountinue = questionIndex == questions.Count -1;

            if(questionIndex == questions.Count -1)
                isDisplayNext = false;
            else if (componentType == typeof(BlazorPOC2.Client.Pages.Components.SelectOne))
            {
                if (!string.IsNullOrEmpty(question.Answer))
                    isDisplayNext = true;
            }
            else
                isDisplayNext = true;

            LoadCompenent();
        }

        public void LoadCompenent()
        {
            question = questions[questionIndex];

            componentParams = new Dictionary<string, object>(){{"question",question} };

            switch (question.Type)
            {
                case QuestionType.SelectOne:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.SelectOne);
                    componentParams.Add("OnSelectOneChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnSelectOneChanged));
                    break;
                case QuestionType.MultiCheckBox:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.MultiCheckBox);
                    IsDisabled = false;
                    break;
                case QuestionType.CheckBox:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.CheckBox);
                    IsDisabled = false;
                    break;
                case QuestionType.Text:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.TextBox);
                    componentParams.Add("OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged));

                    if (string.IsNullOrEmpty(question.Answer))
                        IsDisabled = true;
                    break;
                case QuestionType.Textarea:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.TextArea);
                    componentParams.Add("OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged));

                    if (string.IsNullOrEmpty(question.Answer))
                        IsDisabled = true;
                    break;
                case QuestionType.DropDown:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.DropDownList);
                    componentParams.Add("OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged));

                    if (string.IsNullOrEmpty(question.Answer))
                        IsDisabled = true;
                    break;
                case QuestionType.Date:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.DatePicker);
                    componentParams.Add("OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged));

                    if (string.IsNullOrEmpty(question.Answer))
                        IsDisabled = true;
                    break;
            }
        }
    }
}