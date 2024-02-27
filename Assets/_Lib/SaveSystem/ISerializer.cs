namespace SaveLoad
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
        T DeSerialize<T>(string data);
    }
}