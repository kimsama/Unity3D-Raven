using UnityEngine;
using System.Collections;

public class ErrorObject : MonoBehaviour 
{
	
	int[] i;
	
	void Start()
	{
		string log = "NullReferenceException: Object reference not set to an instance of an object\nErrorObject.OnGUI () (at Assets/Script/ErrorObject.cs:23)";
		string[] s = log.Split('\n');
		
		Debug.Log (s[0]);
		Debug.Log (s[1]);
	}
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(100, 100, 200, 50), "Error"))
		{
			// force to generator null reference error which will be sent to sentry
			i[0] = 0;
		}
	}
}
