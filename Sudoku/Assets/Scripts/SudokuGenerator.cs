using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//CREDITS TO SHELDON L. COOPER
//http://stackoverflow.com/questions/3588962/brute-force-algorithm-for-creation-of-sudoku-board

public class SudokuGenerator : MonoBehaviour {

    //Tutorial Boards
    //mySudokuBoard = new List<List<int>>(9);
    //mySudokuBoard.Add(new List<int>() { 2, 9, 6, 3, 1, 8, 5, 7, 4 });
    //mySudokuBoard.Add(new List<int>() { 5, 8, 4, 9, 7, 2, 6, 1, 3 });
    //mySudokuBoard.Add(new List<int>() { 7, 1, 3, 6, 4, 5, 2, 8, 9 });
    //mySudokuBoard.Add(new List<int>() { 6, 2, 5, 8, 9, 7, 3, 4, 1 });
    //mySudokuBoard.Add(new List<int>() { 9, 3, 1, 4, 2, 6, 8, 5, 7 });
    //mySudokuBoard.Add(new List<int>() { 4, 7, 8, 5, 3, 1, 9, 2, 6 });
    //mySudokuBoard.Add(new List<int>() { 1, 6, 7, 2, 5, 3, 4, 9, 8 });
    //mySudokuBoard.Add(new List<int>() { 8, 5, 9, 7, 6, 4, 1, 3, 2 });
    //mySudokuBoard.Add(new List<int>() { 3, 4, 2, 1, 8, 9, 7, 6, 5 });

    void Start()
    {
        generateBoard();
    }

    public static int[,] generateBoard()
    {
        int r = Random.Range(0, 9);//and.nextInt(9);
        int c = Random.Range(0, 9);//rand.nextInt(9);
        int value = Random.Range(0, 9) + 1;//rand.nextInt(9) + 1;
        Board board = new Board();
        board.set(r, c, value);
        solve(board, 0);

        return(board.getBoard());
    }

    private static bool solve(Board board, int at) {
        if (at == 9*9)
            return true;
        int r = at / 9;
        int c = at % 9;
        if (board.isSet(r, c))
            return solve(board, at + 1);

        List<int> numberPool = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};
        //for (int value = 1; value <= 9; value++) {
        int value = 0;
        while (numberPool.Count > 0){
            value = numberPool[Random.Range(0, numberPool.Count)];
            numberPool.Remove(value);
            if (board.canSet(r, c, value)) {
                board.set(r, c, value);
                if (solve(board, at + 1))
                    return true;
                board.unSet(r, c);
            }
        }
        return false;
    }

    public bool isValidBoard(List<List<int>> SudokuBoard)
    {
        Dictionary<int, List<int>> itemXPos = new Dictionary<int, List<int>>();
        Dictionary<int, List<int>> itemYPos = new Dictionary<int, List<int>>();

        for (int i = 0; i < SudokuBoard.Count; i++)
        {
            itemXPos.Add(i, new List<int>());
            itemYPos.Add(i, new List<int>());
        }

        for (int i = 0; i < SudokuBoard.Count; i++)
        {
            for (int j = 0; j < SudokuBoard[i].Count; j++)
            {
                itemXPos[SudokuBoard[i][j]].Add(j);
                itemYPos[SudokuBoard[i][j]].Add(i);
            }
        }

        //Check rows
        foreach (int entry in itemXPos.Keys)
        {
            if (itemXPos[entry].Count < 9)
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
            for (int j = 0; j < 3; j++)
            {
                Dictionary<int, bool> squareDictionary = new Dictionary<int, bool>();
                squareDictionary.Add(SudokuBoard[3 * i][3 * j], true);
                squareDictionary.Add(SudokuBoard[3 * i][3 * j + 1], true);
                squareDictionary.Add(SudokuBoard[3 * i][3 * j + 2], true);
                squareDictionary.Add(SudokuBoard[3 * i + 1][3 * j], true);
                squareDictionary.Add(SudokuBoard[3 * i + 1][3 * j + 1], true);
                squareDictionary.Add(SudokuBoard[3 * i + 1][3 * j + 2], true);
                squareDictionary.Add(SudokuBoard[3 * i + 2][3 * j], true);
                squareDictionary.Add(SudokuBoard[3 * i + 2][3 * j + 1], true);
                squareDictionary.Add(SudokuBoard[3 * i + 2][3 * j + 2], true);

                if (squareDictionary.Keys.Count != 9)
                    return false;
            }
        }

        return true;
    }

    class Board {
        private int[,] board = new int[9, 9];
        private bool[,] rs = new bool[9, 10];
        private bool[,] cs = new bool[9, 10];
        private bool[,,] bs = new bool[3, 3, 10];
        public Board() {}
        public bool canSet(int r, int c, int value) {
            return !isSet(r, c) && !rs[r, value] && !cs[c, value] && !bs[r/3, c/3, value];
        }
        public bool isSet(int r, int c) {
            return board[r, c] != 0;
        }
        public void set(int r, int c, int value) {
            board[r, c] = value;
            rs[r, value] = cs[c, value] = bs[r/3, c/3, value] = true;
        }
        public void unSet(int r, int c) {
            if (isSet(r, c)) {
                int value = board[r, c];
                board[r, c] = 0;
                rs[r, value] = cs[c, value] = bs[r/3, c/3, value] = false;
            }
        }
        public string toString() {
            string ret = "";
            for (int r = 0; r < 9; r++) {
                for (int c = 0; c < 9; c++)
                    ret += " " + board[r, c];
                ret += "\n";
            }
            return ret;
        }

        public int[,] getBoard()
        {
            return board;
        }
    }
}
