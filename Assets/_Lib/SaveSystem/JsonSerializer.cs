using UnityEngine;

namespace SaveLoad
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj, true);
        }
        public T DeSerialize<T>(string data)
        {
            return JsonUtility.FromJson<T>(data);
        }

    }
}