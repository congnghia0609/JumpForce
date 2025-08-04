using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    private string pathDB;
    public GameData gameData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Khởi tạo giá trị mặc định nếu muốn.
            pathDB = Application.persistentDataPath + "/gamedata.json";
            gameData = Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(pathDB, json);
    }

    public GameData Load()
    {
        if (File.Exists(pathDB))
        {
            string json = File.ReadAllText(pathDB);
            return JsonUtility.FromJson<GameData>(json);
        }
        return new GameData(); // Trả về mặc định nếu chưa có file
    }
}

[System.Serializable]
public class GameData
{
    public int BestScore;
}
