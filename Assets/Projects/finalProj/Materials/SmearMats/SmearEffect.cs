using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmearEffect : MonoBehaviour
{
	Queue<Vector3> _recentPositions = new Queue<Vector3>();

	[SerializeField]
	int _frameLag = 5;
    
	Material _smearMat = null;
	public Material smearMat
	{
		get
		{
			if (!_smearMat)
				_smearMat = GetComponent<Renderer>().material;

			if (!_smearMat.HasProperty("_PrevPosition"))
				_smearMat.shader = Shader.Find("Custom/Smear");

			return _smearMat;
		}
	}

    private int timeLeftOn = 0;
    private bool enabled = false;
    /*
    void Update() {
        if(Input.GetKey(KeyCode.J) && timeLeftOn == 0) {
            enabled = true;
            timeLeftOn = 20;
        } else if(timeLeftOn > 0) {
            timeLeftOn -= 1;
        } else {
            enabled = false;
            timeLeftOn = 0;
        }
    }*/

    void LateUpdate()
	{
        if (enabled) {
            if(_recentPositions.Count > _frameLag)
			    smearMat.SetVector("_PrevPosition", _recentPositions.Dequeue());

		    smearMat.SetVector("_Position", transform.position);
		    _recentPositions.Enqueue(transform.position);
        }
		
	}
}
