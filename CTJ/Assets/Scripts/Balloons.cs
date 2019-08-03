using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : MonoBehaviour
{
    public Transform m_Balloon;
    private bool m_activation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_activation)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 2.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Ground")
        {
            m_activation = true;
        }
        if (coll.collider.tag == "Player")
        {
            //Animation de destruction
        }
    }
}
