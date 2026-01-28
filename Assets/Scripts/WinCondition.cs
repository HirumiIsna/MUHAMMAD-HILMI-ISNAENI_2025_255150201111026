using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinCondition : MonoBehaviour
{
    [SerializeField] GameObject finishUI;
    public MovesTimer movesTimer;
   
    public void CheckWinCondition()
    {
        var num = 0;
        var arrBox = GameObject.FindGameObjectsWithTag("Box");
        var arrGoal = GameObject.FindGameObjectsWithTag("Goal");

        foreach (var box in arrBox)
        {
            if (box.GetComponent<BoxController>().isGoal)
            {
                num++;
            }  
        }

        if (num == arrGoal.Length)
        {
            Debug.Log("Win");
            AudioManager.instance.PlaySFXfinish();
            StartCoroutine(Delay(.5f));
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        finishUI.SetActive(true);

        movesTimer.BestMovesTimes();

        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            Debug.Log("Finish Game");
        }
        else if (SceneManager.GetActiveScene().buildIndex + 1 > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", SceneManager.GetActiveScene().buildIndex + 1);
        }

        Time.timeScale = 0f;
    }
}
