using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//https://www.youtube.com/watch?v=x-C95TuQtf0

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
        }
        else { return; }
    }

    public void End() {

        end = true;
        countdownText.color = Color.red;
    }
}
