using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dyno : Toy
{
    private bool m_activation;
    public float speed = 5.0f;
    public float cooldown = 2.0f;

    void Start()
    {
        GameObject[] GO = GameObject.FindGameObjectsWithTag("Character");
        bool isfliped = GO[0].GetComponent<SpriteRenderer>().flipX;
        if (isfliped)
        {
            speed = -5.0f;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            speed = 5.0f;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_activation && cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0.0f);

            if (cooldown <= 0.0f)
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Ground")
        {
            m_activation = true;
            ReactivateCollider();
        }
    }
}
