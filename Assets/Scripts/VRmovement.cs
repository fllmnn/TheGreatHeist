using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRmovement : MonoBehaviour {

    public Transform vrCam;
    public float toggleAngle = 30.0f;
    public float speed = 3.0f;
    private CharacterController cc;

    public bool moveForward;
    public MovementMethod wayToMove;

    public enum MovementMethod{
        LookWalk,
        Autowalk,
        Teleport
    }

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
	}

    public void teleport(GameObject g) {
        this.transform.position = g.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if (wayToMove == MovementMethod.LookWalk)   //Just for the lols
        {
            if (vrCam.eulerAngles.x >= toggleAngle && vrCam.eulerAngles.x <= 90.0f)
            {
                moveForward = true;
            }
            else {
                moveForward = false;
            }

            if (moveForward) {
                Vector3 forward = vrCam.TransformDirection(Vector3.forward);

                cc.SimpleMove(forward * speed);
            }
        }

        if (wayToMove == MovementMethod.Autowalk)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                moveForward = !moveForward;
            }

            if (moveForward)
            {
                Vector3 forward = vrCam.TransformDirection(Vector3.forward);

                cc.SimpleMove(forward * speed);
            }
        }
	}
}
