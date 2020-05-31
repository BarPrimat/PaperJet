using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private float speedScrolling = 3.2f;
    public bool speedChangeForPlane = false;
    public bool isBackground = false;    
    public float sliceByNSeconds = 10;
    public float CheckSpeedChangeEffect = 1f;

    private Rigidbody2D m_Rb2d;


    // Start is called before the first frame update
    void Start()
    {
        if (speedChangeForPlane)
        {
            speedScrolling = Random.Range(6f, 15);
        }
        m_Rb2d = GetComponent<Rigidbody2D>();
        m_Rb2d.velocity = new Vector2(GameManager.Instance.scrollSpeed * speedScrolling, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!speedChangeForPlane)
        {
            if (GameManager.Instance.isJet) m_Rb2d.velocity = new Vector2(GameManager.Instance.scrollSpeed * 5.5f, 0);
            if (!GameManager.Instance.isJet && isBackground) m_Rb2d.velocity = new Vector2(GameManager.Instance.scrollSpeed * speedScrolling, 0);
            if (GameManager.Instance.gameIsOver) m_Rb2d.velocity = Vector2.zero;
        }
    }

}
