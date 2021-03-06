﻿//Author: Marcel Trattner
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class VRmovement : MonoBehaviour {

    //Fields for movement and inventory
    public Transform vrCam;
    public float speed = 3.0f;
    private CharacterController cc;
    public GameObject[] invetory;
    public GameObject[] invetoryGraphics;
    private int inventoryCount;
    private bool gaze = false;

    private float stepTimer;
    private float stepSoundTriggerTime = 0.75f;
    public SoundManagerScript _soundManagerScript;

    //Fields for tracking highscore
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

        //Set MoveForward true if we dont look at an item and right click
        if (Input.GetButton("Fire1") && !gaze)
        {
            moveForward = true;
        }
        else {
            moveForward = false;
        }

        //Moving via character controller
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

    //OnPointerEnter on every object to check if we are looking at something
    public void gazeAt() {
        gaze = true;
    }

    //OnPointerEnter on every object to check if we are looking at something
    public void stopGaze() {
        gaze = false;
    }


    public void PickUp(GameObject item) {
        if (inventoryCount < invetory.Length)
        {
            _soundManagerScript.PlayItemAudio(item);        //Ruft die entsprechende Methode im Soundmanager auf und übergibt das aufgenommene Item
            invetory[inventoryCount] = item;                //places item into Inventory
            invetoryGraphics[inventoryCount].SetActive(true);       //activates the Canvas image
            invetoryGraphics[inventoryCount].GetComponent<Image>().sprite = item.GetComponent<ObjectsToCollect>().thumbNail;       //Sets the right image in the canvas

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
                invetory[i].transform.position = car.transform.position + Random.insideUnitSphere * 2;      //places all items at the drop off point with a random offset
                invetory[i].SetActive(true);
                invetory[i].GetComponent<EventTrigger>().enabled = false;
                Score += invetory[i].GetComponent<ObjectsToCollect>().moneyValue;
                invetory[i] = null;
            }

        }

        //Reset Inventory in Canvas
        foreach (GameObject item in invetoryGraphics)
        {
            if (item.activeSelf) {
                item.SetActive(false);
            }
        }

        text.text = Score + " €";

        //sets the new highscore if current score is bigger than the old highscore
        if (Score > PlayerPrefs.GetFloat("Highscore", 0.00f))
        {
            PlayerPrefs.SetFloat("Highscore", (float)Score);
        }

        inventoryCount = 0;
    }
}
