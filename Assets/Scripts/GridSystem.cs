using System;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class GridSystem : MonoBehaviour
{
    public int score;
    public int highScore;
    public int currentCentiCount = 0; 
    public int maxCentiPieces = 15;
    public float centiSpeed;
    
    [SerializeField] private float tempTimer = 10;
    [SerializeField] private float spawnTimer = 10;
    [SerializeField] private int centiCount = 15;
    
    public int[][] matrix;
    public int matX;
    public int matY;
    public GameObject[][] MatrixGameObjects;

    [SerializeField] private GameObject gridSquare;
    [SerializeField] private GameObject spawnGameObject;
    [FormerlySerializedAs("canvasGO")] [SerializeField] private GameObject parentGameObject;
    [SerializeField] private GameObject CentiPiece;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private float offsetMultX;
    [SerializeField] private float offsetAddX; 
    [SerializeField] private float offsetMultY;
    [SerializeField] private float offsetAddY;

    private void Start()
    {
        centiCount = maxCentiPieces;

        if (gridSquare == null || parentGameObject == null)
        {
            Debug.LogError("GridSquare or CanvasGO not set!");
            return;
        }
        
        InstantiateMatrix(matX, matY);

        LoadHighScore(); 
        UpdateScoreText(); 
        UpdateHighScoreText();
    }

    void TempSpawnCenti()
    {
        tempTimer -= Time.deltaTime;
        if (tempTimer <= 0)
        {
            tempTimer = spawnTimer;
            if (matrix != null && matrix.Length >= 30 && matrix[29].Length >= 16)
            {
                if (centiCount == 1)
                {
                    currentCentiCount = maxCentiPieces;
                }
                if (centiCount > 0)
                {
                    centiCount--;
                    Vector3 spawnPosition = spawnGameObject.transform.position; 
                    GameObject centi = Instantiate(CentiPiece, spawnPosition, Quaternion.identity, parentGameObject.transform); 
                    centi.GetComponent<CentipedeBehaviour>().speed = centiSpeed;
                    centi.GetComponent<CentipedeBehaviour>().targetX = 16;
                    centi.GetComponent<CentipedeBehaviour>().targetY = 29;
                    centi.GetComponent<CentipedeBehaviour>().currentX = 15;
                    centi.GetComponent<CentipedeBehaviour>().currentY = 29;
                    centi.GetComponent<CentipedeBehaviour>().direction = CentipedeBehaviour.DesiredDirection.Right;
                    centi.GetComponent<CentipedeBehaviour>().previousDirection = CentipedeBehaviour.DesiredDirection.NR;
                }
                else
                {
                    centiCount = maxCentiPieces;
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
        if (currentCentiCount <= 0)
        {
            TempSpawnCenti();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            
        }
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
                GameObject gS = Instantiate(gridSquare, parentGameObject.transform, true);
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

    public void UpdateScoreText()
    {
        scoreText.text = "" + score;
    }


    public void UpdateHighScoreText()
    {
        highScoreText.text = "" + highScore;
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
            UpdateHighScoreText();
        }
    }
}
