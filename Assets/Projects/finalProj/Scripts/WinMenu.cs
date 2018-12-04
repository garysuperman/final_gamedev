using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour {

    [SerializeField] public GameObject treant;

    [SerializeField] public GameObject winMenuUI;

    [SerializeField] public GameObject deathMenuUI;

    void Update()
    {
        if (treant.GetComponent<treant_script>().healthSystem.GetHealth() <= 0 && !deathMenuUI.activeSelf)
        {
            winMenuUI.SetActive(true);
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
