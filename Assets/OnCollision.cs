﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public GameObject card = null;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnColllisiionEnter");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1Cards") {
            Debug.Log("Colided");
            card = other.gameObject;
        }
    }
}
