using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject explanationPanel;
    public GameObject startButton;
    public GameObject confirmButton;

    public void ShowGameExplanation()
    {
        if (explanationPanel != null)
        {
            explanationPanel.SetActive(true);
        }

        if (startButton != null)
        {
            startButton.SetActive(false);
        }

        if(confirmButton != null){
            confirmButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("laboratory");
    }
}