using System.Linq;

namespace Alexa.NET.Request.Type
{
    public class CanFulfillIntentRequestConverter : IRequestTypeConverter
    {
        public bool CanConvert(string requestType)
        {
            return requestType == "CanFulfillIntentRequest";
        }

        public Request Convert(string requestType)
        {
            return new CanFulfillIntentRequest();
        }

        public static void AddToRequestConverter()
        {
            if (RequestConverter.RequestConverters.Where(rc => rc != null)
                .All(rc => rc.GetType() != typeof(CanFulfillIntentRequestConverter)))
            {
                RequestConverter.RequestConverters.Add(new CanFulfillIntentRequestConverter());
            }
        }
    }
}