using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource levelMusic, gameOverMusic, winMusic;

    public AudioSource[] sfx;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (levelMusic != null && GameManager.Instance.StartLevelMusicOnPlay && !levelMusic.isPlaying)
        {
            levelMusic.Play();
        }
    }
    public void PlayGameOver()
    {
        levelMusic.Stop();

        gameOverMusic.Play();

    }
    public void PlayLevelWin()
    {
        levelMusic.Stop();

        winMusic.Play();
    }
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].transform.position = Vector3.zero;
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
    public void PlaySfxAtPosition(int sfxToPlay, Vector3 position)
    {
        sfx[sfxToPlay].transform.position = position;
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
}
