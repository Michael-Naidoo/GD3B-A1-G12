using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class CentipedeBehaviour : MonoBehaviour
    {
        public float speed;

        public bool up = false;

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

            if ((targetX == 0 || targetX == gS.matrix.Length) && targetY == 15 && targetX == gS.matrix.Length - 1 && up)
            {
                Debug.Log("Cenntipiece has reached y = 15");
                previousDirection = direction;
                direction = DesiredDirection.Down;
                currentY = targetY;
                targetY--;
                up = false;
            }
            if ((direction == DesiredDirection.Left || direction == DesiredDirection.Right) && (targetX == 0 || targetX == gS.matrix.Length -1) && targetY == 0)
            {
                previousDirection = direction;
                direction = DesiredDirection.Up;
                currentY = targetY;
                targetY++;
                up = true;
            }
            if (direction == DesiredDirection.Down)
            {
                Debug.Log("Moving Down");
                if (previousDirection == DesiredDirection.Left && (isWithinBounds(currentX + 1, currentY) || gS.matrix[currentX + 1][currentY] == 1))
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Right;
                    currentX = targetX;
                    currentY = targetY;
                    targetX++;
                }
                else if (previousDirection == DesiredDirection.Right && (isWithinBounds(currentX - 1, currentY) || gS.matrix[currentX - 1][currentY] == 1))
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Left;
                    currentX = targetX;
                    currentY = targetY;
                    targetX--;
                }
                else if (isWithinBounds(currentX, currentY - 1))
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Down;
                    currentY = targetY;
                    targetY--;
                }
                else if (isWithinBounds(currentX - 1, currentY))
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Left;
                    currentY = targetY;
                    currentX = targetX;
                    targetX--;
                }
                else if (isWithinBounds(currentX + 1, currentY))
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Right;
                    currentY = targetY;
                    currentX = targetX;
                    targetX++;
                }
            }
            if (direction == DesiredDirection.Up)
            {
                if (previousDirection == DesiredDirection.Left && (isWithinBounds(currentX + 1, currentY) || gS.matrix[currentX + 1][currentY] == 1))
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Right;
                    currentX = targetX;
                    currentY = targetY;
                    targetX++;
                }
                else if (previousDirection == DesiredDirection.Right && (isWithinBounds(currentX - 1, currentY) || gS.matrix[currentX - 1][currentY] == 1))
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Left;
                    currentX = targetX;
                    currentY = targetY;
                    targetX--;
                }
                else if (isWithinBounds(currentX, currentY - 1))
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Down;
                    currentY = targetY;
                    targetY--;
                }
            }
            else if (direction == DesiredDirection.Left && up)
            {
                if (isWithinBounds(currentX - 2, currentY) && gS.matrix[currentX - 2][currentY] == 1)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Up;
                    currentY = targetY;
                    targetY++;
                }
                else if (currentX - 1 == 0)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Up;
                    currentY = targetY;
                    targetY++;
                }
                else if (isWithinBounds(currentX - 1, currentY))
                {
                    previousDirection = direction;
                    currentX = targetX;
                    targetX--;
                }
            }
            else if (direction == DesiredDirection.Left)
            {
                if (isWithinBounds(currentX - 2, currentY) && gS.matrix[currentX - 2][currentY] == 1)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Down;
                    currentY = targetY;
                    targetY--;
                }
                else if (currentX - 1 == 0)
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
            else if (isWithinBounds(currentX + 2, currentY) && direction == DesiredDirection.Right && up)
            {
                if (gS.matrix[currentX + 2][currentY] == 1)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Up;
                    currentY = targetY;
                    targetY++;
                }
                else if (currentX + 1 == gS.matrix.Length - 1)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Up;
                    currentY = targetY;
                    targetY++;
                }
                else if (isWithinBounds(currentX + 1, currentY))
                {
                    previousDirection = direction;
                    currentX = targetX;
                    targetX++;
                }
            }
            else if (direction == DesiredDirection.Right)
            {
                if (isWithinBounds(currentX + 2, currentY) && gS.matrix[currentX + 2][currentY] == 1)
                {
                    previousDirection = direction;
                    direction = DesiredDirection.Down;
                    currentY = targetY;
                    targetY--;
                }
                else if (currentX + 1 == gS.matrix.Length - 1)
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
            gS.currentCentiCount--;
            gS.matrix[targetX][targetY] = 1;
            Debug.Log(gS.matrix[targetX][targetY]);
            Destroy(gameObject);
        }
    }
}
