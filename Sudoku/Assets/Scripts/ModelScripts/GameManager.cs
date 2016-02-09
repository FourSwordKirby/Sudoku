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
        int[] modifiers = createModifiers(4);
        foreach (int mod in modifiers)
        {
            sudokuBoard.initializeMod(mod, Random.Range(0, 9), Random.Range(0, 9));
            modPanel.addMod(mod);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    int[] createModifiers(int modifierCount)
    {
        int[] numberModifiers = new int[modifierCount];
        for(int i = 0; i < modifierCount; i++){
            //if(Random.Range(0.0f, 1.0f) > 0.5f)
                numberModifiers[i] = Random.Range(1, 3);
            //else
                //numberModifiers[i] = -Random.Range(1, 9);
        }
        return numberModifiers;
    }
}
