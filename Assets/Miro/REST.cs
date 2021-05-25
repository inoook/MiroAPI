using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// https://github.com/Kubos-cz/Unity-WebRequest-Example
public class REST
{
    //string authenticate(string username, string password)
    //{
    //    string auth = username + ":" + password;
    //    Debug.Log("auth: " + auth);
    //    auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
    //    auth = "Basic " + auth;
    //    return auth;
    //}

    public IEnumerator Post(string url, string authorization, string jsonStr)
    {
        var request = new UnityWebRequest();
        request.url = url;
        byte[] body = Encoding.UTF8.GetBytes(jsonStr);
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", authorization);
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");

        request.method = UnityWebRequest.kHttpVerbPOST;
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 201)
            {
                Debug.Log("success");
            }
            else
            {
                Debug.Log("failed: " + request.responseCode);
            }

            Debug.Log(request.downloadHandler.text);
        }
    }

    // 変更分を送信
    public IEnumerator Patch(string url, string authorization, string jsonStr)
    {
        var request = new UnityWebRequest();
        request.url = url;
        byte[] body = Encoding.UTF8.GetBytes(jsonStr);
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", authorization);
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        request.method = "PATCH";

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 201)
            {
                Debug.Log("success");
            }
            else
            {
                Debug.Log("failed: " + request.responseCode);
            }

            Debug.Log(request.downloadHandler.text);
        }
    }

    //
    public IEnumerator Delete(string url, string authorization)
    {
        var request = new UnityWebRequest();
        request.url = url;
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", authorization);
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        request.method = "DELETE";

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 204)
            {
                Debug.Log("No Content");
            }
            else
            {
                Debug.Log("failed: " + request.responseCode);
            }

            Debug.Log(request.downloadHandler.text);
        }
    }

    public IEnumerator Get(string url, string authorization, System.Action<UnityWebRequest> successAct)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", authorization);
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        request.method = UnityWebRequest.kHttpVerbGET;
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogWarning(request.error + " / " + url);
        }
        else
        {
            successAct?.Invoke(request);

            //  または、結果をバイナリデータとして取得します
            byte[] results = request.downloadHandler.data;
        }
    }

    public IEnumerator GetImage(string url, System.Action<UnityWebRequest> successAct)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        request.method = UnityWebRequest.kHttpVerbGET;
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogWarning(request.error + " / " + url);
        }
        else
        {
            successAct?.Invoke(request);

            //  または、結果をバイナリデータとして取得します
            //byte[] results = request.downloadHandler.data;
        }
    }

}
