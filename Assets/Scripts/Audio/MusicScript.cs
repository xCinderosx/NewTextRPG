using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour {

    public List<AudioClip> songs = new List<AudioClip>();
    public AudioSource cameraAudioSource;
    public bool normalMusicIsPlaying = false, attackMusicIsPlaying = false, briefMusicPlayed = false, menuMusicIsPlaying = false;
    public CameraScript cameraScript;
    public Mission1 mission;
    public PopMenuScript Menu;
    // Use this for initialization
    void Start () {
        cameraAudioSource = GetComponentInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            if (SceneManager.GetSceneByName("OptionsScene").isLoaded && !normalMusicIsPlaying)
            {
                normalMusicIsPlaying = true;
                cameraAudioSource.clip = songs[0];
                cameraAudioSource.loop = true;
                cameraAudioSource.Play(0);
            }
            else if (SceneManager.GetSceneByName("Scene One").isLoaded)
            {
                if (Menu.pause && !menuMusicIsPlaying)
                {
                    menuMusicIsPlaying = true;
                    attackMusicIsPlaying = false;
                    normalMusicIsPlaying = false;
                    cameraAudioSource.loop = true;
                    cameraAudioSource.clip = songs[0];
                    cameraAudioSource.Play(0);
                }
                if (!briefMusicPlayed && !Menu.pause)
                {
                    cameraAudioSource.volume = 0.06f;
                    briefMusicPlayed = true;
                    cameraAudioSource.loop = false;
                    cameraAudioSource.clip = songs[1];
                    cameraAudioSource.Play(0);
                }
                if (!cameraScript.UnderAttack && !normalMusicIsPlaying && !mission.brief && !Menu.pause)
                {
                    cameraAudioSource.volume = 0.01f;
                    cameraAudioSource.loop = true;
                    attackMusicIsPlaying = false;
                    menuMusicIsPlaying = false;
                    normalMusicIsPlaying = true;
                    cameraAudioSource.clip = songs[3];
                    cameraAudioSource.Play(0);
                }
                else
                if (cameraScript.UnderAttack && !attackMusicIsPlaying && !mission.brief && !Menu.pause)
                {
                    cameraAudioSource.volume = 0.03f;
                    cameraAudioSource.loop = true;
                    attackMusicIsPlaying = true;
                    menuMusicIsPlaying = false;
                    normalMusicIsPlaying = false;
                    cameraAudioSource.clip = songs[2];
                    cameraAudioSource.Play(0);
                }


            }
        }
        catch (System.Exception)
        {

            throw;
        }
		
	}
}
