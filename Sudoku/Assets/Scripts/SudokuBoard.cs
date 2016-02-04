using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SudokuBoard: MonoBehaviour {
    public int[,] sudokuBoard;
    public List<int> modifiers;
    public GameObject AptPrefab;

    public float aptWidth;
    public float aptHeight;

    void Start()
    {
        sudokuBoard = SudokuGenerator.generateBoard();

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject apartment = GameObject.Instantiate(AptPrefab);
                apartment.transform.SetParent(this.transform);
                apartment.transform.position = new Vector2(i * aptHeight, j * aptWidth);

                apartment.GetComponent<Apartment>().happiness = sudokuBoard[i, j];
            }
        }
    }
}
