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

    public IEnumerator Wait() {
        yield return new WaitForSecondsRealtime(8);

    }

    public void DropOff(GameObject car) {
        //Move all Objects to car (GameObject item in invetory)
        for (int i = 0; i < invetory.Length; i++) {
            if (invetory[i] != null)
            {
                StartCoroutine(Wait());

                invetory[i].transform.position = car.transform.position;
                invetory[i].SetActive(true);
                invetory[i].GetComponent<EventTrigger>().enabled = false;
                Score += invetory[i].GetComponent<ObjectsToCollect>().value;
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

        if (Score > PlayerPrefs.GetFloat("Highscore", 0.00f)) {
            PlayerPrefs.SetFloat("Highscore", (float)Score);
        }
        
        inventoryCount = 0;
    }
}
