﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 3;
    public float acceleration = 1f;
    public float deAcceleration = 2f;
    public float stopSpeed = 0.001f;

    public float maxSoundSpeed = 3;
    public float minSoundSpeed = 1;

    public float audioAccelerateTime = 2;

    float curAcceleratingTime;

    public bool debug;

    bool breakButton = false;
    AudioSource bubbleAudio;
    ParticleSystem bubbleSystem;
    bool flipX = false;

    bool dead = false;
    
    public Rigidbody2D rb;

    void Start()
    {
        bubbleAudio = GetComponent<AudioSource>();
        bubbleSystem = gameObject.transform.Find("Bubbles").GetComponent<ParticleSystem>();

        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //print("collision: " + collision);
        //this.speed.Set(0, 0, 0);
    }
    
    void Update()
    {
        bool isAccelerating = false;

        var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        direction = direction.normalized;

        if(direction.x != 0)
        {
            flipX = direction.x < 0;
            if(flipX)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }       
        }

        if (Input.GetKeyDown("space"))
        {
            breakButton = true;
        }

        if(Input.GetKeyUp("space"))
        {
            breakButton = false;
        }

        { 
            var tempDirection = direction;
            var tempAccel = acceleration;
            //stop our current acceleration
            if ((this.dead || breakButton || direction.normalized.magnitude == 0))
            {
                if (this.rb.velocity.magnitude > 0) {
                    if (debug)
                    {
                        print("speed: " + this.rb.velocity);
                    }
                    //start decaying
                    tempDirection = -this.rb.velocity.normalized;
                    tempAccel = deAcceleration;
                }
            } else
            {
                if (debug)
                {
                    print("accelerating");
                }
                isAccelerating = true;
            }

            if (this.debug)
            {
                print("tempAccel: " + tempAccel);
                print("tempDirection: " + tempDirection);
            }

            if (!this.dead)
            {
                this.AddForce(tempDirection * tempAccel);
            }
        }

        if (debug)
        {
            print("transform: " + this.transform.position);
            print("speed: " + this.rb.velocity);
        }

        
        //update the playback speed of the sound
        {
            if (isAccelerating)
            {
                this.curAcceleratingTime += Time.deltaTime;
            }
            else
            {
                this.curAcceleratingTime -= Time.deltaTime;
            }

            if(this.curAcceleratingTime < 0)
            {
                this.curAcceleratingTime = 0;
            }

            if(this.curAcceleratingTime > this.audioAccelerateTime)
            {
                this.curAcceleratingTime = this.audioAccelerateTime;
            }

            float soundSpeed = this.minSoundSpeed;

            float additionalSpeed = (this.maxSoundSpeed - this.minSoundSpeed) * Mathf.Min(1, this.curAcceleratingTime / this.audioAccelerateTime);

            soundSpeed = soundSpeed + additionalSpeed;
            bubbleAudio.pitch = soundSpeed;
        }

    }

    void AddForce(Vector2 acc)
    {
        // add the acceleration value
        var tempSpeed = this.rb.velocity + acc * Time.deltaTime;

        //make sure we don't fly too fast
        if(tempSpeed.magnitude > maxSpeed)
        {
            tempSpeed.Normalize();
            tempSpeed.Scale(new Vector2(maxSpeed, maxSpeed));
            if (debug)
            {
                print(tempSpeed);
            }
        }

        this.rb.velocity = tempSpeed;
    }

    public void Die()
    {
        this.dead = true;
    }

}
