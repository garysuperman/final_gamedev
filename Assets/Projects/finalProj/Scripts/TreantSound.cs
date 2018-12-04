using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreantSound : MonoBehaviour {

    public AudioClip walk_;
    public AudioClip stomp_;
    public AudioClip slam_;

    public AudioSource audioS;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void walk()
    {
        audioS.PlayOneShot(walk_);
    }

    void stomp ()
    {
        audioS.PlayOneShot(stomp_);
    }

    void slam()
    {
        audioS.PlayOneShot(slam_);
    }
}
