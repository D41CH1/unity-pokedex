using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HttpClient
{
    private readonly ISerializationOption _serializationOption;

    public HttpClient(ISerializationOption serializationOption) {
        _serializationOption = serializationOption;
    }

    public async Task<T> Get<T>(string url) {
        try {
            UnityWebRequest www = UnityWebRequest.Get(url);

            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}");

            return _serializationOption.Deserialize<T>(www.downloadHandler.text); ;
        }
        catch (Exception ex) {
            Debug.LogError($"{nameof(Get)} failed: {ex.Message}");

            return default;
        }
    }

    public async Task<Texture> GetTexture(string url) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.LogError($"Failed: {www.error}");

        return DownloadHandlerTexture.GetContent(www);
    }
}
