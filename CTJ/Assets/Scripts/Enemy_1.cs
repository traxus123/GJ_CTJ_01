using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    public Transform enemy;
    public float speed = 5.0f;
    public int m_Max = 100;
    public int m_Min = 0;

    private int step;
    private bool side;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if (!Stun)
        {
            if (side)
            {
                step++;
                if (step >= m_Max)
                {
                    side = false;
                }

                float speedX = speed; 
                if (Slow)
                    speedX /= 2.0f;

                GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                step--;
                if (step <= m_Min)
                {
                    side = true;
                }

                float speedX = -speed;
                if (Slow)
                    speedX /= 2.0f;

                GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }
}
