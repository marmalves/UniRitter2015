using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UniRitter.UniRitter2015.Specs.Models;

namespace UniRitter.UniRitter2015.Specs
{
    [Binding]
    public class CommonAPISteps : BaseAPISteps
    {
        public static string path;
        public static HttpResponseMessage response;
        public static IModel result;
        public static IModel postedData;
        public static Type postType;
        public static IEnumerable<IModel> backgroundData;


        [Given(@"a ""(.*)"" resource")]
        public void GivenAResource(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"(.+) resource")]
        public void GivenAnInvalidResource(string resourceCase)
        {
            // step purposefully left blank
        }

        [Then(@"I receive an error \(code (.*)\) return message")]
        public void ThenIReceiveAnErrorCodeReturnMessage(int code)
        {
            CheckCode(code);
        }


        [Given(@"the populated API")]
        public void GivenThePopulatedAPI()
        {
            // This step has been left blank -- data seeding occurs in the backgorund step
        }

        [When(@"I GET from the /(.+) API endpoint")]
        public void WhenIGETFromTheAPIEndpoint(string path)
        {
            CommonAPISteps.path = path;
            CommonAPISteps.response = client.GetAsync(path).Result;
        }

        [Then(@"I get a list containing the populated resources")]
        public void ThenIGetAListContainingThePopulatedResources()
        {
            IEnumerable<IModel> resourceList = null;
            if (path.Contains("people"))
                resourceList = CommonAPISteps.response.Content.ReadAsAsync<IEnumerable<Person>>().Result;
            else
                resourceList = CommonAPISteps.response.Content.ReadAsAsync<IEnumerable<Post>>().Result;
            Assert.That(CommonAPISteps.backgroundData, Is.SubsetOf(resourceList));
        }

        [Then(@"I receive a success \(code (.*)\) return message")]
        public void ThenIReceiveASuccessCodeReturnMessage(int code)
        {
            if (!CommonAPISteps.response.IsSuccessStatusCode)
            {
                var msg = String.Format("API error: {0}", CommonAPISteps.response.Content.ReadAsStringAsync().Result);
                Assert.Fail(msg);
            }

            CheckCode(code);
        }

        [Then(@"I receive the posted resource")]
        public void ThenIReceiveThePostedResource()
        {
            if (CommonAPISteps.postedData.GetType() == typeof(Person))
            {
                CommonAPISteps.result = CommonAPISteps.response.Content.ReadAsAsync<Person>().Result;
            }
            else if (CommonAPISteps.postedData.GetType() == typeof(Post))
            {
                CommonAPISteps.result = CommonAPISteps.response.Content.ReadAsAsync<Post>().Result;
            }
            Assert.That(CommonAPISteps.postedData.AttributeEquals(CommonAPISteps.result));
        }

        [Then(@"the posted resource now has an ID")]
        public void ThenThePostedResourceNowHasAnID()
        {
            Assert.That(result.GetHashCode(), Is.Not.Null);
        }

        [Then(@"the data matches that id")]
        public void ThenIGetThePersonRecordThatMatchesThatId()
        {
            var id = new Guid(path.Substring(path.LastIndexOf('/') + 1));

            if (path.Contains("people"))
                CommonAPISteps.result = CommonAPISteps.response.Content.ReadAsAsync<Person>().Result;
            else if (path.Contains("posts"))
                CommonAPISteps.result = CommonAPISteps.response.Content.ReadAsAsync<Post>().Result;

            var expected = CommonAPISteps.backgroundData.Single(p => p.GetId() == CommonAPISteps.result.GetId());
            Assert.That(CommonAPISteps.result, Is.EqualTo(expected));
        }

        [Then(@"I can fetch it from the API")]
        public void ThenICanFetchItFromTheAPI()
        {
            Guid id = CommonAPISteps.result.GetId().Value;

            string controller = "people/";
            if (CommonAPISteps.result.GetType() == typeof(Post))
                controller = "posts/";

            var newEntry = client.GetAsync(controller + id).Result;
            Assert.That(newEntry, Is.Not.Null);
        }

        [Then(@"I receive a message that conforms (.+)")]
        public void ThenIReceiveAMessageThatConforms(string pattern)
        {
            var msg = CommonAPISteps.response.Content.ReadAsStringAsync().Result;
            StringAssert.IsMatch(pattern, msg);
        }

    }
}