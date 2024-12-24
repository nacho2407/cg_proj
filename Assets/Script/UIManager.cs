using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;
    public GameObject gameOverPanel;
    public GameObject ClearPanel;
    public Button restartButton;
    public Button quitButton;
    public Button restartButton2;
    public Button quitButton2;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        restartButton2.onClick.AddListener(RestartGame);
        quitButton2.onClick.AddListener(QuitGame);
    }

    public void GameOver()
    {
        player.SetActive(false);
        playerCamera.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // 게임 일시정지
    }
    public void Clear()
    {
        player.SetActive(false);
        playerCamera.SetActive(false);
        ClearPanel.SetActive(true);
        Time.timeScale = 0f;
    }


    private void RestartGame()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
