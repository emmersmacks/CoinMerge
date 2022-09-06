using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CodeBase.Infrastructure.Services;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkController
{
    private const string ServerURL = "https://CoinMerge.somee.com/api/Score";
    private const string ServerURLGet = "https://CoinMerge.somee.com/api/GetScore";
    private const string ServerURLGetImage = "https://CoinMerge.somee.com/api/GetImage";
    private const string RequestHeaderName = "Content-Type";
    private const string ContentType = "application/json";
    
    public IEnumerator SendUserScoreToServer(UserData dataToJson)
    {
        var json = JsonUtility.ToJson(dataToJson);
        var request = new UnityWebRequest(ServerURL, "POST");
        yield return SendRequest(json, request);
    }

    public IEnumerator GetScoreListFromServer(UserData dataToJson, Action<List<UserData>> OnLoaded)
    {
        Debug.Log("Send REQUEST");
        var json = JsonUtility.ToJson(dataToJson);
        Debug.Log(json);

        var request = new UnityWebRequest(ServerURLGet, "POST");
        yield return SendRequest(json, request);

        var responseJson = request.downloadHandler.text;
        Debug.Log("RESPONSE!!! ________________");

        Debug.Log(responseJson);
        Debug.Log(request.error);
        var usersList = ParseJsonList(responseJson);
        OnLoaded(usersList);
        //yield return request; //SendRequest(json, request);


    }

    private List<UserData> ParseJsonList(string responseJson)
    {
        var testString = responseJson.Replace("},{", "}@{");
        testString = testString.Replace("[", "");
        testString = testString.Replace("]", "");
        var testList = testString.Split('@');
        
        var scoreList = new List<UserData>();
        foreach (var userData in testList)
        {
            var user = JsonUtility.FromJson<UserData>(userData);
            scoreList.Add(user);
        }
        return scoreList;
    }

    public IEnumerator SendRequest(string data, UnityWebRequest unityWebRequest)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
        unityWebRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        unityWebRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        unityWebRequest.SetRequestHeader(RequestHeaderName, ContentType);
        unityWebRequest.chunkedTransfer = false;
        yield return unityWebRequest.SendWebRequest();
    }
    
    public IEnumerator DownloadImage(string MediaUrl, RawImage image)
    {
        if (MediaUrl != "")
        {
            var request = UnityWebRequestTexture.GetTexture(ServerURLGetImage+string.Format("?link={0}", MediaUrl));

            yield return request.SendWebRequest();
            yield return new WaitForSeconds(1);

            Debug.Log(request.result);
            
            if(request.isNetworkError || request.isHttpError) 
                Debug.Log(request.error);
            else
                image.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
        
    }
}