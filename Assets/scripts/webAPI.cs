using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class webAPI : MonoBehaviour {
	private string baseUrl = "http://localhost:3000";

	public UnityWebRequest CreateApiGetRequest(string actionUrl, object body = null)
	{
		return CreateApiRequest (baseUrl + actionUrl, UnityWebRequest.kHttpVerbGET, body);
	}

	public UnityWebRequest CreateApiPostRequest(string actionUrl, object body = null)
	{
		return CreateApiRequest (baseUrl + actionUrl, UnityWebRequest.kHttpVerbPOST, body);
	}

	UnityWebRequest CreateApiRequest(string url, string method, object body)
	{
		string bodyString = null;
		if (body is string) {
			bodyString = (string)body;
		} else if (body != null) {
			bodyString = JsonUtility.ToJson (body);
		}

		var request = new UnityWebRequest ();
		request.url = url;
		request.method = method;
		request.downloadHandler = new DownloadHandlerBuffer ();
		request.uploadHandler = new UploadHandlerRaw (string.IsNullOrEmpty (bodyString) ? null : Encoding.UTF8.GetBytes (bodyString));
		request.SetRequestHeader ("Accept", "application/json");
		request.SetRequestHeader ("Content-Type", "application/json");
		if (PlayerPrefs.HasKey ("access_token")) {
			string access_token = PlayerPrefs.GetString ("access_token");
			Debug.Log (access_token);
			request.SetRequestHeader ("access_token", access_token);
		}
		request.timeout = 60;
		return request;
	}
}
