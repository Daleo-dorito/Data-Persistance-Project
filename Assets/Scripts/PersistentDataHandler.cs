using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PersistentDataHandler : MonoBehaviour
{

    public static PersistentDataHandler Instance;

    public string playerName;
    public string highScorePlayerName;
    public int highScore;
    public Boolean firstTime;

    private void Awake()
    {

        if (Instance != null)
        {

            Destroy(gameObject);

        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }


    [System.Serializable]
    class SaveData
    {
        public string highScorePlayerName;

        public int highScore;

        public Boolean firstTime;
    }

    public void SavePlayerScore()
    {
        SaveData data = new SaveData();
        data.highScorePlayerName = highScorePlayerName;
        data.highScore = highScore;
        data.firstTime = firstTime;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScorePlayerName = data.highScorePlayerName;
            highScore = data.highScore;
            firstTime = data.firstTime;

        }
    }
}
