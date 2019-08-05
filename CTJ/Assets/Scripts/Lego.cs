using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lego : Toy
{
    public AudioClip LegoThrow;

    private void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(LegoThrow);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
