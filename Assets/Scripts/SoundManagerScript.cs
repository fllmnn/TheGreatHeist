//Author: Konstantin Hofmann
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManagerScript : MonoBehaviour {

    //GameMusic
    public AudioSource gameMusicSource;
    public AudioClip gameMusicClip;

    //ItemSoundkram and ChaChingClip for dropoff
    public AudioSource itemSoundAudioSource;
    public AudioClip chaChingClip;

    // NoiseOMeterkram...
    public GameObject blackBar;
    private float maxNoise = 1.00f;            
    private float noiseOMeterTime;
    private float noiseOMeterDroprateTriggerTime = 1.0f;
    private float noiseDroprate = 0.02f;
    public float currentNoise;
    public AlarmTrigger _alarmTrigger;

    //Stepsounds        
    public AudioSource stepAudioSource;
    public AudioClip stepClip;
    private float stepTimer;
    private float stepSoundTriggerTime = 0.75f;
    public VRmovement _VRmovementScript;

    // Use this for initialization
    void Start () {
        currentNoise = 0.00f;
        stepAudioSource.clip = stepClip;

        gameMusicSource.clip = gameMusicClip;
        gameMusicSource.Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (_VRmovementScript.moveForward)          //Stepsounds
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepSoundTriggerTime)           
            {
                if (!stepAudioSource.isPlaying)
                {
                    stepAudioSource.Play();
                }
                makeNoise(noiseValue: 0.04f);
                stepTimer = 0f;
            }  
        }

        if (!_VRmovementScript.moveForward)
        {
            noiseOMeterTime += Time.deltaTime;
            stepAudioSource.Stop();

            if (noiseOMeterTime >= noiseOMeterDroprateTriggerTime)            //Noisedrop wird hier abgehandelt                                                   
            {
                currentNoise -= noiseDroprate;

                if (currentNoise <= 0f)
                {
                    currentNoise = 0f;
                    blackBar.transform.localScale = new Vector3(1, 1, 1);
                }
                blackBar.transform.localScale = new Vector3(1, maxNoise - currentNoise, 1);
                noiseOMeterTime = 0f;
            }
        }
    }

    public void makeNoise(float noiseValue)
    {
        currentNoise += noiseValue;
        if (currentNoise >= maxNoise)
        {
            currentNoise = maxNoise;
            blackBar.transform.localScale = new Vector3(1, 0, 1);
            //_alarmTrigger.TriggerAlarm();       //Das AlarmObject spielt den Alarmsound ab.
        }
        if (currentNoise <= 0f)
        {
            currentNoise = 0f;
            blackBar.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            blackBar.transform.localScale = new Vector3(1, maxNoise - currentNoise, 1);
        }
    }

    public void PlayItemAudio(GameObject item)      //Funktion wird in VRMovement beim aufsammeln des jeweiligen Items aufgerufen
    {                                                 
        itemSoundAudioSource.clip = item.GetComponent<ObjectsToCollect>().audioClip;
        itemSoundAudioSource.Play();
        makeNoise(item.GetComponent<ObjectsToCollect>().noiseFactor);
    }

    //is called by VRMovement at ItemDropOff and uses the itemSoundSource as SoundSource
    public void PlayChaChing()
    {
        itemSoundAudioSource.clip = chaChingClip;
        itemSoundAudioSource.Play();
    }
}
