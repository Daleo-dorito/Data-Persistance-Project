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
        
        SceneManager.LoadScene(1);

    }

    public void SetPlayerName ()
    {

        PersistentDataHandler.Instance.playerName = playerNameInput.text;

    }
}
