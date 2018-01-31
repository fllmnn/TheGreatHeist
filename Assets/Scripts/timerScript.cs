//Author: Graciel Fellmann
//Tutorial source: https://www.youtube.com/watch?v=x-C95TuQtf0
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class timerScript : MonoBehaviour {

    public Text countdownText;
    private float startTime;
    public float maxTime = 300;
    private bool end = false;
    public GameObject player;

	// Use this for initialization
	void Start () {

        startTime = Time.time;

	}

    // Update is called once per frame
    void Update()
    {
        float timePassed = Time.time - startTime;
        float timeLeft = maxTime - timePassed;
        if(!end)
        {
            string min = ((int)timeLeft / 60).ToString();
            string sec = (timeLeft % 60).ToString("f1");
            countdownText.text = min + ":" + sec;
            if(timeLeft < 60)
            {
                countdownText.color = Color.yellow;
            }
            if(timeLeft < 30)
            {
                countdownText.color = Color.red;
            }
            if(timeLeft <= 0)
            {
                timeLeft = 0;
                end = true;
                //sets the new highscore if current score is bigger than the old highscore
                if (player.GetComponent<VRmovement>().finalScore > PlayerPrefs.GetFloat("Highscore", 0.00f))
                {
                    PlayerPrefs.SetFloat("Highscore", (float)player.GetComponent<VRmovement>().finalScore);
                }
                SceneManager.LoadSceneAsync("WinningScreen", LoadSceneMode.Single);
            }
        }
       
    }

    public void End() {

        end = true;
        countdownText.color = Color.red;
    }
}
