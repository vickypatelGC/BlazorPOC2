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
        int questionIndex = 1;
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
                LoadCompenent(questions[questionIndex - 1]);
            }
        }

        public void OnSelectOneChanged(ChangeEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value?.ToString()))
            {
                question.Answer = e.Value?.ToString();
                OnNext();
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

        public void OnNext()
        {
            int nextIndex = questionIndex + 1;

            if (nextIndex == 0)
                isDisplayBack = false;
            else
                isDisplayBack = true;

            if (nextIndex <= questions.Count)
            {
                questionIndex++;
                LoadCompenent(questions[questionIndex - 1]);
            }
            else
                isDisplayNext = false;
        }

        public void OnBack()
        {
            int nextIndex = questionIndex - 1;

            if (nextIndex <= 1)
                isDisplayBack = false;
            else
                isDisplayBack = true;

            if (nextIndex != 0)
            {
                questionIndex--;
                LoadCompenent(questions[questionIndex - 1]);
            }
            else
                isDisplayNext = false;
        }

        public void LoadCompenent(Question que)
        {

            question = que;
            componentParams = new Dictionary<string, object>()
            {{"question", que}, {"OnSelectOneChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnSelectOneChanged)}};
            switch (que.Type)
            {
                case QuestionType.SelectOne:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.SelectOne);
                    componentParams = new Dictionary<string, object>()
                    {{"question", que}, {"OnSelectOneChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnSelectOneChanged)}};
                    isDisplayNext = false;
                    break;
                case QuestionType.MultiCheckBox:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.MultiCheckBox);
                    componentParams = new Dictionary<string, object>()
                    {{"question", que}};
                    isDisplayNext = true;
                    //if (string.IsNullOrEmpty(que.Answer))
                    IsDisabled = false;
                    break;
                case QuestionType.CheckBox:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.CheckBox);
                    componentParams = new Dictionary<string, object>()
                    {{"question", que}};
                    IsDisabled = false;
                    break;
                case QuestionType.Text:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.TextBox);
                    componentParams = new Dictionary<string, object>()
                    {{"question", que}, {"OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged)}};
                    isDisplayNext = true;
                    if (string.IsNullOrEmpty(que.Answer))
                        IsDisabled = true;
                    break;
                case QuestionType.Textarea:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.TextArea);
                    componentParams = new Dictionary<string, object>()
                    {{"question", que}, {"OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged)}};
                    if (string.IsNullOrEmpty(que.Answer))
                        IsDisabled = true;
                    isDisplayNext = true;
                    break;
                case QuestionType.DropDown:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.DropDownList);
                    componentParams = new Dictionary<string, object>()
                    {{"question", que}, {"OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged)}};
                    if (string.IsNullOrEmpty(que.Answer))
                        IsDisabled = true;
                    isDisplayNext = true;
                    break;
                case QuestionType.Date:
                    componentType = typeof(BlazorPOC2.Client.Pages.Components.DatePicker);
                    componentParams = new Dictionary<string, object>()
                    {{"question", que}, {"OnInputTextChanged", EventCallback.Factory.Create<ChangeEventArgs>(this, OnInputTextChanged)}};
                    if (string.IsNullOrEmpty(que.Answer))
                        IsDisabled = true;
                    isDisplayNext = true;
                    break;
            }

            if (questionIndex == questions.Count)
            {
                isDisplayNext = false;
                isDisplayCountinue = true;
            }
            else
            {
                if (componentType == typeof(BlazorPOC2.Client.Pages.Components.SelectOne))
                {
                    if (!string.IsNullOrEmpty(que.Answer))
                        isDisplayNext = true;
                }
                else
                    isDisplayNext = true;
                isDisplayCountinue = false;
            }
        }
    }
}