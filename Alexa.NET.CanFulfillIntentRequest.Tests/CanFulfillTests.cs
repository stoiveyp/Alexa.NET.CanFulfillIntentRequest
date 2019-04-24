using System;
using System.IO;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Alexa.NET.CanFulfillIntentRequest.Tests
{
    public class CanFulfillTests
    {
        private const string ExamplesPath = "Examples";
        private const string CanFulfillResponseFile = "CanFulfillResponse.json";
        private const string CanFulfillIntentRequestFile = "CanFulfillIntentRequest.json";

        [Fact]
        public void CanFulfillIntentSerializesCorrectly()
        {
            var fulfillment = CreateIntent();
            Assert.True(CompareJson(fulfillment, CanFulfillResponseFile));
        }

        private CanFulfillIntent CreateIntent()
        {
            var fulfillment = new CanFulfillIntent { CanFulfill = CanFulfill.YES };
            fulfillment.Slots.Add("slotName", new CanfulfillSlot
            {
                CanUnderstand = CanUnderstand.YES,
                CanFulfill = SlotCanFulfill.NO
            });
            return fulfillment;
        }

        [Fact]
        public void CanReadIntentRequestCorrectly()
        {
            CanFulfillIntentRequestConverter.AddToRequestConverter();
            var convertedObj = ExampleFileContent<SkillRequest>(CanFulfillIntentRequestFile);
            Assert.NotNull(convertedObj);
            var intentrequest = Assert.IsType<Request.Type.CanFulfillIntentRequest>(convertedObj.Request);
            Assert.Equal(DialogState.InProgress,intentrequest.DialogState);
            Assert.Equal("PlaySound",intentrequest.Intent.Name);
            var slot = Assert.Single(intentrequest.Intent.Slots);
            Assert.Equal("Sound",slot.Key);
            Assert.Equal("Sound", slot.Value.Name);
            Assert.Equal("crickets",slot.Value.Value);
        }

        [Fact]
        public void DoesResponseBodyExtensionWorkAsExpected()
        {
            var reprompt = new Reprompt("I asked a question");
            var original = ResponseBuilder.Ask("what do you want to do?", reprompt);
            var repackage = original.CanFulfill(CreateIntent());
            Assert.IsType<CanFulfillResponseBody>(repackage.Response);
            Assert.Equal(original.Response.OutputSpeech,repackage.Response.OutputSpeech);
            Assert.Equal(original.Response.Reprompt,repackage.Response.Reprompt);

            var responseBody = JObject.FromObject(repackage.Response)["canFulfillIntent"];
            Assert.True(CompareJson(responseBody, CanFulfillResponseFile));
        }

        public static T ExampleFileContent<T>(string expectedFile)
        {
            using (var reader = new JsonTextReader(new StringReader(ExampleFileContent(expectedFile))))
            {
                return new JsonSerializer().Deserialize<T>(reader);
            }
        }

        public static string ExampleFileContent(string expectedFile)
        {
            return File.ReadAllText(Path.Combine(ExamplesPath, expectedFile));
        }

        private static bool CompareJson(object actual, string expectedFile)
        {
            var actualJObject = JObject.FromObject(actual);
            var expected = File.ReadAllText(Path.Combine(ExamplesPath, expectedFile));
            var expectedJObject = JObject.Parse(expected);
            Console.WriteLine(actualJObject);
            return JToken.DeepEquals(expectedJObject, actualJObject);
        }
    }
}
