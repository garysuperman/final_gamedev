using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallC : MonoBehaviour {

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name.Contains("link")) {
            Debug.Log(this.transform.localPosition);
            Vector3 pos = collider.gameObject.transform.localPosition;
            collider.gameObject.GetComponent<PlayerScript>().setPositionBeforeDeath(pos);
        }
    }
}
