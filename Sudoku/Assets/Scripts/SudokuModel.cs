using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//CREDITS TO SHELDON L. COOPER
//http://stackoverflow.com/questions/3588962/brute-force-algorithm-for-creation-of-sudoku-board

public class SudokuModel : MonoBehaviour {

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

    public static int[,] generateBoard()
    {
        int r = Random.Range(0, 9);//and.nextInt(9);
        int c = Random.Range(0, 9);//rand.nextInt(9);
        int value = Random.Range(0, 9) + 1;//rand.nextInt(9) + 1;
        Board board = new Board();
        board.set(r, c, value);
        solve(board, 0);

        int [,] returnBoard = board.getBoard();
        for (r = 0; r < 9; r++)
        {
            for (c = 0; c < 9; c++)
            {
                returnBoard[r, c] --;
            }
        }
        return returnBoard;
    }

    //Returns a list of positions that need to be fixed & the number of things wrong in that position 
    public static List<Vector3> resolveBoard(int[,] board)
    {
        List<Vector3> problemSpaces = new List<Vector3>();

        Dictionary<int, List<int>> rowCounters = new Dictionary<int, List<int>>();
        Dictionary<int, List<int>> colCounters = new Dictionary<int, List<int>>();
        Dictionary<int, List<int>> squareCounters = new Dictionary<int, List<int>>();

        for (int i = 0; i < 9; i++)
        {
            rowCounters[i] = new List<int>();
            colCounters[i] = new List<int>();
            squareCounters[i] = new List<int>();
        }

        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                int entry = board[r, c];
                rowCounters[r].Add(entry);
                colCounters[c].Add(entry);
                squareCounters[r/3 + (c/3) * 3].Add(entry);
            }
        }

        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                int entry = board[r, c];
                int problemCount = 0;

                if(rowCounters[r].FindAll(delegate(int i) { return i == entry; }).Count > 1)
                    problemCount++;
                if (colCounters[c].FindAll(delegate(int i) { return i == entry; }).Count > 1)
                    problemCount++;
                if (squareCounters[r / 3 + (c / 3) * 3].FindAll(delegate(int i) { return i == entry; }).Count > 1)
                    problemCount++;

                if(problemCount != 0)
                    problemSpaces.Add(new Vector3(r, c, problemCount));
            }
        }

        return problemSpaces;
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
