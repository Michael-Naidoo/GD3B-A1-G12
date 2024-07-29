using System;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem : MonoBehaviour
{
    public float centiSpeed;
    
    [SerializeField] private float tempTimer = 10;
    [SerializeField] private int centiCount = 10;
    
    public int[][] matrix;
    public int matX;
    public int matY;
    public GameObject[][] MatrixGameObjects;

    [SerializeField] private GameObject gridSquare;
    [SerializeField] private GameObject canvasGO;
    [SerializeField] private GameObject CentiPiece;
    
    [SerializeField] private float offsetMultX;
    [SerializeField] private float offsetAddX; 
    [SerializeField] private float offsetMultY;
    [SerializeField] private float offsetAddY;

    private void Start()
    {
        if (gridSquare == null || canvasGO == null)
        {
            Debug.LogError("GridSquare or CanvasGO not set!");
            return;
        }
        
        InstantiateMatrix(matX, matY);
    }

    void TempSpawnCenti()
    {
        tempTimer -= Time.deltaTime;
        if (tempTimer <= 0)
        {
            tempTimer = 0.33f;
            if (matrix != null && matrix.Length >= 30 && matrix[29].Length >= 16)
            {
                if (centiCount > 0)
                {
                    centiCount--;
                    Vector3 pos = MatrixGameObjects[15][29].transform.position;
                    GameObject centi = Instantiate(CentiPiece, canvasGO.transform, true);
                    centi.transform.position = pos;
                    centi.GetComponent<CentipedeBehaviour>().speed = centiSpeed;
                    centi.GetComponent<CentipedeBehaviour>().targetX = 16;
                    centi.GetComponent<CentipedeBehaviour>().targetY = 29;
                    centi.GetComponent<CentipedeBehaviour>().currentX = 15;
                    centi.GetComponent<CentipedeBehaviour>().currentY = 29;
                    centi.GetComponent<CentipedeBehaviour>().direction = CentipedeBehaviour.DesiredDirection.Right;
                    centi.GetComponent<CentipedeBehaviour>().previousDirection = CentipedeBehaviour.DesiredDirection.NR;
                }
            }
            else
            {
                Debug.LogError("Matrix is not properly initialized or out of bounds access attempted!");
            }
        }
    }

    private void Update()
    {
        TempSpawnCenti();
    }
    

    void InstantiateMatrix(int x, int y)
    {
        // Initialize the matrix
        matrix = new int[x][];
        for (int i = 0; i < x; i++)
        {
            matrix[i] = new int[y]; // Initialize each row
        }

        // Initialize the MatrixGameObjects array
        MatrixGameObjects = new GameObject[x][];
        for (int i = 0; i < x; i++)
        {
            MatrixGameObjects[i] = new GameObject[y]; // Initialize each row
        }
        
        // Create a grid of x by y
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                matrix[i][j] = 0;
                Vector3 position = new Vector3(i * offsetMultX + offsetAddX, j * offsetMultY + offsetAddY, 0);
                GameObject gS = Instantiate(gridSquare, canvasGO.transform, true);
                gS.transform.position = position;
                if (gS != null)
                {
                    gS.GetComponent<CellBehaviorHandler>().GetCellData(i, j);
                    MatrixGameObjects[i][j] = gS;
                    if (i == x - 1 && j == y - 1)
                    {
                        gS.GetComponent<CellBehaviorHandler>().isLastCell = true;
                    }
                }
                else
                {
                    Debug.LogError("Failed to instantiate gridSquare!");
                }
            }
        }
    }
}
