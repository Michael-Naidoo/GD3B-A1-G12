using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] public float speed; // Made public.

       

        // Added variable to check if player can shoot.
        public bool canShoot = true;

        private void Awake()
        {
            canShoot = true;
            
        }



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
                canShoot = true;
                
            }
            else if (other.gameObject.CompareTag("Mushroom") && other.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                other.gameObject.transform.GetChild(0).GetComponent<MushroomHandler>().HasBeenHit();
                Destroy(gameObject);
                canShoot = true;
                
                

            }

            // Added bullet being destroyed after colliding with zone outside screen.
            if (other.gameObject.CompareTag("BulletDestroyZone"))
            {
                Destroy(gameObject);
                canShoot = true;
            }
        }


       
    }
}