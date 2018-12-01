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
        /*if (ViewHandler.Instance.IsViewActive("MainMenu") && Input.GetKeyDown(KeyCode.Escape))
        {
            TwoChoiceDialog dialog = (TwoChoiceDialog)DialogBuilder.Create(DialogBuilder.DialogType.CHOICE_DIALOG);
            dialog.SetMessage("Are you sure you want to quit?");
            dialog.SetConfirmText("YES");
            dialog.SetCancelText("NO");

            dialog.SetOnConfirmListener(() =>
            {
                Application.Quit();
            });
        }*/
    }

    public void OnPlayClicked() {
        Time.timeScale = 1f;
		LoadManager.Instance.LoadScene (SceneNames.GAME_SCENE);	
	}

	public void OnQuitClicked() {
		Application.Quit();
	}
}
