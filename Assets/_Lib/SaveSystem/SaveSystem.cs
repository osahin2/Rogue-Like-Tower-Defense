using System.IO;
using UnityEngine;

namespace SaveLoad
{
    public class SaveSystem : ISaveSystem
    {
        private ISerializer _serializer;
        private string _fileExtension;


        private readonly string _saveFolder = Application.persistentDataPath + "/SaveData";

        public SaveSystem(ISerializer serializer)
        {
            _serializer = serializer;
            _fileExtension = "json";
        }
        public void Save<T>(T gameData, bool overwrite = true) where T : GameDataBase
        {
            var fileLocation = PathToFile(gameData.name);
            if (File.Exists(fileLocation) && !overwrite)
            {
                throw new IOException(
                    $"The File {gameData.name}.{_fileExtension} already exists and cannot be overwritten");
            }

            if (!Directory.Exists(_saveFolder))
            {
                Directory.CreateDirectory(_saveFolder);
            }

            File.WriteAllText(fileLocation, _serializer.Serialize(gameData));
        }
        public T Load<T>(string name) where T : GameDataBase
        {
            var fileLocation = PathToFile(name);
            T loadedData = null;

            if (File.Exists(fileLocation))
            {
                loadedData = _serializer.DeSerialize<T>(File.ReadAllText(fileLocation));
            }

            return loadedData;
        }
        public bool TryLoad<T>(string name, out T gameData) where T : GameDataBase
        {
            var fileLocation = PathToFile(name);
            if (File.Exists(fileLocation))
            {
                gameData = _serializer.DeSerialize<T>(File.ReadAllText(fileLocation));
                return true;
            }
            gameData = null;
            return false;
        }
        public void Delete(string profileName)
        {
            var fileLocation = PathToFile(profileName);

            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation);
            }
        }

        private string PathToFile(string profileName) => Path.Combine(_saveFolder,
            string.Concat(profileName, ".", _fileExtension));

    }
}