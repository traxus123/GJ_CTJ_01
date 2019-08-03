using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float m_MaxSpeed = 10.0f;
    public float m_JumpForce = 300.0f;
    public float m_WhenHideSpeedDividedBy = 2.0f;
    public Transform m_GroundCheck;
    public LayerMask m_WatIsGround;
    public HandBehaviour m_HandPrefab;

    public Toy m_LegoPrefab;

    bool m_Hide = false;
    bool m_FacingRight = true;
    Rigidbody2D m_Rigidbody2D;
    bool m_Grounded = false;
    public float m_GroundRadius = 0.01f;

    HandBehaviour handLeft = null;
    HandBehaviour handRight = null;

    private void Awake()
    {

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Check ground
        
        Debug.Log("m_Grounded " + m_Grounded);

        //Move
        // Add anim for moving
        float move = Input.GetAxis("Horizontal");

        float velX = move * m_MaxSpeed;
        if (m_Hide)
            velX = move * m_MaxSpeed / m_WhenHideSpeedDividedBy;

        m_Rigidbody2D.velocity = new Vector2(velX, m_Rigidbody2D.velocity.y);

        // Flip character
        if (move > 0 && !m_FacingRight)
            Flip();
        else if (move < 0 && m_FacingRight)
            Flip();
    }

    private void Update()
    {
        if (m_Grounded && Input.GetKeyDown(KeyCode.Space)) // Jump
        {
            // Add anim for jump
            if (!m_Hide)
                m_Rigidbody2D.AddForce(new Vector2(0, m_JumpForce));
        }

        if (Input.GetKeyDown(KeyCode.S) && !m_Hide && handRight == null) // Hide 
        {
            m_Hide = true;
            handRight = Instantiate<HandBehaviour>(m_HandPrefab, new Vector3(6.8f, -10.0f, -2.0f), new Quaternion());
            handLeft = Instantiate<HandBehaviour>(m_HandPrefab, new Vector3(-6.8f, -10.0f, -2.0f), Quaternion.Euler(0, 180.0f, 0));
        }

        if (Input.GetKeyUp(KeyCode.S) && m_Hide) // Show
        {
            m_Hide = false;
            StartCoroutine(handRight.Disappear());
            StartCoroutine(handLeft.Disappear());
        }

        if (m_LegoPrefab != null && Input.GetMouseButtonDown(0))
        {
            Toy lego = Instantiate(m_LegoPrefab);
            Vector3 newPos = transform.position;
            newPos.z = -1.0f;
            lego.transform.position = newPos;
            lego.LaunchRight();
        }
        
        if (m_LegoPrefab != null && Input.GetMouseButtonDown(1))
        {
            Toy lego = Instantiate(m_LegoPrefab);
            Vector3 newPos = transform.position;
            newPos.z = -1.0f;
            lego.transform.position = newPos;
            lego.LaunchLeft();
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 invScale = transform.localScale;
        invScale.x *= -1;
        transform.localScale = invScale;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Ground")
        {
            m_Grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.tag == "Ground")
        {
            m_Grounded = false;
        }
    }

}
