using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseScript : MonoBehaviour {

    private float maxNoise = 1.00f;
    private float time;
    private float noiseDroprate = 0.01f;

    public float currentNoise;

    public AlarmTrigger _alarmTrigger;
    


	// Use this for initialization
	void Start () {
        currentNoise = 0.00f;
        //AlarmText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;

        if (time >= 1.0f)                                                               //Boah, bestimmt voll ineffizient. Aber es funktioniert erstmal.
        {
            currentNoise -= noiseDroprate;
            if(currentNoise <= 0f)
            {
                currentNoise = 0f;
                transform.localScale = new Vector3(1, 0, 1);
            }
            transform.localScale = new Vector3(1, (currentNoise / maxNoise), 1);
            time = 0f;
        }

            if (Input.GetMouseButtonDown(0))
        {
            makeNoise(0.1f);
        }


	}


    public void makeNoise(float noiseValue)
    {
        currentNoise += noiseValue;
        if (currentNoise >= maxNoise)
        {
            currentNoise = maxNoise;
            transform.localScale = new Vector3(1, 1, 1);
            _alarmTrigger.TriggerAlarm();
        }
        else
        {  
            transform.localScale = new Vector3(1, (currentNoise / maxNoise), 1);
        }  
    }
}
