using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SharpRaven;

public class ErrorReporter : MonoBehaviour {
	
	const string dsnUrl = "http://ef96c1bff6194467bfd566bef1506d42:012cd70cf2f7447a973ea018219a6850@ser4kakao.gameus.co.kr:9000/2";	
    static SharpRaven.RavenClient ravenClient;
	
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		
		setup();
		
		Application.RegisterLogCallback(HandleLog);
	}
	
    static void setup()
    {
        Debug.Log("Initializing RavenClient.");
        ravenClient = new RavenClient(dsnUrl);
        ravenClient.Logger = "C#";
        ravenClient.LogScrubber = new SharpRaven.Logging.LogScrubber();

        Debug.Log("Sentry Uri: " + ravenClient.CurrentDSN.SentryURI);
        Debug.Log("Port: " + ravenClient.CurrentDSN.Port);
        Debug.Log("Public Key: " + ravenClient.CurrentDSN.PublicKey);
        Debug.Log("Private Key: " + ravenClient.CurrentDSN.PrivateKey);
        Debug.Log("Project ID: " + ravenClient.CurrentDSN.ProjectID);
    }
	
	private void HandleLog(string log, string stack, LogType type)
	{
		Dictionary<string, string> dic = new Dictionary<string, string>();
		
		//dic.Add("log", log);
		//dic.Add("stack", stack);
		dic.Add("type", type.ToString());
		dic.Add("playTime", Time.realtimeSinceStartup.ToString());
		dic.Add("time", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
		
		dic.Add("ProcessorType", SystemInfo.processorType);
		dic.Add("ProcessorCount", SystemInfo.processorCount.ToString());
		
		dic.Add("Device-Uid", SystemInfo.deviceUniqueIdentifier);
		dic.Add("Device-Model", SystemInfo.deviceModel);
		dic.Add("Device-Name", SystemInfo.deviceName);
		dic.Add("OS", SystemInfo.operatingSystem);
		dic.Add("MemorySize", SystemInfo.systemMemorySize.ToString());		
		
		dic.Add("GPU-Memory", SystemInfo.graphicsMemorySize.ToString());
		dic.Add("GPU-Name", SystemInfo.graphicsDeviceName);
		dic.Add("GPU-Vendor", SystemInfo.graphicsDeviceVendor);
		dic.Add("GPU-VendorID", SystemInfo.graphicsDeviceVendorID.ToString());
		dic.Add("GPU-id", SystemInfo.graphicsDeviceID.ToString());
		dic.Add("GPU-Version", SystemInfo.graphicsDeviceVersion);
		dic.Add("GPU-ShaderLevel", SystemInfo.graphicsShaderLevel.ToString());
		dic.Add("GPU-PixelFillrate", SystemInfo.graphicsPixelFillrate.ToString());		
		
		//ravenClient.CaptureMessage(log, SharpRaven.Data.ErrorLevel.error, dic, null);
		
		ravenClient.CaptureUntiyLog(log, stack, type, dic, null);
		
	}	
}
