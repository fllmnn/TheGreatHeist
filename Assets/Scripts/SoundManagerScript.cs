using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    //GameMusic
    public AudioSource gameMusicSource;
    public AudioClip gameMusicClip;

    //ItemSoundkram        
    public AudioSource itemSoundAudioSource;

    public AudioClip coinClip;                  
    public AudioClip cashMoneyClip;
    public AudioClip gemäldeClip;
    public AudioClip gemClip;
    public AudioClip goldbarrenClip;
    public AudioClip klopapierClip;
    public AudioClip truheClip;
    public AudioClip uhrClip;

    // NoiseOMeterkram...
    public GameObject blackBar;
    private float maxNoise = 1.00f;            
    private float time;
    private float noiseDroprate = 0.02f;
    public float currentNoise;
    public AlarmTrigger _alarmTrigger;

    //Stepsounds        Das Zeittracking und der Trigger zum Sound abspielen werden in VRMovement abgehandelt
    public AudioSource stepAudioSource;
    public AudioClip stepClip;

    // Use this for initialization
    void Start () {
        currentNoise = 0.00f;
        stepAudioSource.clip = stepClip;

        gameMusicSource.clip = gameMusicClip;
        gameMusicSource.Play();
    }
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;         //handelt die NoiseOMeterskalierung ab

        if (time >= 1.0f)                                                               
        {
            currentNoise -= noiseDroprate;

            if (currentNoise <= 0f)
            {
                currentNoise = 0f;
                blackBar.transform.localScale = new Vector3(1, 1, 1);
            }
            blackBar.transform.localScale = new Vector3(1, maxNoise - currentNoise, 1);
            time = 0f;
        }

        
    }

    public void makeNoise(float noiseValue)
    {
        currentNoise += noiseValue;
        if (currentNoise >= maxNoise)
        {
            currentNoise = maxNoise;
            blackBar.transform.localScale = new Vector3(1, 0, 1);
            _alarmTrigger.TriggerAlarm();       //Das AlarmObject spielt den Alarmsound ab.
        }
        else
        {
            blackBar.transform.localScale = new Vector3(1, maxNoise - currentNoise, 1);
        }
    }

    public void PlayItemAudio(GameObject item)      //Funktion wird in VRMovement beim aufsammeln des jeweiligen Items aufgerufen
    {                                                  //magic numbers noch deklarieren
        itemSoundAudioSource.clip = item.GetComponent<ObjectsToCollect>().audioClip;

        itemSoundAudioSource.Play();
        makeNoise(item.GetComponent<ObjectsToCollect>().noiseFactor);
    }
}
