using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeScript : MonoBehaviour {

    public GameObject keypad;
    public Transform door;
    public Text text;
    private string numbersTyped = "";
    public string combination = "0000";
    private Collider keypadTrigger;


    private void Start()
    {
        keypadTrigger = GetComponent<BoxCollider>();
    }
    // Update is called once per frame
    void Update () {
        text.text = numbersTyped;

        if (text.text == combination) {
            keypad.SetActive(false);
            keypadTrigger.enabled = false;

            float angle = Mathf.LerpAngle(0.0f, 70, Time.time * 0.1f);
            door.eulerAngles = new Vector3(0, angle, 0);
        }
        
    }

    public void showKeypad() {
        Debug.Log("show keyboard");
        keypad.SetActive(true);
    }

    public void typeNumber(int num) {
        Debug.Log("number ooone");
        if (numbersTyped.Length < 4)
        {
            numbersTyped += num;
        }
        
    }

    public void clearKeypad() {
        numbersTyped = "";
    }
}
