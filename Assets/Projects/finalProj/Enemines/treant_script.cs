using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treant_script : MonoBehaviour {
    //stats
    [SerializeField] private int health = 100;

    //speed = 0/idle, 1/run, 2/attack 1, 3/attack 2
    private const string speed = "speed";

    // 0 = right, 1 = left
    private int facing = 0;
    private const float TURN_AMOUNT_MODIFIER = 400.0f;


    [SerializeField] private Animator treant_anim;
    [SerializeField] private GameObject treant;
    [SerializeField] private GameObject player; 
    [SerializeField] private Collider collider;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moving;
        Vector3 rot = treant.transform.rotation.eulerAngles;

        Debug.Log(distanceFromPlayer());
        if(distanceFromPlayer() < 60f) {
            if(distanceFromPlayer() > 35f) {
                this.treant_anim.SetInteger(speed, 1);
            } else {
                if (distanceFromPlayer() > 30f)
                    this.treant_anim.SetInteger(speed, 3);
                else
                    this.treant_anim.SetInteger(speed, 2);
            }
            
            if (facing == faced()) {
                //x == 90
                if(distanceFromPlayer() < 60f && distanceFromPlayer() > 30f 
                   && !treant_anim.GetCurrentAnimatorStateInfo(0).IsName("treant_attack_slam")
                   && !treant_anim.GetCurrentAnimatorStateInfo(0).IsName("treant_attack_stomp")) { 
                    moving = treant.transform.localPosition;
                    moving.x += 0.35f;
                    treant.transform.localPosition = moving;
                }
                else if (rot.y > 90) {
                    treant.transform.Rotate(Vector3.down * Time.deltaTime * TURN_AMOUNT_MODIFIER, Space.World);
                }
                
            } else {
                //x = 270
                if (distanceFromPlayer() < 60f && distanceFromPlayer() > 30f
                   && !treant_anim.GetCurrentAnimatorStateInfo(0).IsName("treant_attack_slam")
                   && !treant_anim.GetCurrentAnimatorStateInfo(0).IsName("treant_attack_stomp")) {
                    moving = treant.transform.localPosition;
                    moving.x -= 0.35f;
                    treant.transform.localPosition = moving;
                }
                else if (rot.y < 270) {
                    treant.transform.Rotate(Vector3.up * Time.deltaTime * TURN_AMOUNT_MODIFIER, Space.World);
                }
                
            }
        } else {
            this.treant_anim.SetInteger(speed, 0);
        }

        
    }

    private float distanceFromPlayer() {
        return Vector3.Distance(player.transform.position, treant.transform.position);
    }

    // 0 = right, 1 = left
    private int faced() {
        float diffrence = treant.transform.position.x - player.transform.position.x;

        if(diffrence > 0) {
            return 1;
        } 
        return 0;
        
    }
}
