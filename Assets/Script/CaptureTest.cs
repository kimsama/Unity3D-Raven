using UnityEngine;
using System.Collections;
using SharpRaven;
using SharpRaven.Logging;
using System;

public class CaptureTest : MonoBehaviour {
	
	//const string dsnUrl = "add-your-sentry-dns-here";
	const string dsnUrl = "http://ef96c1bff6194467bfd566bef1506d42:012cd70cf2f7447a973ea018219a6850@ser4kakao.gameus.co.kr:9000/2";	
    static SharpRaven.RavenClient ravenClient;
	

	void Start () 
	{	
		setup();
        testWithStacktrace();
        testWithoutStacktrace();
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

	
    static void testWithoutStacktrace()
    {
            Debug.Log("Send exception without stacktrace.");
            var id = ravenClient.CaptureException(new Exception("Test without a stacktrace."));
            Debug.Log("Sent packet: " + id);
    }

    static void testWithStacktrace()
    {
        Debug.Log("Causing division by zero exception.");
        try
        {
            PerformDivideByZero();
        }
        catch (Exception e)
        {
            Debug.Log("Captured: " + e.Message);
            var id = ravenClient.CaptureException(e);
            Debug.Log("Sent packet: " + id);
        }
    }

    static void PerformDivideByZero()
    {
        int i2 = 0;
        int i = 10 / i2;
    }
	
}
