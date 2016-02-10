using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static SudokuBoard sudokuBoard;
    public static ModifierPanel modPanel;

	// Use this for initialization
	void Start () {
        sudokuBoard = GameObject.FindObjectOfType<SudokuBoard>();
        modPanel = GameObject.FindObjectOfType<ModifierPanel>();

        sudokuBoard.instantiateBoard();
        int[] modifiers = createModifiers(1);
        foreach (int mod in modifiers)
        {
            int x = Random.Range(0, 9);
            int y = Random.Range(0, 9);

            while (sudokuBoard.getValue(x, y) - mod < 0 || sudokuBoard.getValue(x, y) - mod > 8)
                x = Random.Range(0, 9);
                y = Random.Range(0, 9);

            sudokuBoard.initializeMod(mod, x, y);
            modPanel.addMod(mod);
        }
	}
	
	//Plays in the win condition
    public static void Win()
    {
        Debug.Log("Win");
    }

    int[] createModifiers(int modifierCount)
    {
        int[] numberModifiers = new int[modifierCount];
        for(int i = 0; i < modifierCount; i++){
            if(Random.Range(0.0f, 1.0f) > 0.5f)
                numberModifiers[i] = Random.Range(1, 3);
            else
                numberModifiers[i] = -Random.Range(1, 3);
        }
        return numberModifiers;
    }
}
