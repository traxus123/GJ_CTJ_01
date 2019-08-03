using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dyno : MonoBehaviour
{
    private bool m_activation;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_activation)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Ground")
        {
            m_activation = true;
        }
    }
}
