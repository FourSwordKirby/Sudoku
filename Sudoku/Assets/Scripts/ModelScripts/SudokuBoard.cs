using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SudokuBoard: MonoBehaviour {
    private int[,] sudokuBoard;
    private Residence[,] Apartments;
    private List<int>[,] modifiers;

    public GameObject AptPrefab;

    public float aptWidth;
    public float aptHeight;

    public float horizontalDivSize;
    public float verticalDivSize;

    public bool isTutorial;

    void Start()
    {
        
    }

    public void instantiateBoard()
    {
        sudokuBoard = SudokuModel.generateBoard();

        Apartments = new Residence[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject apartment = GameObject.Instantiate(AptPrefab);
                apartment.transform.SetParent(this.transform);
                apartment.transform.localPosition = new Vector2(i * aptWidth + (i / 3) * horizontalDivSize, j * -aptHeight - (j / 3) * verticalDivSize);
                Apartments[i, j] = apartment.GetComponent<Residence>();

                apartment.GetComponent<Residence>().happiness = sudokuBoard[i, j];
                apartment.GetComponent<Residence>().row = i;
                apartment.GetComponent<Residence>().col = j;
            }
        }

        modifiers = new List<int>[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                modifiers[i, j] = new List<int>();
            }
        }
    }

    public void initializeMod(int mod, int x, int y)
    {
        sudokuBoard[x, y] = Mathf.Clamp( sudokuBoard[x, y] - mod, 0, 8);
        Apartments[x, y].happiness = sudokuBoard[x, y];
        checkBoard();
    }

    public void applyMod(int mod, int x, int y)
    {
        sudokuBoard[x, y] = Mathf.Clamp(sudokuBoard[x, y] + mod, 0, 8);
        Apartments[x, y].happiness = sudokuBoard[x, y];
        modifiers[x, y].Add(mod) ;
        checkBoard();
    }

    public void removeMod(int mod, int x, int y)
    {
        sudokuBoard[x, y] = Mathf.Clamp(sudokuBoard[x, y] - mod, 0, 8);
        Apartments[x, y].happiness = sudokuBoard[x, y];
        modifiers[x, y].Remove(mod);
        checkBoard();
    }

    public bool isSolved()
    {
        List<Vector3> problemSpaces = SudokuModel.resolveBoard(sudokuBoard);

        return (problemSpaces.Count == 0);
    }

    public void checkBoard()
    {
        List<Vector3> problemSpaces = SudokuModel.resolveBoard(sudokuBoard);
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Apartments[i, j].markResolved();
            }
        }
        foreach (Vector3 problem in problemSpaces)
        {
            int x = (int)problem.x;
            int y = (int)problem.y;
            int severity = (int)problem.z;
            Apartments[x, y].markUnresolved(severity);
        }

        if (problemSpaces.Count == 0)
        {
            if (!isTutorial)
                GameManager.Win();
            else
                TutorialGameManager.Progress();
        }
    }

    public int getValue(int x, int y)
    {
        return sudokuBoard[x, y];
    }
}
