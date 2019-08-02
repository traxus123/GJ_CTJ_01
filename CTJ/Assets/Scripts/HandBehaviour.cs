using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBehaviour : MonoBehaviour
{
    public float m_FinishPosY = 5.0f;
    public float m_FinishScale = 5.0f;

    public float m_SpeedY = 0.1f;
    public float m_SpeedScale = 0.2f;

    bool Appearing = false;

    private void Awake()
    {
        StartCoroutine(Appear());
    }

    // Maybe a bit of clean
    public IEnumerator Appear()
    {
        Appearing = true;
        while (m_FinishPosY > transform.localPosition.y)
        {
            Vector3 newPos = transform.localPosition;
            newPos.y += m_SpeedY * Time.deltaTime;
            this.transform.localPosition = newPos;

            if (m_FinishScale > transform.localScale.x && -5.0f < transform.localPosition.y)
            {
                Vector3 newScale = transform.localScale;
                newScale.x += m_SpeedScale * Time.deltaTime;
                newScale.y += m_SpeedScale * Time.deltaTime;
                transform.localScale = newScale;
            }
            yield return null;
        }
        Appearing = false;
    }

    public IEnumerator Disappear()
    {
        while (Appearing) yield return null;
        while (-m_FinishPosY < transform.localPosition.y)
        {
            yield return null;
            Vector3 newPos = transform.localPosition;
            newPos.y -= m_SpeedY * Time.deltaTime;
            this.transform.localPosition = newPos;

            if (-m_FinishScale < transform.localScale.x && 5.0f > transform.localPosition.y)
            {
                Vector3 newScale = transform.localScale;
                newScale.x -= m_SpeedScale * Time.deltaTime;
                newScale.y -= m_SpeedScale * Time.deltaTime;
                transform.localScale = newScale;
            }
        }
        Destroy(gameObject);
    }
}
