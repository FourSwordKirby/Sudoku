using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SudokuScripts : MonoBehaviour {

    private List<List<int>> SudokuBoard;

    public bool isBoardValid()
    {
        Dictionary<int, List<int>> itemXPos = new Dictionary<int,List<int>>();
        Dictionary<int, List<int>> itemYPos = new Dictionary<int,List<int>>();

        for (int i = 0; i < SudokuBoard.Count; i++)
        {
            itemXPos.Add(i, new List<int>());
            itemYPos.Add(i, new List<int>());
        }

        //Check rows
        for (int i = 0; i < SudokuBoard.Count; i++)
        {
            for (int j = 0; j < SudokuBoard[i].Count; j++)
            {
                itemXPos[SudokuBoard[i][j]].Add(j);
                itemYPos[SudokuBoard[i][j]].Add(j);
            }
        }

        //Check rows
        foreach(int entry in itemXPos.Keys){
            if(itemXPos[entry].Count < 9)
                return false;
        }

        //Check columns
        foreach (int entry in itemXPos.Keys)
        {
            if (itemYPos[entry].Count < 9)
                return false;
        }

        //Check squares
        for (int i = 0; i < 3; i++)
        {
            Dictionary<int, bool> squareDictionary = new Dictionary<int, bool>();
            for (int j = 0; j < 3; j++)
            {
                squareDictionary.Add(SudokuBoard[3*i][3*j], true);
                squareDictionary.Add(SudokuBoard[3*i][3*j+1], true);
                squareDictionary.Add(SudokuBoard[3*i][3*j+2], true);
                squareDictionary.Add(SudokuBoard[3*i+1][3*j], true);
                squareDictionary.Add(SudokuBoard[3*i+1][3*j+1], true);
                squareDictionary.Add(SudokuBoard[3*i+1][3*j+2], true);
                squareDictionary.Add(SudokuBoard[3*i+2][3*j], true);
                squareDictionary.Add(SudokuBoard[3*i+2][3*j+1], true);
                squareDictionary.Add(SudokuBoard[3*i+2][3*j+2], true);

                if (squareDictionary.Keys.Count != 9)
                    return false;
            }
        }
        return true;
    }

    // Use this for initialization
    void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
