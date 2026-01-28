using UnityEngine;
using TMPro;

public class LevelInfo : MonoBehaviour
{
    public GameObject LevelCanvas;
    [SerializeField] TextMeshProUGUI bestMoves;
    [SerializeField] TextMeshProUGUI bestTimes;
    public int levelIndex;

    private void OnEnable()
    {
        string bestMovesKey = "BestMoves" + (levelIndex + 1);
        string bestTimesKey = "BestTimes" + (levelIndex + 1);

        float bestTime = PlayerPrefs.GetFloat(bestTimesKey);

        int minutes = Mathf.FloorToInt(bestTime / 60f);
        int seconds = Mathf.FloorToInt(bestTime % 60f);

        bestMoves.text = PlayerPrefs.GetInt(bestMovesKey).ToString();
        bestTimes.text = string.Format("{0:00}:{1:00}", minutes, seconds);   
    }

    public void setEnterLevel()
    {
        Debug.Log("Entering Level " + (levelIndex));
        GameManager.instance.enterLevel(levelIndex);
    }

    public void showUI()
    {

        LevelCanvas.SetActive(true);
    }

    public void hideUI()
    {
        LevelCanvas.SetActive(false);
    }
}
