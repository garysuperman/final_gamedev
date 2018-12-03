using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallB : MonoBehaviour {

    [SerializeField] Camera cam;

    void OnTriggerEnter(Collider collider) {
        Vector3 moving;
        if (collider.gameObject.name.Contains("link")){
            cam.GetComponent<followPlayer>().lever();
            moving = collider.gameObject.transform.localPosition;
            moving.x = 2.34f;
            moving.y = 35f;
            moving.z = -22.93f;
            collider.gameObject.transform.localPosition = moving;
        }
    }
}
