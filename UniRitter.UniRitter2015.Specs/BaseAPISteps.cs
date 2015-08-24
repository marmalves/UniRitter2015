using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services.Implementation;
using UniRitter.UniRitter2015.Support;

namespace UniRitter.UniRitter2015.Specs
{
    public class BaseAPISteps
    {
        protected readonly HttpClient client;

        public BaseAPISteps()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49556/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected void CheckCode(int code)
        {
            Assert.That(CommonAPISteps.response.StatusCode, Is.EqualTo((HttpStatusCode)code));
        }


    }
}