using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campheal : MonoBehaviour {

    [SerializeField] GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(distanceFromPlayer() <= 20) {
            if(!(player.GetComponent<PlayerScript>().healthSystem.GetHealth() == 100))
                Debug.Log(player.GetComponent<PlayerScript>().healthSystem.GetHealth());
            player.GetComponent<PlayerScript>().healthSystem.Heal(1);
            player.GetComponent<PlayerScript>().healthBar.value = player.GetComponent<PlayerScript>().healthSystem.GetHealthInPercent();
        }
	}

    private float distanceFromPlayer() {
        return Vector3.Distance(player.transform.position, this.transform.position);
    }
}
