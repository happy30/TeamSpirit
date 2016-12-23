using UnityEngine;
using System.Collections;

public class BGMPlayer : MonoBehaviour
{

    public AudioClip[] BGM;
    public AudioSource sound;
    public StatsManager stats;
    public float audioVolume;

    public bool fading;

    public enum CurrentlyPlaying
    {
        Level1,
        Level2,
        Chase,
        Cave,
        BossFight,
        BossFightRage
    };

    public CurrentlyPlaying currentlyPlaying;
    public CurrentlyPlaying nextPlaying;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
    }

    void Update()
    {
        if (fading)
        {
            FadeOut();
        }
        if (stats.health <= 0)
        {
            sound.volume -= 2f * Time.deltaTime;
        }
    }

    public void changeBGM(CurrentlyPlaying state)
    {
        if (state != nextPlaying)
        {
            fading = true;
            nextPlaying = state;
        }

    }

    public void FadeOut()
    {
        sound.volume -= 2f * Time.deltaTime;
        if (sound.volume < 0.1f)
        {
            sound.clip = BGM[(int)nextPlaying];
            sound.volume = audioVolume;
            fading = false;
            sound.Play();
        }
    }
}