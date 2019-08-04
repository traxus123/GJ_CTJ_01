using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : Enemy
{
    public Transform Player;
    public Rigidbody2D Nuage1;
    public Rigidbody2D Nuage2;
    public Rigidbody2D Nuage3;
    public float Cooldown = 5.0f;
    public float speed = 5.0f;
    public Animator m_Animator;
    
    private Vector3 destination;
    private GameObject Figure;
    private Queue<Vector3> mouvmentCharacter = new Queue<Vector3>();
    private Queue<Vector3> mouvmentFigure = new Queue<Vector3>();
    public int Hit = 0;
    public bool onHit = false;
    bool firstTowards = false;
    bool m_facingRight = true;

    protected override void Awake()
    {
        base.Awake();

        destination = Player.localPosition;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Queue<Vector3> mouvement = mouvmentCharacter;

        /*Figure = GameObject.FindGameObjectWithTag("Figure");
        if (Figure != null)
            mouvement = mouvmentFigure;*/
        
        float speedX = speed;
        if (Slow)
            speedX /= 2.0f;

        if (Figure == null)
            mouvement.Enqueue(Player.position);
        else
            mouvement.Enqueue(Figure.transform.position);

        if (Cooldown > 0.0f)
        {
            Cooldown -= Time.deltaTime;
        }
        else
        {
            if (!Stun)
            {
                m_Animator.SetBool("m_Run", true);
                if (firstTowards || Vector3.Distance(transform.position, destination) < 0.1f)
                {
                    firstTowards = true;

                    Vector3 dir = transform.position - mouvement.Peek();
                    
                    if (dir.x < 0.0f && !m_facingRight)
                        Flip();
                    else if (dir.x > 0.0f && m_facingRight)
                        Flip();

                    transform.position = mouvement.Peek();                    
                    mouvement.Dequeue();
                }
                else if (!firstTowards)
                {
                    transform.localPosition = Vector3.MoveTowards(transform.position, destination, speedX * Time.deltaTime);
                }
            }
            else
                m_Animator.SetBool("m_Run", false);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Player" && !onHit)
        {
            onHit = true;
            Hit++;
        }

        if(Hit == 1)
        {
            Nuage1.velocity = new Vector2(5.0f, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(Hit == 2)
        {
            Nuage2.velocity = new Vector2(-5.0f, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(Hit == 3)
        {
            Nuage3.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 5.0f);
        }
        /*
        if (coll.collider.tag == "Lego")
        {
            Stun = true;
            touchLego = true;
        }

        if (coll.collider.tag == "Figure")
        {
            Stun = true;
        }

        if (coll.collider.tag == "Dyno")
        {
            Destroy(gameObject);
        }*/
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.tag == "Player")
        {
            onHit = false;
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



