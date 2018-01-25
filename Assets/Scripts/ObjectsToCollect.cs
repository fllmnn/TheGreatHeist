using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsToCollect : MonoBehaviour {

    public float noiseFactor;
    public double moneyValue = 1.99;
    string description;
    public Sprite thumbNail;
    public AudioClip audioClip;

    //void OnTriggerEnter(Collider player) {
    //    if (player.gameObject.tag == "Player") {
    //        VRmovement vRmovement = player.GetComponent<VRmovement>();
    //        vRmovement.PickUp(this.gameObject);
    //    }     
    //}

}
