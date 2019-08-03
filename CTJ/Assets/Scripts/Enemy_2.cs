using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
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
    void Update()
    {
        if (!mooving)
        {
            if (!onCooldown)
            {
                if (((player.position.x - enemy.position.x) <= radius && (player.position.x - enemy.position.x) >= -radius))
                {
                    if (((player.position.y - enemy.position.y) <= radius && (player.position.y - enemy.position.y) >= -radius))
                    {
                        destination = player.position;
                        mooving = true;
                    }
                }
            }
        }

        if (mooving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            
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
}
