using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform playerTransform; // Added reference to player transform.

        public AudioSource shootSound;
        [FormerlySerializedAs("canvasGO")][SerializeField] private GameObject parentGameObject;

        private GameObject currentBullet;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentBullet == null)
                {
                    shootSound.Play();
                    Vector2 playerPosition = playerTransform.position; // Uses player position instead of mouse position.

                    currentBullet = Instantiate(bullet, playerPosition, quaternion.identity, parentGameObject.transform); // Spawns bullet from player position.
                    currentBullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * currentBullet.GetComponent<BulletBehaviour>().speed; // Sets bullet velocity directly upwards.

                    Debug.LogError(playerPosition);
                }
                else
                {
                    Debug.Log("Bullet still on screen");
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == currentBullet)
            {
                Destroy(currentBullet);
                currentBullet = null;
            }
        }
    }
}