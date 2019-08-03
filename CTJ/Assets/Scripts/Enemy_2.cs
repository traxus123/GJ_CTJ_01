using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    private bool mooving;
    private bool onCooldown;
    private int cooldown;
    private Vector3 destination;
    private float speed = 5.0f;
    private bool first = true;
    
    // Update is called once per frame
    void Update()
    {

        if (!mooving)
        {
            if (!onCooldown)
            {
                if (((player.position.x - enemy.position.x) <= 5 && (player.position.x - enemy.position.x) >= -5))
                {
                    if (((player.position.y - enemy.position.y) <= 5 && (player.position.y - enemy.position.y) >= -5))
                    {
                        destination = player.position;
                        mooving = true;
                    }
                }
            }
        }
        Debug.Log(destination);

        if (mooving)
        {
            float step = speed * Time.deltaTime;
            if (first)
            {
                step = 3.0f;
            }
            transform.position = Vector3.MoveTowards(transform.position, destination, step);

            if (enemy.position.x == destination.x && enemy.position.y == destination.y)
            {
                first = false;
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
