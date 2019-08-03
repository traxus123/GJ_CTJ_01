using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    public Vector2 forceWhenLaunched;

    Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void LaunchRight()
    {
        m_Rigidbody2D.AddForce(forceWhenLaunched);
    }

    public void LaunchLeft()
    {
        Vector2 InvForce = forceWhenLaunched;
        InvForce.x *= -1;
        m_Rigidbody2D.AddForce(InvForce);
    }
}
