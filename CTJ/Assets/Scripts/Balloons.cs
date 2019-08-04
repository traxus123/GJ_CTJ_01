using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : Toy
{
    private bool m_activation = false;

    public float m_JumpForce = 400.0f;
    public float speed = 2.0f;
    public float cooldownBeforeDestory = 2.0f;
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (m_activation)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, speed);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Ground")
        {
            m_activation = true;
            
            ReactivateCollider();
        }
        if (coll.collider.tag == "Character")
        {
            //Animation de destruction
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, m_JumpForce));
            Destroy(gameObject);
        }
    }
}
