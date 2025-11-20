using System;
using System.Collections;
using System.Collections.Generic;
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
    
    bool FillCellReCurSivePure(int row, int col)
    {
        if (col == 9) { col = 0; row++; }
        if (row == 9) return true;

        List<int> nums = new List<int>{1,2,3,4,5,6,7,8,9};
        Shuffle(nums);

        foreach (int num in nums)
        {
            if (ValidateNumber(num, row, col))
            {
                sudukoBoard[row, col] = num;

                if (FillCellReCurSivePure(row, col + 1))
                    return true; // solved

                sudukoBoard[row, col] = 0; // undo
            }
        }

        return false; // dead end → backtrack
    }

    public void StartGame()
    {
        /*Debug.Log("Starting game");
        ClearBoard();
        StartCoroutine(FillBoardCoroutine());*/
        
        ClearBoard();
        if (FillCellReCurSivePure(0, 0))
        {
            Debug.Log("Sudoku generated successfully!");
            PrintBoard();
        }
        else
        {
            Debug.Log("Failed to generate Sudoku.");
        }
    }
    
    private IEnumerator FillBoardCoroutine()
    {
        yield return StartCoroutine(FillCell(0, 0));
        PrintBoard();
    }

    
    private IEnumerator FillCell(int row, int col)
    {
        // If col passed 8 → next row
        if (col == 9)
        {
            col = 0;
            row++;
        }

        // If row passed 8 → BOARD COMPLETE
        if (row == 9)
            yield break;

        // Create a shuffled list of numbers 1–9
        List<int> numbers = new List<int>() {1,2,3,4,5,6,7,8,9};
        Shuffle(numbers);

        foreach (int number in numbers)
        {
            if (ValidateNumber(number, row, col))
            {
                sudukoBoard[row, col] = number;

                // Continue to next cell
                yield return StartCoroutine(FillCell(row, col + 1));

                // IF the board got filled completely, stop
                if (IsBoardFull())
                    yield break;

                // ❌ DEAD END → undo this cell
                sudukoBoard[row, col] = 0;
            }
        }

        // If we finish the foreach loop:
        // → No number worked here → dead end → return to caller
    }
    
    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }
    
    private bool IsBoardFull()
    {
        for (int r = 0; r < 9; r++)
        for (int c = 0; c < 9; c++)
            if (sudukoBoard[r, c] == 0)
                return false;
        return true;
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
               // Debug.Log("Row Wrong" + row);
                return false;
            }
        }
        //check the column
        for (int i = 0; i < 9; i++)
        {
            if (sudukoBoard[i,column] == randomNumber)
            {
               // Debug.Log("Column Wrong");
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
