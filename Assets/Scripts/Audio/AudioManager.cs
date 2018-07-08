using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip[] footsteps;
    public AudioClip jump;
    public float pitch, volume;
	// Use this for initialization
	void Start () {
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayFootstep()
    {
        var randomSound = Random.Range(0, footsteps.Length);
        audioSource.clip = footsteps[randomSound];
        audioSource.Play(0);
    }

    void PlayJump()
    {
        audioSource.clip = jump;
        audioSource.Play(0);
    }
}
