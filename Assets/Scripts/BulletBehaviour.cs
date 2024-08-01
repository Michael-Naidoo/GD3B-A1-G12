using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float speed;

        public AudioSource centipedeShot;

        private void Update()
        {
            Move();
        }

        void Move()
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Hit Confirmed");
            if (other.gameObject.CompareTag("CentiPiece"))
            {
                
                other.gameObject.GetComponent<CentipedeBehaviour>().HasBeenHit();
                Destroy(gameObject);
                
            }
            else if (other.gameObject.CompareTag("Mushroom") && other.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                other.gameObject.transform.GetChild(0).GetComponent<MushroomHandler>().HasBeenHit();
                Destroy(gameObject);
            }
        }
    }
}