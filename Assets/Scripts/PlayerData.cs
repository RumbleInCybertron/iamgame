using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public int levelIndex;
    public int lives;
    public int score;

    private static string savePath = Application.persistentDataPath + "/playerData.fun";
    private static BinaryFormatter formatter = new BinaryFormatter();

    private static PlayerData freshInstance = new PlayerData(1, 3, 0);

    public PlayerData(int levelIndex, int lives, int score)
    {
        this.levelIndex = levelIndex;
        this.lives = lives;
        this.score = score;
    }

    public static void SavePlayerData(int levelIndex, int lives, int score)
    {
        FileStream stream = new FileStream(savePath, FileMode.Create);
        PlayerData data = new PlayerData(levelIndex, lives, score);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static bool LoadPlayerData()
    {
        if (File.Exists(savePath))
        {
            FileStream stream = new FileStream(savePath, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            GameSession.LoadGame(data);
            return true;
        }
        else { Debug.Log("Save data not found."); return false; }
    }

    public static void EraseSaveFile()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);
    }
}