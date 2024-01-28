using UnityEngine;

public class Sound : MonoBehaviour
{
    public enum bgmValue
    {
        title,
        game,
        result,
    }

    public enum seValue
    {
        correct,
        endGong,
        incorrect,
        startGong,
        themeAnnouncement,
        tukkomi,
        wadaiko,
        badEnd,
        goodEnd,
        goodEndVoice,
    }

    [SerializeField] private AudioClip[] bgm;
    [SerializeField] private AudioClip[] se;
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource seAudioSource;

    public static Sound Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        //PlayBGM(0);
    }

    public void PlayBGM(bgmValue bgmNo)
    {
        bgmAudioSource.clip = bgm[(int)bgmNo];
        bgmAudioSource.Play();
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void PlaySE(seValue seNo)
    {
        seAudioSource.PlayOneShot(se[(int)seNo]);
    }
}
