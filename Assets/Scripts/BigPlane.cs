using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPlane : MonoBehaviour
{
    private Rigidbody2D m_Rb2d;

    void Start()
    {
        m_Rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D()
    {
        if (!GameManager.Instance.isJet)
        {
            GameManager.Instance.SetCollisionWithAsset();
        }
    }
}
