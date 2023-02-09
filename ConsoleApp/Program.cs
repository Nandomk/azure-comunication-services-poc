// See https://aka.ms/new-console-template for more information
using Azure;
using Azure.Communication.Sms;


var connectionString = "<connection_string>"; // Find your Communication Services resource in the Azure portal
SmsClient client = new SmsClient(connectionString);



try
{
    var response = await client.SendAsync(
        from: "<from-phone-number>" // Your E.164 formatted phone number used to send SMS
        to: new string[] { "<to-phone-number-1>", "<to-phone-number-2>" }, // E.164 formatted recipient phone number
        message: "Weekly Promotion!",
        options: new SmsSendOptions(enableDeliveryReport: true) // OPTIONAL
        {
            Tag = "marketing", // custom tags
        });
    foreach (SmsSendResult result in response.Value)
    {
        if (result.Successful)
        {
            Console.WriteLine($"Successfully sent this message: {result.MessageId} to {result.To}.");
        }
        else
        {
            Console.WriteLine($"Something went wrong when trying to send this message {result.MessageId} to {result.To}.");
            Console.WriteLine($"Status code {result.HttpStatusCode} and error message {result.ErrorMessage}.");
        }
    }
}
catch (RequestFailedException ex)
{
    Console.WriteLine(ex.Message);
}