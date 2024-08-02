using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerCollider : MonoBehaviour
    {
        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("CentiPiece"))
            {
              
                Debug.Log("Player triggered with Mushroom");
                
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }
}
