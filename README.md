# ONEPROOF Edge USB Event Monitoring Console App (Windows)

This sample console application demonstrates how to integrate with ONEPROOF Edge devices using our native Windows DLL (`edge_windows.dll`). It provides a reference implementation for monitoring USB and BLE events from ONEPROOF Edge devices, with detailed event logging and data verification capabilities.

## What is ONEPROOF?

ONEPROOF provides comprehensive solutions for ISO 18013-5 and ISO 18013-7 compliant Mobile Driver's License (mDL) and National ID verification. Our product suite includes:

### Core Products
- **Mobile SDK**: Native libraries for iOS and Android to integrate mDL verification into mobile applications for ISO 18013-5 compliance
- **Server Deployments**: Scalable server solutions for enterprise-grade identity verification
- **APIs**: RESTful APIs for seamless integration with existing systems
- **Container Images**: Docker containers for easy deployment in cloud environments with ISO 18013-7 compliance
- **Edge Device**: Embedded hardware verification device for secure mDL and National ID scanning
- **Custom Solutions**: Tailored hardware and software solutions for specific use cases

### Key Features
- **ISO Compliance**: Full support for ISO 18013-5 and ISO 18013-7 standards
- **Multi-Platform Support**: Solutions for mobile, desktop, and server environments
- **Secure Verification**: End-to-end encrypted verification process
- **Custom Firmware**: Specialized firmware for specific verification requirements
- **Hardware Solutions**: Dedicated verification devices and custom hardware options
- **Integration Support**: Comprehensive documentation and developer tools

### Use Cases
- Mobile Driver's License verification
- National ID verification
- Age verification
- Identity authentication
- Banking & Finance
- Retail
- Access control
- Compliance reporting

Learn more about our complete product suite at [https://oneproof.com](https://oneproof.com)

## 🔧 Requirements

- Windows OS (x64)
- .NET 6.0 or later
- ONEPROOF Edge device
- Native DLLs: `edge_windows.dll` (contact ONEPROOF for access)

## 🚀 Getting Started (For Developers)

1. **Clone this repository**

2. **Build the project**:
   ```bash
   dotnet build
   ```

3. **Add the required DLLs**:
   - Place `edge_windows.dll` and `edge_windows.dll` in the output directory
   - Default location: `bin\Debug\net6.0` or `bin\Release\net6.0`

4. **Run the sample app**:
   ```bash
   dotnet run
   ```

## 📁 Project Structure

- `Program.cs` – Main entry point demonstrating basic usage
- `UsbEventService.cs` – Core service class with P/Invoke bindings and USB monitoring logic

## 💻 Integration Guide

### 1. Initialize the Service

```csharp
var usbService = new UsbEventService();
```

### 2. Start Monitoring

```csharp
usbService.StartMonitoring(
    json: "{ \"org.iso.18013.5.1\": { \"issuing_country\": true } }",
    onEvent: (eventData) => Console.WriteLine(eventData),
    verify: true,
    timeoutSeconds: 30
);
```

### 3. Handle Events

The service supports the following event types:

| Code | Event Name | Description |
|------|------------|-------------|
| 1 | DEVICE CONNECTED | ONEPROOF Edge device connected |
| 2 | DEVICE DISCONNECTED | ONEPROOF Edge device disconnected |
| 3 | QRCODE SCANNED | QR code scanned from device |
| 4 | DEVICE HKDF KEY | Device key received |
| 5 | BLE CONNECTED | BLE connection established |
| 6 | BLE DISCONNECTED | BLE connection terminated |
| 7 | BLE SENDING | Data being sent via BLE |
| 8 | BLE RECEIVING | Data being received via BLE |
| 9 | DATA RECEIVED | Data successfully received |
| 10 | VERIFYING DATA | Data verification in progress |
| 11 | ERROR | Error occurred during operation |

## 🔌 Native Integration

The sample uses the following native methods from `edge_windows.dll`:

```csharp
[DllImport("edge_windows.dll")] 
public static extern void register_event_callback(UsbEventCallback callback);

[DllImport("edge_windows.dll")] 
public static extern void start_usb_monitoring(byte[] dataPtr, UIntPtr dataLen, bool verify);

[DllImport("edge_windows.dll")] 
public static extern void stop_usb_monitoring();

[DllImport("edge_windows.dll")] 
public static extern bool is_monitoring_active();
```

## 📝 Data Request Format

The JSON configuration specifies which data elements to request from the ONEPROOF Edge device:

```json
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
```

## ⚠️ Important Notes

- Ensure `edge_windows.dll` is compiled for x64 architecture
- The ONEPROOF Edge device must be connected via USB
- Proper error handling should be implemented in production code
- Contact ONEPROOF for licensing and support

## 📞 Support

For integration support or to obtain the required DLLs, please contact ONEPROOF technical support.