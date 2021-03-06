﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    Vector3 startingPoint;
    bool backNForth = true;
    bool playerOn = false;

    [SerializeField] GameObject player;

    int target = 75*2;
	// Use this for initialization
	void Start () {
        startingPoint = this.transform.localPosition;
        playerOn = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 moving;
		if(target > 0 && backNForth) {
            target -= 1;
            moving = this.transform.localPosition ;
            moving.x -= 0.25f;
            this.transform.localPosition = moving;

            if (playerOn) {
                moving = player.transform.localPosition;
                moving.x -= 0.25f;
                player.transform.localPosition = moving;
            }

        } else {
            backNForth = false;
        }

        if (target < (75 * 2) && !backNForth) {
            target += 1;
            moving = this.transform.localPosition ;
            moving.x += 0.25f;
            this.transform.localPosition = moving;

            if (playerOn) {
                moving = player.transform.localPosition;
                moving.x += 0.25f;
                player.transform.localPosition = moving;
            }
        } else {
            backNForth = true;
        }
    }

    void OnCollisionEnter(Collision collision) {
        playerOn = true;
    }

    void OnCollisionExit(Collision collision) {
        playerOn = false;
    }
}
