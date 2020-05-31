using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeating : MonoBehaviour
{
    public float multiplicationmove = 3;
    private BoxCollider2D m_BackgroundCollider;
    private float m_GroundHorizontalLength;

    // Use this for initialization
    void Start()
    {
        m_BackgroundCollider = GetComponent<BoxCollider2D>();
        m_GroundHorizontalLength = m_BackgroundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -m_GroundHorizontalLength - 4) RepositionBackground();
    }

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(m_GroundHorizontalLength * multiplicationmove, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
