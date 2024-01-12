using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScore : MonoBehaviour
{

    public Text highScoreText;


    public void UpdateHighScoreText()
    {

        highScoreText.text = "High Score: " + PersistentDataHandler.Instance.highScore + " by " + PersistentDataHandler.Instance.highScorePlayerName;

    }
}
