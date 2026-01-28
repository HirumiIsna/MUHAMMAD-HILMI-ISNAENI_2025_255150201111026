using UnityEngine;

public class LevelSelection : MonoBehaviour
{

    public GameObject[] levels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        Debug.Log("Back to Lobby");
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].GetComponent<SpriteRenderer>().color = new Color(76f/255f, 193f/255f, 248f/255f, 1f);
            levels[i].GetComponent<BoxCollider2D>().enabled = true;

            if (i + 2 > levelAt)
            {
                levels[i].GetComponent<SpriteRenderer>().color = Color.gray;
                levels[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
