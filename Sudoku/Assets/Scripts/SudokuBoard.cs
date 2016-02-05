using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SudokuBoard: MonoBehaviour {
    private int[,] sudokuBoard;
    private Apartment[,] Apartments;

    public GameObject AptPrefab;

    public float aptWidth;
    public float aptHeight;

    void Start()
    {
        
    }

    public void instantiateBoard()
    {
        sudokuBoard = SudokuModel.generateBoard();

        Apartments = new Apartment[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject apartment = GameObject.Instantiate(AptPrefab);
                apartment.transform.SetParent(this.transform);
                apartment.transform.localPosition = new Vector2(i * aptWidth, j * -aptHeight);
                Apartments[i, j] = apartment.GetComponent<Apartment>();

                apartment.GetComponent<Apartment>().happiness = sudokuBoard[i, j];
                apartment.GetComponent<Apartment>().row = i;
                apartment.GetComponent<Apartment>().col = j;
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
        checkBoard();
    }

    public void removeMod(int mod, int x, int y)
    {
        sudokuBoard[x, y] = Mathf.Clamp(sudokuBoard[x, y] - mod, 0, 8);
        Apartments[x, y].happiness = sudokuBoard[x, y];
        checkBoard();
    }

    public void checkBoard()
    {
        List<Vector3> problemSpaces = SudokuModel.resolveBoard(sudokuBoard);
        foreach (Vector3 problem in problemSpaces)
        {
            int x = (int)problem.x;
            int y = (int)problem.y;
            int severity = (int)problem.z;
            Apartments[x, y].markUnresolved(severity);
        }
    }
}
