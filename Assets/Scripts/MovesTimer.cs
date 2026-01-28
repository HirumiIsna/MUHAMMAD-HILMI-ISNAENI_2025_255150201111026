using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MovesTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI movesText;
    [SerializeField] TextMeshProUGUI timerText;
    public static int movesCount;
    private float elapsedTime = 0f;

    void Start()
    {
        movesCount = 0;

    }
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void incrementMoves()
    {
        movesCount++;
        movesText.text = movesCount.ToString();
    }

    public void BestMovesTimes()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        string bestMovesKey = "BestMoves" + levelIndex;
        string bestTimesKey = "BestTimes" + levelIndex;

        if (PlayerPrefs.HasKey(bestMovesKey))
        {
            if (movesCount < PlayerPrefs.GetInt(bestMovesKey))
            {
                PlayerPrefs.SetInt(bestMovesKey, movesCount);
            }
        }
        else PlayerPrefs.SetInt(bestMovesKey, movesCount);
        
        if (PlayerPrefs.HasKey(bestTimesKey))
        {
            if (elapsedTime < PlayerPrefs.GetFloat(bestTimesKey))
            {
                PlayerPrefs.SetFloat(bestTimesKey, elapsedTime);
            }
        }
        else PlayerPrefs.SetFloat(bestTimesKey, elapsedTime);

        Debug.Log(bestMovesKey);
        Debug.Log(PlayerPrefs.GetInt(bestMovesKey));
        Debug.Log(bestTimesKey);
        Debug.Log(PlayerPrefs.GetFloat(bestTimesKey));
    }
}
