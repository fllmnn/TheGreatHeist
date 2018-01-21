using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public Text highscore;

    private void Start()
    {
        highscore.text = PlayerPrefs.GetFloat("Highscore",0.00f).ToString();
    }

    public void ResetHighScore() {

        PlayerPrefs.SetFloat("Highscore", 0.00f);
        highscore.text = PlayerPrefs.GetFloat("Highscore", 0.00f).ToString();
    }

    public void StartGame() {

        //StartCoroutine(LoadScene());
        SceneManager.LoadSceneAsync("Storage01", LoadSceneMode.Single);
    }

    public IEnumerator LoadScene() {

        yield return new WaitForEndOfFrame();
        StartCoroutine(UnloadScene());
        SceneManager.LoadSceneAsync("Storage01", LoadSceneMode.Single);
        
    }

    private IEnumerator UnloadScene() {
        yield return new WaitForEndOfFrame();

        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
