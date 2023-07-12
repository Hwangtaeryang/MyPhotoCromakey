using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource sfxPlayer;

    public AudioClip bgmClip;
    AudioClip sfxClip;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        BGMSound();
    }

    public void BGMSound()
    {
        musicPlayer.clip = bgmClip;
        musicPlayer.Play();
    }

    public void SFXSound(string _sfxName)
    {
        sfxPlayer.PlayOneShot(Resources.Load<AudioClip>("Sound/" + _sfxName));
    }
}
