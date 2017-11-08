﻿using UnityEngine.Audio; // Wrapper for unity audio feautres
using System; // To use Array.Find
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // An array of all sounds that can be played in game
    public Sound[] sounds;

    // Using Singleton pattern so that there cant be more than one AudioManager
    public static AudioManager instance;

	void Awake ()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Loop through all sounds and set different variables
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
	}

    void Start()
    {
        Play("ThemeSong");
    }

    public void Play(string name)
    {
        // Find the requestet sound (by name) in sound array
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == name);

        // If the sound is not found, return
        if (soundToPlay == null)
        {
            Debug.LogWarning("Could not find audio with name: " + name);
            return;
        }

        // Play the sound
        soundToPlay.source.Play();
        Debug.Log("Playing sound: " + name);
    }
}
