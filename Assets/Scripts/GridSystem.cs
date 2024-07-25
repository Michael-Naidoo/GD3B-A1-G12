using System;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    //create a new 2d array
    public int[][] matrix;

    [SerializeField] private int matX;
    [SerializeField] private int matY;
    
    [SerializeField] private GameObject gridSquare;
    [SerializeField] private GameObject canvasGO;
    
    [SerializeField] private float offsetMultX;
    [SerializeField] private float offsetAddX; 
    [SerializeField] private float offsetMultY;
    [SerializeField] private float offsetAddY;
    private void Start()
    {
        InstantiateMatrix(matX, matY);
    }

    void InstantiateMatrix(int x, int y)
    {
        // Initialize the matrix
        matrix = new int[x][];
        for (int i = 0; i < x; i++)
        {
            matrix[i] = new int[y]; // Initialize each row
        }
        
        //create a grid of x by y
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                //loop through each cell and instantiate it at 0 with 0 meaning empty
                matrix[i][j] = 0;
                //spawn a game object that will hold the sprites for each cell
                //offsetMultX is the x size of the bocks
                //offsetMultY is the y size of the bocks
                //offsetAddX is half the x size of the bocks
                //offsetAddY is half the y size of the bocks
                Vector3 position = new Vector3(i * offsetMultX + offsetAddX, j * offsetMultY + offsetAddY, 0);

                GameObject gS = Instantiate(gridSquare, position, quaternion.identity, canvasGO.transform);
                // set the cell reference on the game object
                gS.GetComponent<CellBehaviorHandler>().GetCellData(i, j);
            }
        }
    }
}
