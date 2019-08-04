﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    public new GameObject particleSystem;
    public Transform player;
    public Transform enemy;
    public float speed = 5.0f;
    public float radius = 5.0f;
    private float cooldown = 2.0f;

    private bool m_facingRight = false;
    private bool mooving = false;
    private bool onCooldown = true;
  
    private Vector3 destination;
    private bool first = true;
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if (mooving && !Stun)
        {
            float speedX = speed;
            if (Slow)
                speedX /= 2.0f;
            
            if (onCooldown)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0.0f)
                    onCooldown = false;
            }

            if (!onCooldown)
            {
                transform.position += (-transform.right) * Time.deltaTime * speedX;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character" && !mooving)
        {
            transform.right = -(collision.transform.position - transform.position); 

            particleSystem.SetActive(true);
            mooving = true;
        }

        if (collision.tag == "Figure" && !onCooldown)
        {
            destination = collision.transform.position;
        }
    }

    private void Flip()
    {
        m_facingRight = !m_facingRight;
        Vector3 invScale = transform.localScale;
        invScale.x *= -1;
        transform.localScale = invScale;
    }
}
