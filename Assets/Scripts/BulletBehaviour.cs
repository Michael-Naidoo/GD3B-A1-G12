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
            transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Hit Confirmed");
            if (other.gameObject.CompareTag("CentiPiece"))
            {
                other.gameObject.GetComponent<CentipedeBehaviour>().HasBeenHit();
                Destroy(gameObject);
            }
        }
    }
}