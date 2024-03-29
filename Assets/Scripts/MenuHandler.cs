using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public TextMeshProUGUI playerNameInput;

    public void StartGame()
    {
        
        PersistentDataHandler.Instance.firstTime = true;
        SceneManager.LoadScene(1);

    }

    public void SetPlayerName ()
    {
        if (string.IsNullOrWhiteSpace(playerNameInput.text)) { playerNameInput.text = "PLAYER"; }

        PersistentDataHandler.Instance.playerName = playerNameInput.text;

    }
}
