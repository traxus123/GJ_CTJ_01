using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float CooldownStun = 10.0f;
    public float CooldownSlow = 10.0f;

    protected bool Stun;
    protected bool Slow;

    float timeStun;
    float timeSlow;

    bool touchLego;

    protected virtual void Awake()
    {
        timeStun = 0.0f;
        timeSlow = 0.0f;
    }

    protected virtual void Update()
    {
        if (Stun)
        {
            timeStun += Time.deltaTime;
            if (timeStun > CooldownStun)
            {
                Stun = false;
                timeStun = 0.0f;

                if (touchLego)
                {
                    Slow = true;
                    touchLego = false;
                }
            }
        }

        if (Slow)
        {
            timeSlow += Time.deltaTime;
            if (timeSlow > CooldownSlow)
            {
                Slow = false;
                timeSlow = 0.0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Lego")
        {
            Stun = true;
            touchLego = true;
        }

        if (coll.collider.tag == "Figure")
        {
            Stun = true;
        }
    }
}
