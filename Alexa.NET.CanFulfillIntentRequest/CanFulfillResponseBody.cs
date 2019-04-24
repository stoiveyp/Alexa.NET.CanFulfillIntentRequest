using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;
using Newtonsoft.Json;

namespace Alexa.NET
{
    public class CanFulfillResponseBody:ResponseBody
    {
        public CanFulfillResponseBody()
        {
            
        }

        public CanFulfillResponseBody(CanFulfillIntent canFulfillIntent)
        {
            CanFulfillIntent = canFulfillIntent;
        }

        public CanFulfillResponseBody(ResponseBody body, CanFulfillIntent canFulfillIntent):this(canFulfillIntent)
        {
            Card = body.Card;
            Directives = body.Directives;
            OutputSpeech = body.OutputSpeech;
            Reprompt = body.Reprompt;
            ShouldEndSession = body.ShouldEndSession;
        }

        [JsonProperty("canFulfillIntent", NullValueHandling = NullValueHandling.Ignore)]
        public CanFulfillIntent CanFulfillIntent { get; set; }
    }
}
