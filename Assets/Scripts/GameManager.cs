using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int[,] sudukoBoard = new int[9, 9];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HighLightCells(int row, int column)
    {
        Debug.Log("Cell Reow" + row + "," + column);
    }

    public void StartGame()
    {
        Debug.Log("Starting game");
        ClearBoard();
        StartCoroutine(GenerateSudukoBoard());
    }

    private IEnumerator GenerateSudukoBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            Debug.Log(i);
            Debug.Break();
            for (int j = 0; j < 9; j++)
            {
                while (true)
                {
                    yield return null;
                    var randomNumber = Random.Range(1, 10);
                    Debug.Log("Trying Number " + randomNumber);
                    if (ValidateNumber(randomNumber, i, j))
                    {
                        Debug.Log(randomNumber + "Ok");
                        sudukoBoard[i, j] = randomNumber;
                        break;
                    };
                }
            }
        }
        yield return null;
        PrintBoard();
    }

    private bool ValidateNumber(int randomNumber,int row,int column)
    {
        //check the row
        for (int i = 0; i < 9; i++)
        {
            if (sudukoBoard[row, i] == randomNumber)
            {
                Debug.Log("Row Wrong" + row);
                return false;
            }
        }
        //check the column
        for (int i = 0; i < 9; i++)
        {
            if (sudukoBoard[i,column] == randomNumber)
            {
                Debug.Log("Column Wrong");
                return false;
            }
        }
        // check the box
        //determine which box it belongs
        int startRow;
        int endRow;
        if (row <= 2)
        {
            startRow = 0;
            endRow = 2;
        }
        else if(row <= 5)
        {
            startRow = 3;
            endRow = 5;
        }
        else
        {
            startRow = 6;
            endRow = 8;
        }

        int startColumn;
        int endColumn;
        if (column <= 2)
        {
            startColumn = 0;
            endColumn = 2;
        }
        else if(column <= 5)
        {
            startColumn = 3;
            endColumn = 5;
        }
        else
        {
            startColumn = 6;
            endColumn = 8;
        }

        for (int i = startRow; i <= endRow; i++)
        {
            for (int j = startColumn; j <= endColumn; j++)
            {
                if (sudukoBoard[i, j] == randomNumber)
                {
                    return false;
                }
            }
        }
        //if all satisy return true otherwise return false

        return true;
    }

    private void ClearBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                sudukoBoard[i, j] = 0;
            }
        }
    }

    private void PrintBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Debug.Log("Row : " + i +" Column : " + j + " Value" + sudukoBoard[i, j]);
            }
        }
    }
}
