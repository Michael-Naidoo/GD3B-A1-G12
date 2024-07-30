using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            Move();
        }

        void Move()
        {
            Vector3 target = transform.position + (transform.up * 10);
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("CentiPiece"))
            {
                other.gameObject.GetComponent<CentipedeBehaviour>().HasBeenHit();
                Destroy(gameObject);
            }
        }
    }
}