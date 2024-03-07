using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private bool isAudioPlay;
    private bool isAudioScenePlay;

    public static AudioManager instance;
    
    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        //DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        isAudioPlay = false;
    }

    void Start()
    {
        Play(SceneManager.GetActiveScene().name);
        isAudioPlay = false;
        isAudioScenePlay = true;
    }

    void Update()
    {
        if(!isAudioPlay)
        {
            if(FindObjectOfType<MenuScript>() && FindObjectOfType<MenuScript>().GameOverScreenIsActive())
            {
                Stop(SceneManager.GetActiveScene().name);
                Play("GameOverMusic");
                isAudioPlay = true;
            }
            if(FindObjectOfType<MenuScript>() && FindObjectOfType<MenuScript>().WinGameScreenIsActive())
            {
                Stop(SceneManager.GetActiveScene().name);
                Play("WinMusic");
                isAudioPlay = true;
            }
            //if (isAudioScenePlay)
            //{
            //    String soundPlay = "";
            //    foreach (Sound s in sounds)
            //    {
            //        soundPlay = s.name;
            //        if (s.source.isPlaying) {
            //            soundPlay = s.source.name;
            //            break;
            //        }
            //    }
            //    if (SceneManager.GetActiveScene().name != soundPlay)
            //    {
            //        Stop(soundPlay);
            //        Play(SceneManager.Get.name);
            //    }
            //}
        }
        else
        {
            if(!FindObjectOfType<MenuScript>().GameOverScreenIsActive() && !FindObjectOfType<MenuScript>().WinGameScreenIsActive())
            {
                Stop("GameOverMusic");
                Stop("WinMusic");
                Play(SceneManager.GetActiveScene().name);
                isAudioPlay = false;
            }
        }
    }

    private void OnDestroy()
    {
        Stop(SceneManager.GetActiveScene().name);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}
