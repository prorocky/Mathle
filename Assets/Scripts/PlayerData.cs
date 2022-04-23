using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int[] intBoard = new int[36];
    public int[] sequence = new int[6];
    public int currentRow = 0;
    public bool finished = false;
    public bool started = false;

    public PlayerData(int[] intBoard, int[] sequence, int currentRow, bool finished, bool started){
        this.intBoard = intBoard;
        this.sequence = sequence;
        this.currentRow = currentRow;
        this.finished = finished;
        this.started = started;
    }

    // public override string ToString(){
    //     return $"TESTING IntBoard {intBoard} || Sequence {sequence} || CurrentRow {currentRow} || Won {won}";
    // }
}
