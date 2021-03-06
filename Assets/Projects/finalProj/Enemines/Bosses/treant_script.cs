﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treant_script : MonoBehaviour {
    //stats
    public HealthSystem healthSystem = new HealthSystem(100);
    [SerializeField] public Slider healthBar;
    [SerializeField] public Text nameText;

    //speed = 0/idle, 1/run, 2/attack 1, 3/attack 2
    private const string speed = "speed";

    // 0 = right, 1 = left
    private int facing = 0;
    private const float TURN_AMOUNT_MODIFIER = 200.0f;


    [SerializeField] private Animator treant_anim;
    [SerializeField] private GameObject treant;
    [SerializeField] private GameObject player;
    private BoxCollider[] components;

    // Use this for initialization
    void Start () {
        components = treant.GetComponents<BoxCollider>();
        healthBar.value = healthSystem.GetHealthInPercent();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 moving;
        Vector3 rot = treant.transform.rotation.eulerAngles;
        bool attacking = (treant_anim.GetCurrentAnimatorStateInfo(0).IsName("treant_attack_slam") 
                       || treant_anim.GetCurrentAnimatorStateInfo(0).IsName("treant_attack_stomp"));
        bool onTheMove = treant_anim.GetCurrentAnimatorStateInfo(0).IsName("treant_walk");
        bool idled = treant_anim.GetCurrentAnimatorStateInfo(0).IsName("treant_idle");
        int crrntSpeed = this.treant_anim.GetInteger(speed);

        if (distanceFromPlayer() < 60f && healthSystem.GetHealth() > 0) {
            if (!healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(true);
                nameText.gameObject.SetActive(true);
            }

            if (distanceFromPlayer() > 35f) {
                this.treant_anim.SetInteger(speed, 1);
            } else {
                if (rot.y < 90 || rot.y > 270) {
                    if (distanceFromPlayer() > 30f) {
                        if (crrntSpeed == 0 || crrntSpeed == 1)
                            Invoke("attackPlayer", 2);
                        this.treant_anim.SetInteger(speed, 3);
                    } else {
                        if (crrntSpeed == 0 || crrntSpeed == 1)
                            Invoke("attackPlayer", 1);
                        this.treant_anim.SetInteger(speed, 2);
                    }
                        
                }
            }

            facing = faced();

            if (facing == 0) {
                //x == 90
                if (distanceFromPlayer() < 60f && distanceFromPlayer() > 30f && onTheMove) { 
                    moving = treant.transform.localPosition;
                    moving.x += 0.35f;
                    treant.transform.localPosition = moving;
                }
                if (!(rot.y <= 90) && !attacking) {
                    this.treant_anim.SetInteger(speed, 0);
                    treant.transform.Rotate(Vector3.down * Time.deltaTime * TURN_AMOUNT_MODIFIER, Space.World);
                }
                
            } else {
                //x = 270
                if (distanceFromPlayer() < 60f && distanceFromPlayer() > 30f && onTheMove) {
                    moving = treant.transform.localPosition;
                    moving.x -= 0.35f;
                    treant.transform.localPosition = moving;
                }
                if (!(rot.y >= 270) && !attacking) {
                    this.treant_anim.SetInteger(speed, 0);
                    treant.transform.Rotate(Vector3.up * Time.deltaTime * TURN_AMOUNT_MODIFIER, Space.World);
                }
                
            }
        } else {
            this.treant_anim.SetInteger(speed, 0);
            if (healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(false);
                nameText.gameObject.SetActive(false);
            }
        }

        
    }

    public void attackPlayer() {
        if (distanceFromPlayer() <= 35f && inAttackZone())
            this.player.GetComponent<PlayerScript>().wasHit("treant");
        else Debug.Log("Missed");
    }

    public void hitByPlayer() {
        healthSystem.Damage(2);
        healthBar.value = healthSystem.GetHealthInPercent();
        Debug.Log("treant :" + healthSystem.GetHealth());

        if (healthSystem.GetHealth() > 0)
        {
            //damaged animation
        }
        else {
            Physics.IgnoreCollision(player.GetComponent<BoxCollider>(), components[2]);
            //death animation
            healthBar.gameObject.SetActive(false);
            nameText.gameObject.SetActive(false);
        }
    }

    private float distanceFromPlayer() {
        return Vector3.Distance(player.transform.position, treant.transform.position);
    }

    private bool inAttackZone() {
        Vector3 rot = treant.transform.rotation.eulerAngles;
        if (faced() == 0 && rot.y < 90) {
            return true;
        } else if (faced() == 1 && rot.y > 270) {
            return true;
        } else {
            return false;
        }
    }

    // 0 = right, 1 = left
    private int faced() {
        float diffrence = treant.transform.position.x - player.transform.position.x;

        if(diffrence > 0) {
            return 1;
        } 
        return 0;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("link"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), components[0]);
            //Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), components[1]);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), components[2]); //body
            
        }
    }
}
