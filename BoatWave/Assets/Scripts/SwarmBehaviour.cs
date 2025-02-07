﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmBehaviour : MonoBehaviour {

    public float strayFromCenter = 2;

    new protected Rigidbody2D rigidbody;
    protected float directionTime = -1;
    protected Vector3 direction = Vector3.zero;
    protected Vector3 center;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        center = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(directionTime < Time.time)
        {
            directionTime = Time.time + Random.Range(.5f, 3f);
            direction = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
            rigidbody.velocity = direction;
            transform.rotation = Quaternion.Euler(direction / direction.magnitude);
        }
        Vector2 centerDirection = (center - transform.position);
        if (centerDirection.magnitude > strayFromCenter)
        {
            direction = centerDirection;
            rigidbody.velocity = direction;
            transform.rotation = Quaternion.Euler(direction / direction.magnitude);
        }
	}
}
