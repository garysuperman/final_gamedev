using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public AudioClip attack_sound;
    public AudioClip jump_start;
    public AudioClip landing_;

    public AudioSource audioS;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void attacksound()
    {
        audioS.PlayOneShot(attack_sound);
    }

    void jumpstart()
    {
        audioS.PlayOneShot(jump_start);
    }

    void landing()
    {
        audioS.PlayOneShot(landing_);
    }
}
