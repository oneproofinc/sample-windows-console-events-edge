using System;

class Program
{
    static void Main()
    {
        string json = @"
        {
            ""org.iso.18013.5.1"": {
                ""family_name"": false,
                ""given_name"": false,
                ""portrait"": false,
                ""issuing_country"": true,
                ""birth_date"": false,
                ""issuing_authority"": true
            }
        }";

        var service = new UsbEventService();
        // verify parameter controls whether the DLL performs verification:
        // true = DLL will perform verification
        // false = verification will be skipped
        var verify = true;
        service.StartMonitoring(json, Console.WriteLine, verify, timeoutSeconds: 30);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
