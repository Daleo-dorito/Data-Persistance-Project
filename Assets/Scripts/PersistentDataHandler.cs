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

        }
    }

    [ContextMenu("Reset All Data")]
    public void DeleteAllData()
    {

        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {

            Debug.Log("Data reseted");

            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);


            highScorePlayerName = "PLAYER";

            highScore = 0;

            firstTime = false;

            SavePlayerScore();

            LoadPlayerScore();

            if (FindObjectOfType<MainManager>() != null)
            {

                FindObjectOfType<MainManager>().displayHighScore.UpdateHighScoreText();

            }

        }

    }
}
