using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPictureMatchListener {
	void OnMatchValid ();
	void OnMatchInvalid();
}
