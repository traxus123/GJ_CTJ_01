using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : Toy
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
