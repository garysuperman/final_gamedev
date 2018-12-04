using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : View {

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		
	}

    private void Update()
    {

    }

    public void OnPlayClicked() {
		LoadManager.Instance.LoadScene (SceneNames.GAME_SCENE);	
	}

	public void OnQuitClicked() {
		Application.Quit();
	}

    public void OnInstructionsClicked()
    {
        LoadManager.Instance.LoadScene(SceneNames.INSTRUCTIONS_SCENE);
    }
}
