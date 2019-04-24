using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;

namespace Alexa.NET
{
    public static class CanFulfillExtensions
    {
        public static SkillResponse CanFulfill(this SkillResponse response, CanFulfillIntent intent)
        {
            response.Response = new CanFulfillResponseBody(response.Response, intent);
            return response;
        }

        public static CanFulfillResponseBody CanFulfill(this ResponseBody body, CanFulfillIntent intent)
        {
            return new CanFulfillResponseBody(body, intent);
        }
    }
}
