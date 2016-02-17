﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : SudokuManager{

    public SudokuBoard sudokuBoard;
    public ModifierPanel modPanel;

	// Use this for initialization
	void Start () {
        sudokuBoard.instantiateBoard();
        populateBoard(0);
	}

    override public void BoardCompleted()
    {
        Debug.Log("Win");
        Application.LoadLevel("VictoryScene");
    }

    override public void ApplyMod(int mod, int x, int y)
    {
        sudokuBoard.applyMod(mod, x, y);
        Debug.Log("applying mod");
    }

    override public void RemoveMod(int mod, int x, int y)
    {
        sudokuBoard.removeMod(mod, x, y);
        Debug.Log("removing mod");
    }

    override public int GetValue(int x, int y)
    {
        Debug.Log("getting value");
        return sudokuBoard.getValue(x, y);
    }

    void populateBoard(int difficulty)
    {
        int[] modifiers = createModifiers(Random.Range(3, 6));
        foreach (int mod in modifiers)
        {
            int x = 0;
            int y = 0;
            int init = 0;
            do
            {
                sudokuBoard.initializeMod(-init, x, y);
                x = Random.Range(0, 9);
                y = Random.Range(0, 9);

                while (sudokuBoard.getValue(x, y) - mod < 0 || sudokuBoard.getValue(x, y) - mod > 8)
                {
                    x = Random.Range(0, 9);
                    y = Random.Range(0, 9);
                }
                sudokuBoard.initializeMod(mod, x, y);
                init = mod;
            } while (sudokuBoard.isSolved());

            modPanel.addMod(mod);
        }
    }

    int[] createModifiers(int modifierCount)
    {
        int[] numberModifiers = new int[modifierCount];
        for(int i = 0; i < modifierCount; i++){
            if(Random.Range(0.0f, 1.0f) > 0.5f)
                numberModifiers[i] = Random.Range(1, 4);
            else
                numberModifiers[i] = -Random.Range(1, 4);
        }
        return numberModifiers;
    }
}
