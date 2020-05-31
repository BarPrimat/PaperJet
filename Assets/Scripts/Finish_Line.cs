using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Line : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.GetComponent<paperPlane>() != null)
        {
            GameManager.Instance.FinishLineCrossed();
        }
    }
}
