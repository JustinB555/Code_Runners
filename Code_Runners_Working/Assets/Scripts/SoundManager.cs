﻿/*
 * Editor: Wyatt Curtiss
 * Date of Creation: 11/7/2020
 * Note: Almost all ofthis except for the scene name bit was from a Brackeys tutorial https://www.youtube.com/watch?v=6OT43pvUyfY
*/

using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public bool isMenu = false;

    private string sceneName;

    public Sound[] sounds;
    public AudioSource[] audioSources;

    //public static SoundManager instance;

    private void Awake()
    {

        //if (instance == null)
        //    instance = this;
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject);

        MakeAudioSources();

        audioSources = gameObject.GetComponents<AudioSource>();
    }

    public void MakeAudioSources()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    private void Start()
    {
        


        //Debug.Log(audioSources.Length);

        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu")
        {
            isMenu = true;
        }
        else
        {
            isMenu = false;
        }

        if (isMenu)
        {
            Play("Menu");
        }
        else
        {
            Play("Gameplay");
        }
    }

    private void Update()
    {

    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
