﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetheredCamera : MonoBehaviour {

    public GameObject player;
    public float tetherStrength = 100;

	// Use this for initialization
	void Start ()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.z = -10;
        this.transform.position = playerPos;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void LateUpdate()
    {
        if(player != null)
        {
            Vector3 playerPos = player.transform.position;
            playerPos.z = -10;
            Vector3 offset = playerPos - this.transform.position;
            if (offset.magnitude > .001)
            {
                this.transform.position += offset / this.tetherStrength;
            }
        } 
    }
}
