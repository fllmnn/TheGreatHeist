using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToCollect : MonoBehaviour {

    public float xAmount = 1;

    //void OnTriggerEnter(Collider player) {
    //    if (player.gameObject.tag == "Player") {
    //        VRmovement vRmovement = player.GetComponent<VRmovement>();
    //        vRmovement.PickUp(this.gameObject);
    //    }     
    //}

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(xAmount, 0, 0, Space.Self);
    }

}
