using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    public new GameObject particleSystem;
    public Transform player;
    public Transform enemy;
    public float speed = 5.0f;
    public float radius = 5.0f;
    public float cooldownBeforeAttck = 2.0f;
    public float TimeAttack = 2.0f;

    private bool m_facingRight = false;
    private bool mooving = false;
    private bool onCooldown = true;
    private float timeAtt = 0.0f;
    private float coolBef = 0.0f;

    private Vector3 destination;
    private bool first = true;
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if (mooving && !Stun)
        {
            float speedX = speed;
            if (Slow)
                speedX /= 2.0f;
            
            if (onCooldown)
            {
                coolBef += Time.deltaTime;
                if (coolBef > cooldownBeforeAttck)
                {
                    particleSystem.SetActive(true);
                    onCooldown = false;
                }
            }

            if (!onCooldown)
            {
                transform.position += (-transform.right) * Time.deltaTime * speedX;

                timeAtt += Time.deltaTime;
                if (timeAtt > TimeAttack)
                {
                    timeAtt = 0.0f;
                    onCooldown = true;
                    mooving = false;
                    particleSystem.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character" && !mooving)
        {
            transform.right = -(collision.transform.position - transform.position); 
            
            mooving = true;
        }

        if (collision.tag == "Figure" && !onCooldown)
        {
            destination = collision.transform.position;
        }

        if (collision.tag == "simpleWall")
        {
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
    }

    private void Flip()
    {
        m_facingRight = !m_facingRight;
        Vector3 invScale = transform.localScale;
        invScale.x *= -1;
        transform.localScale = invScale;
    }
}
