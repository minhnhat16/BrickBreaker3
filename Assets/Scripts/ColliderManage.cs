using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManage : MonoBehaviour
{
    private ContactHandle contact;
    [SerializeField] private Collider2D[] m_colliders;
    private int index;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Debug.Log("Bein hit");
        }
    }

}
