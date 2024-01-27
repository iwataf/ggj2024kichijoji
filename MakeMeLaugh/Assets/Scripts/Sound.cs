using System.Collections;
using System.Collections.Generic;
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
    }

    [SerializeField] AudioClip[] bgm;
    [SerializeField] AudioClip[] se;

    private AudioClip selectBGM;
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm[0];
    }

    public void PlayBGM(bgmValue bgmNo)
    {
        audioSource.clip = bgm[(int)bgmNo];
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void PlaySE(seValue seNo)
    {
        audioSource.PlayOneShot(bgm[(int)seNo]);
    }
}
