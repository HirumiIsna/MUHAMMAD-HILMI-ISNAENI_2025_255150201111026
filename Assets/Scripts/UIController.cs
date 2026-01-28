using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] GameObject pauseMenuUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu pauseMenu = pauseMenuUI.GetComponent<PauseMenu>();
            if (pauseMenu != null)
            {
                if (Time.timeScale == 1f)
                {
                    pauseMenu.pauseGame();
                }
                else
                {
                    pauseMenu.resumeGame();
                }
            }
        }
    }
}
