using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperPlane : MonoBehaviour
{
    public static paperPlane Instance;
    public float upForce = 300f;
    public bool isAlive = true;
    public int afterNScoreChengeToJet = 10;
    public bool isJet = false;
    public float timeSinceLastSpawn;
    public GameObject  fire;

    private Animator m_Anim;
    private Rigidbody2D m_Rb2d;
    private bool m_IsFlickering = false;

    // Start is called before the first frame update
    void Start()
    {
        fire.SetActive(false);
        m_Anim = GetComponent<Animator>();
        m_Rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.paused) return;
        if (!GameManager.Instance.collisionWithAsset) collisionWithAsset();
        GameManager.Instance.SetJetActive(isJet);
        if (isAlive && (Input.GetMouseButton(0) == true))
        {
            m_Rb2d.velocity = Vector2.zero;
            m_Rb2d.AddForce(new Vector2(0, upForce));
            if (isJet && !m_IsFlickering)
            {
                fire.SetActive(true);
            }
        }
        else fire.SetActive(false);
        int sequenceOfScore = GameManager.Instance.sequenceOfScore;
        timeSinceLastSpawn += Time.deltaTime;
        if (sequenceOfScore >= afterNScoreChengeToJet && sequenceOfScore != 0 && timeSinceLastSpawn != 0)
        {
            GameManager.Instance.sequenceOfScore = 0;
            setOnAnimJet();
            timeSinceLastSpawn = 0;
        }
        if(timeSinceLastSpawn >= 8 && isJet == true)
        {
            m_Anim.SetTrigger("Flickering");
            m_IsFlickering = true;
        }
        if (timeSinceLastSpawn >= 10 && isJet == true) setOffAnimJet();
    }
    void OnCollisionEnter2D()
    {
        isAlive = false;
        m_Rb2d.velocity = Vector2.zero;
        m_Anim.SetTrigger("DiePlane");
        GameManager.Instance.Die();
    }
    private void setOnAnimJet()
    {
        isJet = true;
        m_IsFlickering = false;
        m_Anim.ResetTrigger("JetToPaper");
        m_Anim.SetTrigger("PaperToJet");
    }
    private void setOffAnimJet()
    {
        isJet = false;
        m_Anim.ResetTrigger("PaperToJet");
        m_Anim.SetTrigger("JetToPaper");
    }
    private void collisionWithAsset()
    {
        isAlive = false;
        m_Rb2d.velocity = Vector2.zero;
        m_Anim.SetTrigger("DiePlane");
        GameManager.Instance.Die();
    }
}
