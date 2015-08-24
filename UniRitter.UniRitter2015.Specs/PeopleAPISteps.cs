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
using UniRitter.UniRitter2015.Specs.Models;

namespace UniRitter.UniRitter2015.Specs
{
    [Binding]
    public class PeopleAPISteps : BaseAPISteps
    {


        [When(@"I post it to the /people API endpoint")]
        public void WhenIPostItToThePeopleAPIEndpoint()
        {
            CommonAPISteps.response = client.PostAsJsonAsync("people", (Person)CommonAPISteps.postedData).Result;
        }

        [Given(@"a person resource as described below:")]
        public void GivenAPersonResourceAsDescribedBelow(Table table)
        {
            CommonAPISteps.postedData = new Person();
            table.FillInstance((Person)CommonAPISteps.postedData);
        }


        [Then(@"I receive a message listing all validation errors")]
        public void ThenIReceiveAMessageListingAllValidationErrors()
        {
            var validationMessage = CommonAPISteps.response.Content.ReadAsStringAsync().Result;
            Assert.That(validationMessage, Contains.Substring("firstName"));
            Assert.That(validationMessage, Contains.Substring("email"));
        }

        [Given(@"an API populated with the following people")]
        public void GivenAnAPIPopulatedWithTheFollowingPeople(Table table)
        {
            CommonAPISteps.backgroundData = table.CreateSet<Person>();
            //var mongoRepo = new MongoRepository<PersonModel>(new ApiConfig());
            //mongoRepo.Upsert(table.CreateSet<PersonModel>());
            var repo = new InMemoryRepository<PersonModel>();
            foreach (var entry in table.CreateSet<PersonModel>())
            {
                repo.Add(entry);
            }
        }

        [When(@"I post the following data to the /people API endpoint: (.+)")]
        public void WhenIPostTheFollowingDataToThePeopleAPIEndpoint(string jsonData)
        {
            CommonAPISteps.postedData = JsonConvert.DeserializeObject<Person>(jsonData);
            CommonAPISteps.response = client.PostAsJsonAsync("people", (Person)CommonAPISteps.postedData).Result;
        }

    }
}