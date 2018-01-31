//Author: Graciel Fellmann
//Tutorial source: https://www.youtube.com/watch?v=x-C95TuQtf0
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timerScript : MonoBehaviour {

    public Text countdownText;
    private float startTime;
    public float maxTime = 300;
    private bool end = false;

	// Use this for initialization
	void Start () {

        startTime = Time.time;
	}

    // Update is called once per frame
    void Update()
    {
        float timePassed = Time.time - startTime;
        float timeLeft = maxTime - timePassed;

        if (!end)
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
        }
        else { return; }
    }

    public void End() {

        end = true;
        countdownText.color = Color.red;
    }
}
