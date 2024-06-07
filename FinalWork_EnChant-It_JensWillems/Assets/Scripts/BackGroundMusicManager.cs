using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip BackgroundMusic;
    public AudioClip CombatMusic;
    public GameManager GameManager;

    private bool isCombatMusicPlaying = false;

    void Start()
    {
        AudioSource.clip = BackgroundMusic;
        AudioSource.loop = true; 
        AudioSource.Play();
    }

    void Update()
    {
        if (GameManager.StartWave && !isCombatMusicPlaying)
        {
            AudioSource.clip = CombatMusic;
            AudioSource.loop = true; 
            AudioSource.Play();
            isCombatMusicPlaying = true;
        }
    }
}
