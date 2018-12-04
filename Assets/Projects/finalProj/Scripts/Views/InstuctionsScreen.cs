using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstuctionsScreen : View {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBackClicked()
    {
        LoadManager.Instance.LoadScene(SceneNames.MAIN_MENU_SCENE);
    }
}
