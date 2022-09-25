using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class ColliderBridge : MonoBehaviour
 {
    public GameObject player;
    void OnTriggerStay(Collision collision)
    {
        player.GetComponent<PlayerMove>().grounded = true;
        print("col: " + Time.fixedTime);
    }
    void FixedUpdate()
    {
        player.GetComponent<PlayerMove>().grounded = false;
        print("up:  " + Time.fixedTime);
    }
 }