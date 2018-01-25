using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRmovement : MonoBehaviour {

    public Transform vrCam;
    public float toggleAngle = 30.0f;
    public float speed = 3.0f;
    private CharacterController cc;
    public GameObject[] invetory;
    public GameObject[] invetorySlots;
    private int inventoryCount;
    private bool gaze = false;

    private float stepTimer;
    private float stepSoundTriggerTime = 0.75f;
    public SoundManagerScript _soundManagerScript;

    public GameObject UIText;
    private Text text;
    private double Score;

    public bool moveForward;

    // Use this for initialization
    void Start() {
        cc = GetComponent<CharacterController>();
        text = UIText.GetComponent<Text>();
        invetory = new GameObject[4];
        inventoryCount = 0;

        stepTimer = 0.0f;
    }

    // Update is called once per frame
    void Update() {


        if (Input.GetButton("Fire1") && !gaze)
        {
            moveForward = true;
        }
        else {
            moveForward = false;
        }

        if (moveForward)
        {
            Vector3 forward = vrCam.TransformDirection(Vector3.forward);

            cc.SimpleMove(forward * speed);

            stepTimer += Time.deltaTime;
            if (stepTimer >= stepSoundTriggerTime)           //Magic number noch als variable deklarieren!
            {
                _soundManagerScript.stepAudioSource.Play();
                _soundManagerScript.makeNoise(noiseValue: 0.04f);
                stepTimer = 0f;
            }
        }
    }

    public void gazeAt() {
        gaze = true;
    }

    public void stopGaze() {
        gaze = false;
    }

    public void PickUp(GameObject item) {
        if (inventoryCount < invetory.Length)
        {
            _soundManagerScript.PlayItemAudio(item);        //Ruft die entsprechende Methode im Soundmanager auf und übergibt das aufgenommene Item
            invetory[inventoryCount] = item;
            invetorySlots[inventoryCount].SetActive(true);
            invetorySlots[inventoryCount].GetComponent<Image>().sprite = item.GetComponent<ObjectsToCollect>().thumbNail;

            inventoryCount++;
            item.SetActive(false);

            gaze = false;
        }
        else {
            Debug.Log("INVENTORY FULL");
        }
    }

    public void DropOff(GameObject car) {
        //Move all Objects to car
        for (int i = 0; i < invetory.Length; i++)
        {
            if (invetory[i] != null)
            {
                invetory[i].transform.position = car.transform.position + Random.insideUnitSphere * 2;
                invetory[i].SetActive(true);
                invetory[i].GetComponent<EventTrigger>().enabled = false;
                Score += invetory[i].GetComponent<ObjectsToCollect>().moneyValue;
                invetory[i] = null;
            }

        }

        //Reset Inventory in Canvas
        foreach (GameObject item in invetorySlots)
        {
            if (item.activeSelf) {
                item.SetActive(false);
            }
        }

        text.text = Score + " €";

        if (Score > PlayerPrefs.GetFloat("Highscore", 0.00f))
        {
            PlayerPrefs.SetFloat("Highscore", (float)Score);
        }

        inventoryCount = 0;
    }
}
