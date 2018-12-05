using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wolf_script : MonoBehaviour {
    //stats
    private int health = 10;
    //public HealthSystem healthSystem = new HealthSystem(10);
    //[SerializeField] public Slider healthBar;

    //speed = 0/idle, 1/run, 2/attack 1, 3/attack 2
    private const string speed = "speed";
    private const string damaged = "damaged";
    private const string die = "die";

    // 0 = right, 1 = left
    private int facing = 0;
    private const float TURN_AMOUNT_MODIFIER = 500.0f;
    private int cooldown = 0;
    private float distance = 0;

    [SerializeField] private Animator wolf_anim;
    [SerializeField] private GameObject wolf;
    [SerializeField] private GameObject player;
    [SerializeField] private Collider collider;
    [SerializeField] private Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        //healthBar.value = healthSystem.GetHealthInPercent();
        if (this.name.Contains("wolf")) {
            distance = 50f;
            health = 10;
        } else if (this.name.Contains("BigWolf")){
            distance = 100f;
            health = 25;
        } else if (this.name.Contains("BlackWolf")) {
            distance = 50f;
            health = 15;
        }
    }

    // Update is called once per frame
    void Update(){
        Vector3 moving;
        Vector3 rot = wolf.transform.rotation.eulerAngles;
        bool onTheMove = wolf_anim.GetCurrentAnimatorStateInfo(0).IsName("wolf_running");
        bool takingDamage = wolf_anim.GetCurrentAnimatorStateInfo(0).IsName("wolf_damaged");
         float heightDistance = Mathf.Abs(player.transform.localPosition.y - this.transform.localPosition.y);

        if (distanceFromPlayer() < distance && cooldown == 0 && health >0){
            //healthBar.gameObject.SetActive(true);
            this.wolf_anim.SetInteger(speed, 1);
            facing = faced();

            if (facing == 0) {
                //x == 90
                if (distanceFromPlayer() < distance && onTheMove && !takingDamage) {
                    Physics.IgnoreCollision(player.GetComponent<BoxCollider>(), collider, false);
                    moving = wolf.transform.localPosition;
                    moving.x += Time.deltaTime * 50;
                    wolf.transform.localPosition = moving;
                } 
                if (!(rot.y <= 90)) {
                    this.wolf_anim.SetInteger(speed, 0);
                    wolf.transform.Rotate(Vector3.down * Time.deltaTime * TURN_AMOUNT_MODIFIER, Space.World);
                }

            } else {
                //x = 270
                if (distanceFromPlayer() < distance && onTheMove && !takingDamage) {
                    Physics.IgnoreCollision(player.GetComponent<BoxCollider>(), collider, false);
                    moving = wolf.transform.localPosition;
                    moving.x -= Time.deltaTime * 50;
                    wolf.transform.localPosition = moving;
                } 
                if (!(rot.y >= 270)) {
                    this.wolf_anim.SetInteger(speed, 0);
                    wolf.transform.Rotate(Vector3.up * Time.deltaTime * TURN_AMOUNT_MODIFIER, Space.World);
                }

            }
        } else if(health > 0){
            if(cooldown > 0) {
                cooldown -= 1;
                if (facing == 0) {
                    moving = wolf.transform.localPosition;
                    moving.x += Time.deltaTime * 60;
                    wolf.transform.localPosition = moving;
                } else {
                    moving = wolf.transform.localPosition;
                    moving.x -= Time.deltaTime * 60;
                    wolf.transform.localPosition = moving;
                }
            }
            this.wolf_anim.SetInteger(speed, 0);
            //if (distanceFromPlayer() > 50)
                //healthBar.gameObject.SetActive(false);
        } 

        if(health <= 0) {
            Physics.IgnoreCollision(player.GetComponent<BoxCollider>(), collider);
            this.wolf_anim.SetTrigger(die);
        }

    }

    public void hitByPlayer() {
        health -= 1;
        //healthSystem.Damage(1);
        //healthBar.value = healthSystem.GetHealthInPercent();
        if (health > 0) {
            this.wolf_anim.SetTrigger(damaged);
            cooldown = 0;
        }
        else {
            Physics.IgnoreCollision(player.GetComponent<BoxCollider>(), collider);
            this.wolf_anim.SetTrigger(die);
            //healthBar.gameObject.SetActive(false);
        }
        
    }

    // 0 = right, 1 = left
    private int faced() {
        float diffrence = wolf.transform.position.x - player.transform.position.x;

        if (diffrence > 0) {
            return 1;
        }
        return 0;

    }

    private float distanceFromPlayer(){
        return Vector3.Distance(player.transform.position, wolf.transform.position);
    }

    void OnCollisionEnter(Collision collision) {
        Vector3 moving;
        if (collision.gameObject.name.Contains("link")) {
            Physics.IgnoreCollision(player.GetComponent<BoxCollider>(), collider);
            cooldown = 45;
            collision.gameObject.GetComponent<PlayerScript>().wasHit("wolf");
            if (facing == 0) {
                moving = player.transform.localPosition;
                moving.x += Time.deltaTime * 100;
                player.transform.localPosition = moving;
            } else {
                moving = player.transform.localPosition;
                moving.x -= Time.deltaTime * 100;
                player.transform.localPosition = moving;
            }
        }
    }
}
