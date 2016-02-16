using UnityEngine;
using System.Collections;

public abstract class SudokuManager : MonoBehaviour {

    /// <summary>
    /// This function is called when the board is completed
    /// </summary>
    public abstract void BoardCompleted();

    /// <summary>
    /// Call this function to apply a mod to a board
    /// </summary>
    public abstract void ApplyMod(int mod, int x, int y);

    /// <summary>
    /// Call this function to remove a mod from the board
    /// </summary>
    public abstract void RemoveMod(int mod, int x, int y);

    /// <summary>
    /// Call this function to get the value of the at that position on the board
    /// </summary>
    public abstract int GetValue(int x, int y);
}
