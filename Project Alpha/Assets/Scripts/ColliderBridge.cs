using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class ColliderBridge : MonoBehaviour
 {
    public GameObject player;
    bool buffer;
    void OnTriggerStay(Collider other)
    {
        player.GetComponent<PlayerMove>().grounded = true;
        buffer = true;
        print("col: " + Time.fixedTime);
    }
    void FixedUpdate()
    {
        if(!buffer)
        {
            player.GetComponent<PlayerMove>().grounded = false;
        }
        else
        {
            buffer=false;
        }
        print("up:  " + Time.fixedTime);
    }
 }