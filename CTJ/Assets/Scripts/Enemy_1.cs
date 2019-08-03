using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public Transform enemy;
    public int m_Max = 100;
    public int m_Min = 0;

    private int step;
    private bool side;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (side)
        {
            step++;
            if (step >= m_Max)
            {
                side = false;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            step--;
            if (step <= m_Min)
            {
                side = true;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, GetComponent<Rigidbody2D>().velocity.y);
        }
        //Debug.Log(enemy.position);
    }
}
