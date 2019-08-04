using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    public Transform player;
    public Transform enemy;
    public float speed = 5.0f;
    public float radius = 5.0f;

    private bool mooving = false;
    private bool onCooldown = false;
    private int cooldown;
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

            transform.position = Vector3.MoveTowards(transform.position, destination, speedX * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, destination) < 1f)
            {
                mooving = false;
                onCooldown = true;
            }
        }

        if (onCooldown)
        {
            cooldown++;
            if(cooldown == 120)
            {
                onCooldown = false;
                cooldown = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character" && !mooving && !onCooldown)
        {
            destination = collision.transform.position;
            mooving = true;
        }

        if (collision.tag == "Figure" && !onCooldown)
        {
            destination = collision.transform.position;
        }
    }
}
