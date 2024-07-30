using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CentipedeBehaviour : MonoBehaviour
    {
        public float speed;

        private GridSystem gS;

        public int targetX;
        public int targetY;
        public int currentX;
        public int currentY;

        public enum DesiredDirection
        {
            Left,
            Right,
            Up,
            Down,
            NR
        }

        public DesiredDirection direction;
        public DesiredDirection previousDirection;

        private void Start()
        {
            gS = GameObject.FindWithTag("GridSystem").gameObject.GetComponent<GridSystem>();
        }

        private void Update()
        {
            MoveCenti();
        }

        private void MoveCenti()
        {
            Transform targetPos = gS.MatrixGameObjects[targetX][targetY].transform;
            var step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, step);

            if (transform.position == targetPos.position)
            {
                Debug.LogError(gS.matrix[currentX][currentY]);
                CalculateNextCell();
            }
        }

        void CalculateNextCell()
        {
            bool isWithinBounds(int x, int y)
            {
                return x >= 0 && x < gS.matrix.Length && y >= 0 && y < gS.matrix[0].Length;
            }

            if (direction == DesiredDirection.Down)
            {
                if (previousDirection == DesiredDirection.Right && isWithinBounds(currentX - 1, currentY) || gS.matrix[currentX - 1][currentY] == 1)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Left;
                    currentX = targetX;
                    targetX--;
                }
                else if (previousDirection == DesiredDirection.Left && isWithinBounds(currentX + 1, currentY) || gS.matrix[currentX + 1][currentY] == 1)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Right;
                    currentX = targetX;
                    targetX++;
                }
                else if (isWithinBounds(currentX, currentY - 1))
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Down;
                    currentY = targetY;
                    targetY--;
                }
            }
            else if (direction == DesiredDirection.Left)
            {
                if (currentX - 1 == 0)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Down;
                    currentY = targetY;
                    targetY--;
                }
                else if (isWithinBounds(currentX - 1, currentY))
                {
                    previousDirection = direction;
                    currentX = targetX;
                    targetX--;
                }
            }
            else if (direction == DesiredDirection.Right)
            {
                if (currentX + 1 == gS.matrix.Length - 1)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Down;
                    currentY = targetY;
                    targetY--;
                }
                else if (isWithinBounds(currentX + 1, currentY))
                {
                    previousDirection = direction;
                    currentX = targetX;
                    targetX++;
                }
            }
        }
        
        public void HasBeenHit()
        {
            gS.matrix[targetX][targetY] = 1;
            Destroy(gameObject);
        }
    }
}
