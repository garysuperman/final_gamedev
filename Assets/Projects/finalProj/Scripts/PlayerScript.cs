using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    //stats
    [SerializeField] private int health = 1000;

    //speed = 0/idle, 1/run, 2/attack, 3/downward_thrust
    private const string speed = "speed";
    private const string onAir = "onAir";
    private const string jump = "jump";
    private const string falling = "falling";
    private const string damage = "damage";

    // 0 = right, 1 = left
    private int facing = 0;
    private const float TURN_AMOUNT_MODIFIER = 650.0f;

    //to Determine if Player is on ground
    private float distToGround;
    private float playerWidth;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Collider collider;

    // Use this for initialization
    void Start () {
        distToGround = collider.bounds.extents.y - 5.07f;
        playerWidth = collider.bounds.extents.z;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 moving;

        bool jumped = this.playerAnim.GetBool(jump);
        bool airborn = this.playerAnim.GetBool(onAir);
        int crrntSpeed = this.playerAnim.GetInteger(speed);
        bool takingDamage = !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("damaged");

        if (!isGrounded() && !airborn) {
            this.playerAnim.SetBool(onAir, true);
            this.playerAnim.SetTrigger(falling);
        }
        
        if (Input.GetKey(KeyCode.D)) {
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack") &&
                !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack_air") &&
                !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack_downthrust") && takingDamage)
                    facing = 0;
            this.playerAnim.SetInteger(speed, 1);
            moving = player.transform.localPosition;
            if(!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack") && takingDamage) 
                moving.x += 0.5f;
            player.transform.localPosition = moving;
            
        } else if (Input.GetKey(KeyCode.A)) {
            if(!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack") &&
                !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack_air") &&
                !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack_downthrust") && takingDamage)
                    facing = 1;
            this.playerAnim.SetInteger(speed, 1);
            moving = player.transform.localPosition;
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack") && takingDamage)    
                moving.x -= 0.5f;
            player.transform.localPosition = moving;

        } else {
            this.playerAnim.SetInteger(speed, 0);
        }

        if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.S) && !isGrounded()) {
            this.playerAnim.SetInteger(speed, 3);
        }
        else if (Input.GetKey(KeyCode.J)) {
            this.playerAnim.SetInteger(speed, 2);
            attack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !(jumped || airborn || !takingDamage) && isGrounded()) {
            this.playerAnim.SetBool(onAir, true);
            this.playerAnim.SetBool(jump, true);
            moving = rigidbody.velocity;
            moving.y = 50;
            rigidbody.velocity = moving;
        } else if (jumped) {
            this.playerAnim.SetBool(jump, false);
            this.playerAnim.SetTrigger(falling);
        }
        else if (isGrounded() && airborn) {
            this.playerAnim.SetBool(onAir, false);
        }

        Vector3 rot =  player.transform.rotation.eulerAngles;
        
        if (facing == 0) {
            //x == 90
            if(rot.y > 90) {
                player.transform.Rotate(Vector3.down * Time.deltaTime * TURN_AMOUNT_MODIFIER, Space.World);
            }
        } else if(facing == 1) {
            //x = 270
            if (rot.y < 270) {
                player.transform.Rotate(Vector3.up * Time.deltaTime * TURN_AMOUNT_MODIFIER, Space.World);
            }
        }
        
    }

    public void wasHit(string name) {
        if (name.Equals("treant")) {
            health -= 10;
            Debug.Log(health);
        }
        if(!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("damaged"))
            this.playerAnim.SetTrigger(damage); 

    }

    private void attack() {
        //stab
        Vector3 stab = transform.position;
        Ray ray = new Ray();
        RaycastHit hit;
        if (facing == 0) {
            //left
            stab.x += collider.bounds.extents.x;
            stab.y += collider.bounds.extents.y;
            ray = new Ray(stab, -Vector3.left);
                
        } else {
            //right
            stab.x -= collider.bounds.extents.x;
            stab.y += collider.bounds.extents.y;
            ray = new Ray(stab, -Vector3.right);
        }

        if (Physics.Raycast(ray, out hit, 8.68f)) {
            if (hit.transform.gameObject.name.Contains("treant")) {
                hit.transform.gameObject.GetComponent<treant_script>().hitByPlayer();
            }
        }
    }

    private bool isGrounded() {
        Vector3 front = transform.position;
        front.x += playerWidth;
        Vector3 back = transform.position;
        back.x -= playerWidth;

        return Physics.Raycast(front, -Vector3.up, distToGround + 0.1f) ||
               Physics.Raycast(back, -Vector3.up, distToGround + 0.1f);
    }
}
