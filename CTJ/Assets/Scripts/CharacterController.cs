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

    bool m_Hide = false;
    bool m_FacingRight = true;
    Rigidbody2D m_Rigidbody2D;
    bool m_Grounded = false;
    float m_GroundRadius = 0.01f;

    HandBehaviour handLeft = null;
    HandBehaviour handRight = null;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Check ground
        m_Grounded = Physics2D.OverlapCircle(m_GroundCheck.position, m_GroundRadius, m_WatIsGround);
        
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
        
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 invScale = transform.localScale;
        invScale.x *= -1;
        transform.localScale = invScale;
    }

}
