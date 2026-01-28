using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void StartGame ()
    {
        GameManager.instance.returnToLobby(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
