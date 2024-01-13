using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public DisplayHighScore displayHighScore;

    private AudioSource audioSource;
    public AudioClip startSound;
    public AudioClip endSound;
    public Boolean endSoundPlayed;

    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {

        GenerateBricks();
        audioSource = GetComponent<AudioSource>();

        if (PersistentDataHandler.Instance != null) 
        { 

            PersistentDataHandler.Instance.LoadPlayerScore();
            if (string.IsNullOrWhiteSpace(PersistentDataHandler.Instance.playerName)) { PersistentDataHandler.Instance.playerName = "PLAYER"; }
            if (PersistentDataHandler.Instance.firstTime) { audioSource.PlayOneShot(startSound); }

        }
        displayHighScore.UpdateHighScoreText();   
        
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }

            endSoundPlayed = false;

        }
        else if (m_GameOver)
        {

            PersistentDataHandler.Instance.firstTime = false;

            if (!endSoundPlayed)
            {

                audioSource.PlayOneShot(endSound, 2);
                endSoundPlayed = true;

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (PersistentDataHandler.Instance != null)
                {


                    if (m_Points >= PersistentDataHandler.Instance.highScore)
                    {

                        PersistentDataHandler.Instance.highScorePlayerName = PersistentDataHandler.Instance.playerName;
                        PersistentDataHandler.Instance.highScore = m_Points;
                        PersistentDataHandler.Instance.SavePlayerScore();

                    }

                }          
                              
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (FindObjectsOfType<Brick>().Length == 0)
        {

            GenerateBricks();
            FindObjectOfType<Ball>().maxSpeed += FindObjectOfType<Ball>().maxSpeed * 0.2f;

        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void GenerateBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

    }

    private void OnApplicationFocus(bool focus)
    {

        if (!focus)
        {

            pausePanel.SetActive(true);
            Time.timeScale = 0f;

        }
        else
        {

            pausePanel.SetActive(false);
            Time.timeScale = 1f;

        }

    }

}
