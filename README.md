# Alexa.NET.CanFulfillIntentRequest
Package that allows Alexa skills to take part in the CanFulfillIntentRequest Beta

## Extending Requests to pick up CanFulfill
```
CanFulfillIntentRequestConverter.AddToRequestConverter();
...
if (convertedObj.Request is Request.Type.CanFulfillIntentRequest)
{
  ...
}
```

## Responding to CanFull requests
```
var skillResponse = ResponseBuilder.Ask(...);
return skillResponse.CanFulfill(canFulfillIntent);
````