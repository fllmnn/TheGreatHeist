using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToCollect : MonoBehaviour {

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.tag == "Player") gameObject.SetActive(false);
    }
}
