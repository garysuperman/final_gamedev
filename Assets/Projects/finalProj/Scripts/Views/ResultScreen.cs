using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the results screen
/// BY: Neil DG
/// </summary>
public class ResultScreen : View {

	[SerializeField] private Text levelText;
	[SerializeField] private Text scoreText;
	[SerializeField] private Text correctMatchText;
	[SerializeField] private Text wrongMatchText;

	// Use this for initialization
	void Start () {
		this.SetCancelable (false); //do not allow this to be cancelled.
	}

	public void OnPlayAgainClicked() {
		LoadManager.Instance.LoadScene (SceneNames.GAME_SCENE);
	}

	public void OnMainMenuClicked() {
		LoadManager.Instance.LoadScene (SceneNames.MAIN_MENU_SCENE);
	}

	public void SetResult(int level, int score, int correctMatches, int wrongMatches) {
		this.levelText.text = level.ToString () + " out of " + GameMechanicHandler.MAX_LEVEL + " levels";
		this.scoreText.text = score.ToString ();
		this.correctMatchText.text = correctMatches.ToString ();
		this.wrongMatchText.text = wrongMatches.ToString ();
	}
}
