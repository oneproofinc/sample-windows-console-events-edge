# USB Event Monitoring Console App (Windows)

This sample console application demonstrates how to monitor USB events using a native Windows DLL (`usb_new.dll`) with C#. It captures various USB and BLE events and prints human-readable output to the console.

## 🔧 Requirements

- Windows OS (x64)
- .NET 6.0 or later
- Native DLL: `usb_new.dll` and `edge_windows.dll` must be present in the output (`bin`) directory

## 📁 Setup Instructions (For Customers)

1. **Clone or download this repository.**

2. **Build the project using Visual Studio or the .NET CLI**:
   ```bash
   dotnet build

3. **Place usb_new.dll and edge_windows.dll into the output directory**:
(usually bin\Debug\net6.0 or bin\Release\net6.0).

4. Run the console app:

bash
Copy
Edit
dotnet run
You should see output similar to:

scss
Copy
Edit
[INFO] Monitoring... (5s)
[12:00:01.123] DEVICE CONNECTED: Device XYZ
...
Stopped monitoring.
Press any key to exit...

Project Structure
Program.cs – Entry point of the console application.

UsbEventService.cs – Provides P/Invoke bindings and handles USB monitoring logic.

Key Components
UsbEventService
Handles registration and lifecycle of the USB monitoring session.

Public Methods
csharp
Copy
Edit
public void StartMonitoring(string json, Action<string> onEvent, bool verify, int timeoutSeconds = 30)
json: Configuration string specifying which data elements to request.

onEvent: Callback for event logging (e.g., Console.WriteLine).

verify: Whether to perform data verification.

timeoutSeconds: Duration to monitor before stopping automatically.

Event Types
Code	Event Name
1	DEVICE CONNECTED
2	DEVICE DISCONNECTED
3	QRCODE SCANNED
4	DEVICE HKDF KEY
5	BLE CONNECTED
6	BLE DISCONNECTED
7	BLE SENDING
8	BLE RECEIVING
9	DATA RECEIVED
10	VERIFYING DATA
11	ERROR
other	UNKNOWN

Native Methods (via usb_new.dll)
csharp
Copy
Edit
[DllImport("usb_new.dll")] 
public static extern void register_event_callback(UsbEventCallback callback);

[DllImport("usb_new.dll")] 
public static extern void start_usb_monitoring(byte[] dataPtr, UIntPtr dataLen, bool verify);

[DllImport("usb_new.dll")] 
public static extern void stop_usb_monitoring();

[DllImport("usb_new.dll")] 
public static extern bool is_monitoring_active();
⚠️ Ensure usb_new.dll is compiled for the same architecture (x64) as your .NET runtime.

🧪 Sample JSON Format
The json string passed to StartMonitoring defines the data request schema:

json
Copy
Edit
{
  "org.iso.18013.5.1": {
    "family_name": false,
    "given_name": false,
    "portrait": false,
    "issuing_country": true,
    "birth_date": false,
    "issuing_authority": true
  }
}
Fields with true are requested. Others are ignored.

📄 License
This project is provided as a sample for demonstration purposes. Licensing of usb_new.dll or edge_windows.dll is not covered and should be obtained from the respective provider.

📞 Support
For any issues using or integrating this sample, please contact your technical support representative or system integrator.