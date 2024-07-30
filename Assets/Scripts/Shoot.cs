using System;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject canvasGO;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 positionRaw = Input.mousePosition;
                Vector3 position = new Vector3(positionRaw.x, positionRaw.y, 0);
                Instantiate(bullet, position, quaternion.identity, canvasGO.transform);
                Debug.LogError(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}