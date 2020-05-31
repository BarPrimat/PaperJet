using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon : MonoBehaviour
{
    public int changeColorAfterNTrigger = 2;
    private SpriteRenderer m_SpriteRenderer;

    private Animator anim;
    private Rigidbody2D rb2d;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = GameManager.Instance.currentColorBallon;
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.GetComponent<paperPlane>() != null)
        {
            GameManager.Instance.Score();
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            anim.SetTrigger("Explodes");
            int score = GameManager.Instance.getScore();
            if (score % changeColorAfterNTrigger == 0)
            {
                GameManager.Instance.nextColor();
            }
        }
    }
}