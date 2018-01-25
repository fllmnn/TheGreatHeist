using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public bool moveForward;

    // Use this for initialization
    void Start() {
        cc = GetComponent<CharacterController>();
        invetory = new GameObject[4];
        inventoryCount = 0;

        stepTimer = 0.0f;
    }

    public void teleport(GameObject g) {
        this.transform.position = g.transform.position;
    }

    // Update is called once per frame
    void Update() {


        if (Input.GetButton("Fire1") && !gaze)
        {
            moveForward = true;
            Debug.Log(moveForward);
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
        Debug.Log("Gazin");
        gaze = true;
    }

    public void stopGaze() {
        Debug.Log("stop Gazin");
        gaze = false;
    }

    public void PickUp(GameObject item) {
        if (inventoryCount < invetory.Length)
        {
            _soundManagerScript.PlayItemAudio(item);        //Ruft die entsprechende Methode im Soundmanager auf und übergibt das aufgenommene Item
            invetory[inventoryCount] = item;
            invetorySlots[inventoryCount].SetActive(true);
            inventoryCount++;
            item.SetActive(false);

            Debug.Log("stop Gazin");
            gaze = false;
        }
        else {
            Debug.Log("INVENTORY FULL");
        }
    }

    public void DropOff(GameObject car) {
        //Move all Objects to car
        foreach (GameObject item in invetory) {
            item.transform.position = car.transform.position;
            item.SetActive(true);
        }

        //Reset Inventory in Canvas
        foreach (GameObject item in invetorySlots)
        {
            if (item.activeSelf) {
                item.SetActive(false);
            }
        }

        inventoryCount = 0;
    }
}
