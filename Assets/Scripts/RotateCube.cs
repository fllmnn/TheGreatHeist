using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour {

    public float yAmount = 1;

	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, yAmount, 0, Space.Self);	
    }
}
