using System;
using UnityEngine;

public interface ISerializationOption
{
    T Deserialize<T>(string text);
}

public class JsonSerializationOption : ISerializationOption {
    public T Deserialize<T>(string text) {
        try {
            var result = JsonUtility.FromJson<T>(text);
            return result;
        }
        catch (Exception ex) {
            Debug.LogError($"{this} Could not parse json {text}. {ex.Message}");
            return default;
        }
    }
}