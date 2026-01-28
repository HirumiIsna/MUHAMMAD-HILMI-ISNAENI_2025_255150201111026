using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject pauseButtonUI;
    public GameObject finishUI;

    public void pauseGame()
    {
        pauseMenuUI.SetActive(true);
        pauseButtonUI.SetActive(false);
        AudioManager.instance.PlaySFXopen();
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        pauseMenuUI.SetActive(false);
        pauseButtonUI.SetActive(true);
        AudioManager.instance.PlaySFXclose();
        Time.timeScale = 1f;
    }

    public void restartLevel()
    {
        Time.timeScale = 1f;
        GameManager.instance.loadNextScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void nextLevel()
    {
        Time.timeScale = 1f;
        GameManager.instance.loadNextScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void backToLobby()
    {
        Time.timeScale = 1f;
        GameManager.instance.returnToLobby(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}
