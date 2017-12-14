using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmTrigger : MonoBehaviour {

    private GvrAudioSource alarmAudio;
    private Text alarmText;
    public bool alarmBool = false;

    // Use this for initialization
    void Start () {
        alarmAudio = GetComponent<GvrAudioSource>();
        alarmText = GetComponent<Text>();
        alarmText.enabled = false;
    }

    public void TriggerAlarm()
    {
        alarmText.enabled = true;
        alarmAudio.Play();
    }
	

}
