using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallA : MonoBehaviour {

    [SerializeField] Camera cam;

    void OnTriggerEnter(Collider collider) {
        Vector3 moving;
        if (collider.gameObject.name.Contains("link")) {
            cam.GetComponent<followPlayer>().turnoff();
        }
    }
}
