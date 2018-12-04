using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour {

    [SerializeField] public GameObject player;

    [SerializeField] public GameObject deathMenuUI;

    [SerializeField] public GameObject winMenuUI;

    // Update is called once per frame
    void Update () {
		if (player.GetComponent<PlayerScript>().healthSystem.GetHealth() <= 0 && !winMenuUI.activeSelf)
        {
            deathMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
	}

    public void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        LoadManager.Instance.LoadScene(SceneNames.MAIN_MENU_SCENE);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

}
