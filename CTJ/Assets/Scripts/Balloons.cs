using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : Toy
{
    private bool m_activation = false;
    private bool destroy = false;

    public float speed = 2.0f;
    public float cooldownBeforeDestory = 2.0f;
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (m_activation && !destroy)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, speed);
        }

        if (destroy)
        {
            cooldownBeforeDestory -= Time.deltaTime;
            if (cooldownBeforeDestory <= 0)
                Destroy(gameObject);
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
            destroy = true;
        }
    }
}
