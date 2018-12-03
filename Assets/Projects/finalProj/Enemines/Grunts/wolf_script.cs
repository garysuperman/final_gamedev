using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolf_script : MonoBehaviour {
    //stats
    [SerializeField] private int health = 500;

    //speed = 0/idle, 1/run, 2/attack 1, 3/attack 2
    private const string speed = "speed";

    // 0 = right, 1 = left
    private int facing = 0;
    private const float TURN_AMOUNT_MODIFIER = 500.0f;


    [SerializeField] private Animator wolf_anim;
    [SerializeField] private GameObject wolf;
    [SerializeField] private GameObject player;
    [SerializeField] private Collider collider;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
