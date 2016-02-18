using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialGameManager : SudokuManager {

    public DialogBox dialogBox;
    public TextAsset textFile;

    private string name;
    private string[] dialog;
    private int dialogCounter;

    private static bool wait = false;
    private bool waiting;

    string[] separatingStrings = { "\n" };


    //Tutorial Board Stuff
    public SudokuBoard sudokuBoard;
    public ModifierPanel modPanel;

    int[,] mySudokuBoard;

    // Use this for initialization
    void Start()
    {
        //dialog initialization
        this.name = textFile.text.Substring(0, textFile.text.IndexOf('\n'));
        this.dialog = textFile.text.Substring(textFile.text.IndexOf('\n') + 1).Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        this.dialogCounter = 0;

        dialogBox.displayDialog(name, dialog[dialogCounter]);
        dialogCounter++;

        //board initialization
        mySudokuBoard = new int[,] {{ 1, 8, 5, 2, 0, 7, 4, 6, 3 }, 
                                    { 4, 7, 3, 8, 6, 1, 5, 0, 2 },
                                    { 6, 0, 2, 5, 3, 4, 1, 7, 8 },
                                    { 5, 1, 4, 7, 8, 6, 2, 3, 0 },
                                    { 8, 2, 0, 3, 1, 5, 7, 4, 6 },
                                    { 3, 6, 7, 4, 2, 0, 8, 1, 5 },
                                    { 0, 5, 6, 1, 4, 2, 3, 8, 7 },
                                    { 7, 4, 8, 6, 5, 3, 0, 2, 1 },
                                    { 2, 3, 1, 0, 7, 8, 6, 5, 4 }};

        sudokuBoard.instantiateBoard(mySudokuBoard);
    }

    // What goes here in the update function is basically a linear flow through the tutorial
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogBox.dialogField.text == dialog[dialogCounter-1] && !wait || (waiting && !wait))
        {
            waiting = false;
            dialogBox.displayDialog(name, dialog[dialogCounter]);
            dialogCounter++;
            if (dialogCounter == 3)
            {
                sudokuBoard.initializeMod(-1, 1, 1);
                modPanel.addMod(-1);
                sudokuBoard.checkBoard();
                wait = true;
                waiting = true;
                modPanel.disableMods();
            }
            if (dialogCounter == 7)
            {
                sudokuBoard.initializeMod(1, 0, 0);
                modPanel.addMod(1);
                sudokuBoard.checkBoard();
                modPanel.disableMods();
            }
        }
        if (dialogCounter == 3 && dialogBox.dialogField.text == dialog[dialogCounter - 1])
        {
            if (Input.GetMouseButtonDown(1))
            {
                wait = false;
                waiting = false;
                dialogBox.displayDialog(name, dialog[dialogCounter]);
                dialogCounter++;
            }
        }

        if (dialogCounter == 5)
        {
            modPanel.enableMods();
            wait = true;
            waiting = true;
        }
        if (dialogCounter == 10)
        {
            modPanel.enableMods();
            wait = true;
            waiting = true;
        }

        if(dialogCounter == dialog.Length)
        {
            Application.LoadLevel("SudokuScene");
        }
    }

    override public void BoardCompleted()
    {
        wait = false;
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
}
