using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallB : MonoBehaviour {

    [SerializeField] Camera cam;

    void OnTriggerEnter(Collider collider) {
        Vector3 moving;
        if (collider.gameObject.name.Contains("link")){
            cam.GetComponent<followPlayer>().turnon();
            moving = collider.gameObject.GetComponent<PlayerScript>().getPositionBeforeDeath();
            collider.gameObject.transform.localPosition = moving;
            collider.gameObject.GetComponent<PlayerScript>().wasHit("pitfall");
        }
    }
}
