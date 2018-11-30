using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game mechanic handler handles the overall mechanic of the game such as scoring and tracking of matches.
/// By: Neil DG
/// </summary>
public class GameMechanicHandler : MonoBehaviour {

	private static GameMechanicHandler sharedInstance = null;
	public static GameMechanicHandler Instance {
		get {
			return sharedInstance;
		}
	}

	public const string PICTURE_MODEL_KEY = "PICTURE_MODEL_KEY";
	public const string PICTURE_MATCH_LISTENER_KEY = "PICTURE_MATCH_LISTENER_KEY";

	private PictureModel firstPicture;
	private PictureModel secondPicture;

	private IPictureMatchListener firstListener;
	private IPictureMatchListener secondListener;

	private List<PictureModel> generatedPictureModels = new List<PictureModel>(); //holds picture models to be referenced by several picture components
	private int currentLevel = 1;

	public const int MAX_LEVEL = 5;

	private int requiredNumMatches = 0;

	void Awake() {
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		EventBroadcaster.Instance.AddObserver (EventNames.ON_PICTURE_CLICKED, this.OnReceivedPictureClickedEvent);
	}

	void OnDestroy() {
		sharedInstance = null;
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_PICTURE_CLICKED);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncreaseLevel() {
		this.currentLevel++;
		this.currentLevel = Mathf.Clamp (this.currentLevel, 1, MAX_LEVEL);

		EventBroadcaster.Instance.PostEvent (EventNames.ON_INCREASE_LEVEL);
	}

	public int GetCurrentLevel() {
		return this.currentLevel;
	}

	public int GetRequiredMatches() {
		return this.requiredNumMatches;
	}

	/// <summary>
	/// Generates a set of picture models.
	/// </summary>
	/// <returns>The picture models.</returns>
	public PictureModel[] GeneratePictureModels() {
		this.requiredNumMatches = (4 + (this.currentLevel * 4)) / 2;

		this.generatedPictureModels.Clear (); //clear models first
		for (int i = 0; i < requiredNumMatches; i++) {
			this.generatedPictureModels.Add (new PictureModel (PictureModel.GenerateRandomType ()));
		}

		return this.generatedPictureModels.ToArray ();
	}

	private void OnReceivedPictureClickedEvent(Parameters parameters) {
		PictureModel pictureModel = (PictureModel) parameters.GetObjectExtra (GameMechanicHandler.PICTURE_MODEL_KEY);
		IPictureMatchListener pictureMatchListener = (IPictureMatchListener)parameters.GetObjectExtra (GameMechanicHandler.PICTURE_MATCH_LISTENER_KEY);

		if (this.firstPicture == null) {
			this.firstPicture = pictureModel;
			this.firstListener = pictureMatchListener;
		} else if (this.secondPicture == null) {
			this.secondPicture = pictureModel;
			this.secondListener = pictureMatchListener;

			//verify if both are matched
			if (this.firstPicture.GetPictureType () == this.secondPicture.GetPictureType ()) {
				this.firstListener.OnMatchValid ();
				this.secondListener.OnMatchValid ();

				EventBroadcaster.Instance.PostEvent (EventNames.ON_UPDATE_SCORE);
				EventBroadcaster.Instance.PostEvent (EventNames.ON_CORRECT_MATCH);
			} else {
				this.firstListener.OnMatchInvalid ();
				this.secondListener.OnMatchInvalid ();

				EventBroadcaster.Instance.PostEvent (EventNames.ON_WRONG_MATCH);
			}

			//set to null after checking
			this.firstPicture = null;
			this.secondPicture = null;

			this.firstListener = null;
			this.secondListener = null;

		} else {
			Debug.LogWarning ("[GameMechanicHandler] Error. Both picture models are stored already!");
		}
	}


}
