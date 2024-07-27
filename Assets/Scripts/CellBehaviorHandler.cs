using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CellBehaviorHandler : MonoBehaviour
    {
        [SerializeField] private int xVal;
        [SerializeField] private int yVal;

        private int left;
        private int right;
        private int up;
        private int down;
        private int cellVal;

        private GridSystem gS;

        private Image image;
        private Text text;

        private float moveDelay = 0.5f; // Delay in seconds between moves
        private bool isMoving = false;
        public bool isLastCell = false;

        private void Start()
        {
            // get ref to the grid system
            gS = GameObject.FindWithTag("GridSystem")?.GetComponent<GridSystem>();
            if (gS == null)
            {
                Debug.LogError("GridSystem not found!");
                return;
            }
            // set game frame rate
            Application.targetFrameRate = 60;
            
            // get image component
            image = GetComponentInChildren<Image>();
            
            // get text component
            text = GetComponentInChildren<Text>();

            if (image == null || text == null)
            {
                Debug.LogError("Image or Text component not found!");
            }
        }

        private void Update()
        {
            if (gS != null && !isMoving)
            {
                StartCoroutine(MoveCell());
                if (isLastCell == true)
                {
                    // if this is the last cell then update the matrix
                    gS.matrix = gS.matrixTemp;
                }
            }
        }

        private IEnumerator MoveCell()
        {
            isMoving = true;
            CalculateNextVal();
            ShowCellSprite();
            yield return new WaitForSeconds(moveDelay);
            isMoving = false;
        }

        private void ShowCellSprite()
        {
            
            // all this does is give each value a diferent color so that i can identify whats happening
            
            int cellValue = GetCellValue(xVal, yVal);
            switch (cellValue)
            {
                case 0:
                    image.color = Color.white;
                    break;
                case 1:
                    image.color = Color.green;
                    break;
                case 2:
                    image.color = Color.red;
                    break;
                case 3:
                    image.color = Color.blue;
                    break;
                case 4:
                    image.color = Color.black;
                    break;
                default:
                    Debug.LogError("Invalid cell value: " + cellValue);
                    break;
            }

            Debug.Log("Cell [" + xVal + ", " + yVal + "] has value: " + cellVal);
        }

        public void GetCellData(int x, int y)
        {
            // the GridSystem script uses this to let this script know what its cell number is
            
            xVal = x;
            yVal = y;
            GetCellValue(xVal, yVal);
        }

        int GetCellValue(int x, int y)
        {
            if (gS == null || gS.matrix == null)
            {
                Debug.LogError("GridSystem or matrix is not initialized!");
                return -1; // Error value
            }
            if (x < 0 || x >= gS.matX || y < 0 || y >= gS.matY)
            {
                Debug.LogError("Matrix index out of bounds!");
                return -1; // Error value
            }
            
            // uses this cell number to get the integer value associated with this cell
            
            cellVal = gS.matrix[x][y];
            return cellVal;
        }

        private void CheckOrthagonalCells()
        {
            // if the horizontal position is to the right of the left most collumn, get its vale, else mark it as -1 indicating it is outside the grid
            if (xVal > 0)
            {
                left = GetCellValue(xVal - 1, yVal);
            }
            else
            {
                left = -1;
            }
            // if the horizontal position is to the left of the right most collumn, get its vale, else mark it as -1 indicating it is outside the grid
            if (xVal < gS.matX - 1)
            {
                right = GetCellValue(xVal + 1, yVal);
            }
            else
            {
                right = -1;
            }
            // if the vertical position is above  the lowest row, get its vale, else mark it as -1 indicating it is outside the grid
            if (yVal > 0)
            {
                down = GetCellValue(xVal, yVal - 1);
            }
            else
            {
                down = -1;
            }
            // if the vertical position is below  the highest row, get its vale, else mark it as -1 indicating it is outside the grid
            if (yVal < gS.matY - 1)
            {
                up = GetCellValue(xVal, yVal + 1);
            }
            else
            {
                up = -1;
            }

            Debug.Log("Cell [" + xVal + ", " + yVal + "] neighbors - Left: " + left + ", Right: " + right + ", Up: " + up + ", Down: " + down);
        }

        private void CalculateNextVal()
        {
            
            // set this cells value based on the values of the cells around it.
            
            CheckOrthagonalCells();
            int newVal = 0;

            if (up == 0 && down == 0 && left == 0 && right == 0)
            {
                newVal = 0;
            }
            else if (right == 1 && left == -1)
            {
                newVal = 3;
            }
            else if (left == 2 && right == -1)
            {
                newVal = 4;
            }
            else if (up == 3)
            {
                newVal = 2;
            }
            else if (up == 4)
            {
                newVal = 1;
            }
            else if (left == 2)
            {
                newVal = 2;
            }
            else if (right == 1)
            {
                newVal = 1;
            }
            else
            {
                newVal = 0;
            }

            gS.matrixTemp[xVal][yVal] = newVal;
            Debug.Log("Cell [" + xVal + ", " + yVal + "] updated to value: " + newVal);
        }
    }
}
