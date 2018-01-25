using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneControl : MonoBehaviour {

    public Transform Crane;
    public float speed = 1f;

    private bool isForwardPressed = false;
    private bool isBackwardPressed = false;
    private bool isRightPressed = false;
    private bool isLeftPressed = false;

    private void Update()
    {
        if (isForwardPressed) {
            Crane.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (isBackwardPressed)
        {
            Crane.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (isRightPressed)
        {
            Crane.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (isLeftPressed)
        {
            Crane.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    public void onForwardDown()
    {
        isForwardPressed = true;
    }
    public void onForwardUp()
    {
        isForwardPressed = false;
    }

    public void onBackwardDown()
    {
        isBackwardPressed = true;
    }
    public void onBackwardUp()
    {
        isBackwardPressed = false;
    }

    public void onRightDown()
    {
        isRightPressed = true;
    }
    public void onRightUp()
    {
        isRightPressed = false;
    }

    public void onLeftDown()
    {
        isLeftPressed = true;
    }
    public void onLeftUp()
    {
        isLeftPressed = false;
    }
}
