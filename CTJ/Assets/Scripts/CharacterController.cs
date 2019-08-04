using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float m_MaxSpeed = 10.0f;
    public float m_JumpForce = 300.0f;
    public float m_JumpForcePogo = 600.0f;
    public float m_WhenHideSpeedDividedBy = 2.0f;
    public HandBehaviour m_HandPrefab;
    public Animator m_Animator;
    public Toy[] m_ToysPrefab;
    public UIBag m_UIBag;

    bool[] m_UsedToys;
    bool m_OnPogostick;
    int m_IndexToy;
    bool m_Hide = false;
    bool m_FacingRight = true;
    Rigidbody2D m_Rigidbody2D;
    bool m_Grounded = false;
    public float m_GroundRadius = 0.01f;
    int m_handCooldown = 0;

    HandBehaviour handLeft = null;
    HandBehaviour handRight = null;

    private void Awake()
    {
        m_OnPogostick = false;
        m_IndexToy = 0;
        if (m_UIBag != null)
            m_UIBag.ChangedToy(m_IndexToy, false);

        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        m_UsedToys = new bool[m_ToysPrefab.Length];
        for (int i = 0; i < m_UsedToys.Length; i++)
            m_UsedToys[i] = false;
    }

    private void FixedUpdate()
    {
        //Move
        // Add anim for moving
        float move = Input.GetAxis("Horizontal");

        float velX = move * m_MaxSpeed;
        if (m_Hide)
            velX = move * m_MaxSpeed / m_WhenHideSpeedDividedBy;
        
        if (m_Animator != null)
        {
            if (move != 0)
                m_Animator.SetBool("m_Run", true);
            else
                m_Animator.SetBool("m_Run", false);
        }

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

            if (m_OnPogostick)
            {
                m_Rigidbody2D.AddForce(new Vector2(0, m_JumpForce));
                m_OnPogostick = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && !m_Hide && handRight == null) // Hide 
        {
            m_Hide = true;
            handRight = Instantiate<HandBehaviour>(m_HandPrefab, new Vector3(6.8f, -10.0f, -5.0f), new Quaternion());
            handLeft = Instantiate<HandBehaviour>(m_HandPrefab, new Vector3(-6.8f, -10.0f, -5.0f), Quaternion.Euler(0, 180.0f, 0));
        }

        if (Input.GetKeyUp(KeyCode.S) && m_Hide) // Show
        {
            m_Hide = false;
            StartCoroutine(handRight.Disappear());
            StartCoroutine(handLeft.Disappear());
        }
        if(m_Hide && m_handCooldown == 300)
        {
            m_Hide = false;
            StartCoroutine(handRight.Disappear());
            StartCoroutine(handLeft.Disappear());
            m_handCooldown = 0;
        }
        if (m_Hide)
        {
            m_handCooldown++;
        }

        if (m_ToysPrefab != null && Input.GetMouseButtonDown(0) && !m_UsedToys[m_IndexToy])
        {
            if (m_ToysPrefab[m_IndexToy].name == "Pogostick")
            {
                m_OnPogostick = true;
                m_UsedToys[m_IndexToy] = true;
            }
            else
            {
                if (m_FacingRight)
                    Flip();

                Toy lego = Instantiate(m_ToysPrefab[m_IndexToy]);
                m_UsedToys[m_IndexToy] = true;

                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), lego.GetComponent<BoxCollider2D>());

                Vector3 newPos = transform.position;
                newPos.z = 5.0f;
                lego.transform.position = newPos;
                lego.LaunchLeft();
            }
        }
        
        if (m_ToysPrefab != null && Input.GetMouseButtonDown(1) && !m_UsedToys[m_IndexToy])
        {
            if (m_ToysPrefab[m_IndexToy].name == "Pogostick")
            {
                m_OnPogostick = true;
                m_UsedToys[m_IndexToy] = true;
            }
            else
            {
                if (!m_FacingRight)
                    Flip();

                Toy lego = Instantiate(m_ToysPrefab[m_IndexToy]);
                m_UsedToys[m_IndexToy] = true;

                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), lego.GetComponent<BoxCollider2D>());

                Vector3 newPos = transform.position;
                newPos.z = 5.0f;
                lego.transform.position = newPos;
                lego.LaunchRight();
            }
        }

        if (m_ToysPrefab != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                m_IndexToy = 0;

            if (Input.GetKeyDown(KeyCode.Alpha2))
                m_IndexToy = 1;

            if (Input.GetKeyDown(KeyCode.Alpha3))
                m_IndexToy = 2;
            
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                int updateIndex = (int)(Input.GetAxis("Mouse ScrollWheel") * 10 % 3);
                m_IndexToy += updateIndex;

                if (m_IndexToy < 0)
                    m_IndexToy = m_ToysPrefab.Length - 1;
                else if (m_IndexToy > m_ToysPrefab.Length - 1)
                    m_IndexToy = 0;
            }

            if (m_UIBag != null)
                m_UIBag.ChangedToy(m_IndexToy, m_UsedToys[m_IndexToy]);

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

        if (coll.collider.tag == "Enemy" && !m_Hide)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
