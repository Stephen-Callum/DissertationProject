using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool paused;
    private AudioSource[] allAudioSources; 

    private void Start()
    {
        paused = false;
        allAudioSources = FindObjectsOfType<AudioSource>() as AudioSource[];
    }

    private void Update()
    {
        Pause();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;

            
        }
        if (paused)
        {
            Time.timeScale = 0;
            // pause all sounds
            foreach (var sound in allAudioSources)
            {
                sound.Pause();
            }
        }
        else if (!paused)
        {
            Time.timeScale = 1;
            // unpause all sounds
            foreach (var sound in allAudioSources)
            {
                sound.UnPause();
            }
        }
    }
}
