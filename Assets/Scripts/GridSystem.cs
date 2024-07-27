using System;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    private float tempTimer = 10;
    
    public int[][] matrix;
    public int matX;
    public int matY;
    public int[][] matrixTemp;
    
    [SerializeField] private GameObject gridSquare;
    [SerializeField] private GameObject canvasGO;
    
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
            tempTimer = 10;
            if (matrix != null && matrix.Length > 28 && matrix[28].Length > 15)
            {
                matrix[28][15] = 1;
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

    public void Swap()
    {
        matrixTemp = matrix;
    }

    void InstantiateMatrix(int x, int y)
    {
        // Initialize the matrix
        matrix = new int[x][];
        for (int i = 0; i < x; i++)
        {
            matrix[i] = new int[y]; // Initialize each row
        }
        // Initialize the matrix
        matrixTemp = new int[x][];
        for (int i = 0; i < x; i++)
        {
            matrixTemp[i] = new int[y]; // Initialize each row
        }
        
        // Create a grid of x by y
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                matrix[i][j] = 0;
                matrixTemp[i][j] = 0;
                Vector3 position = new Vector3(i * offsetMultX + offsetAddX, j * offsetMultY + offsetAddY, 0);
                GameObject gS = Instantiate(gridSquare, position, quaternion.identity, canvasGO.transform);
                if (gS != null)
                {
                    gS.GetComponent<CellBehaviorHandler>().GetCellData(i, j);
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