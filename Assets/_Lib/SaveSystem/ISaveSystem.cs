namespace SaveLoad
{
    public interface ISaveSystem
    {
        void Save<T>(T gameData, bool overwrite = true) where T : GameDataBase;
        T Load<T>(string name) where T : GameDataBase;
        bool TryLoad<T>(string name, out T gameData) where T : GameDataBase;
        void Delete(string name);

    }
}