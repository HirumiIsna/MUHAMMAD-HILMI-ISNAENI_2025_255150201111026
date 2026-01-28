using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background1;
    public AudioClip background2;
    public AudioClip UIClick1;
    public AudioClip UIClick2;
    public AudioClip pushBox;
    public AudioClip correctGoal;
    public AudioClip portalTeleport;
    public AudioClip finishedLevel;

    public static AudioManager instance;

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

    private void Start()
    {
        Debug.Log("PlayingMusic");
        musicSource.clip = background1;
        musicSource.Play();
    }

    //main menu + lobby pake music background1, ingame nya pake music background2
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 2)
        {
            PlayMusic(background2);
        } 
        else
        {
            PlayMusic(background1);
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }   

    public void PlaySFXopen()
    {
        SFXSource.PlayOneShot(UIClick1);
    }

    public void PlaySFXclose()
    {
        SFXSource.PlayOneShot(UIClick2);
    }

    public void PlaySFXpush()
    {
        SFXSource.PlayOneShot(pushBox);
    }

    public void PlaySFXgoal()
    {
        SFXSource.PlayOneShot(correctGoal);
    }

    public void PlaySFXportal()
    {
        SFXSource.PlayOneShot(portalTeleport);
    }

    public void PlaySFXfinish()
    {
        SFXSource.PlayOneShot(finishedLevel);
    }
}
