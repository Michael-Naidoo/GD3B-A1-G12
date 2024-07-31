using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [FormerlySerializedAs("canvasGO")] [SerializeField] private GameObject parentGameObject;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 positionRaw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(bullet, positionRaw, quaternion.identity, parentGameObject.transform);
                Debug.LogError(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}