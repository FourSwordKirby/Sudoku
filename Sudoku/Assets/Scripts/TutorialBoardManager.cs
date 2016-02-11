using UnityEngine;
using System.Collections;

public class TutorialBoardManager : MonoBehaviour {
    public static SudokuBoard sudokuBoard;
    public static ModifierPanel modPanel;

    public static Pause pauseComponent;


	// Use this for initialization
	void Start () {
        sudokuBoard = GameObject.FindObjectOfType<SudokuBoard>();
        modPanel = GameObject.FindObjectOfType<ModifierPanel>();
        pauseComponent = GameManager.FindObjectOfType<Pause>();

        sudokuBoard.instantiateBoard();
        sudokuBoard.isTutorial = true;
        modPanel.isTutorial = true;
        /*
        int[] modifiers = createModifiers(3);
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
         */
	}

    public static void spawnMod(int mod, int x, int y)
    {
        sudokuBoard.initializeMod(mod, x, y);
        modPanel.addMod(mod);
    }
}
