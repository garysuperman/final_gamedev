using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSound : MonoBehaviour {

    public AudioClip idle_;


    public AudioSource audioS;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void idle()
    {
        audioS.PlayOneShot(idle_);
    }
}
