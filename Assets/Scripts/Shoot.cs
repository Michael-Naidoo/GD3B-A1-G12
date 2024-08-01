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
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 direction = (mousePosition - playerPosition).normalized;

                    currentBullet = Instantiate(bullet, playerPosition, quaternion.identity, parentGameObject.transform); // Spawns bullet from player position.
                    currentBullet.GetComponent<Rigidbody2D>().velocity = direction * currentBullet.GetComponent<BulletBehaviour>().speed;

                    Debug.LogError(mousePosition);
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