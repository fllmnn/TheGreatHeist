using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmTrigger : MonoBehaviour
{

    public AudioSource alarmAudioSource;
    public AudioClip alarmClip;
    private Text alarmText;
    public bool alarmBool = false;

    // Use this for initialization
    void Start()
    {
        alarmAudioSource.clip = alarmClip;
        alarmText = GetComponent<Text>();
        alarmText.enabled = false;
    }

    public void TriggerAlarm()
    {
        alarmText.enabled = true;
        alarmAudioSource.Play();
    }


}
