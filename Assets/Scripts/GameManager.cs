using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator transition;
    public int lastBuildIndex = 0;
    // singleton pattern (buat mastiin hanya ada 1 game manager)
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void loadNextScene(int buildIndex)
    {
        AudioManager.instance.PlaySFXopen();
        StartCoroutine(loadScene(buildIndex));  
    }

    public void restartLevel (int buildIndex)
    {
        StartCoroutine(loadScene(buildIndex));  
    }

    public void returnToMenu()
    {
        StartCoroutine(loadScene(0));     
        AudioManager.instance.PlaySFXclose();
    }

    public void returnToLobby(int buildIndex)
    {
        StartCoroutine(loadScene(1));      
        AudioManager.instance.PlaySFXopen();
        lastBuildIndex = buildIndex;
    }

    public void enterLevel(int levelIndex)
    {
        StartCoroutine(loadScene(levelIndex + 1));
        AudioManager.instance.PlaySFXopen();
    }

    IEnumerator loadScene(int buildIndex)
    {
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
        transition.SetTrigger("Start");
    }
}
