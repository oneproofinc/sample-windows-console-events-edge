using System;
using System.Runtime.InteropServices;
using System.Text;

public class UsbEventService
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UsbEventCallback(int eventType, IntPtr timeStamp, IntPtr message);

    [DllImport("usb_new.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void register_event_callback(UsbEventCallback callback);

    [DllImport("usb_new.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void start_usb_monitoring(byte[] dataPtr, UIntPtr dataLen, bool verify);

    [DllImport("usb_new.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void stop_usb_monitoring();

    [DllImport("usb_new.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool is_monitoring_active();

    private UsbEventCallback? _callback;

    public void StartMonitoring(string json, Action<string> onEvent, bool verify, int timeoutSeconds = 30)
    {
        _callback = new UsbEventCallback((int type, IntPtr ts, IntPtr msgPtr) =>
        {
            string msg = Marshal.PtrToStringAnsi(msgPtr) ?? "Unknown";
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            string name = GetEventTypeName(type);
            onEvent?.Invoke($"[{time}] {name}: {msg}");
        });

        register_event_callback(_callback);
        byte[] data = Encoding.UTF8.GetBytes(json);
        start_usb_monitoring(data, (UIntPtr)data.Length, verify);

        int elapsed = 0;
        while (is_monitoring_active() && elapsed < timeoutSeconds)
        {
            Thread.Sleep(1000);
            elapsed++;
            if (elapsed % 5 == 0)
                onEvent?.Invoke($"[INFO] Monitoring... ({elapsed}s)");
        }

        stop_usb_monitoring();
        onEvent?.Invoke("Stopped monitoring.");
    }

    private string GetEventTypeName(int eventType)
    {
        return eventType switch
        {
            1 => "DEVICE CONNECTED", // Working
            2 => "DEVICE DISCONNECTED",
            3 => "QRCODE SCANNED",
            4 => "DEVICE HKDF KEY",
            5 => "BLE CONNECTED", // Working
            6 => "BLE DISCONNECTED",
            7 => "BLE SENDING",
            8 => "BLE RECEIVING",
            9 => "DATA RECEIVED", // Working
            10 => "VERIFYING DATA",
            11 => "ERROR",
            _ => "UNKNOWN"
        };
    }
}
