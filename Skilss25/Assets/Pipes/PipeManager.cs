using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public PipeSpin[] row1;
    public PipeSpin[] row2;
    public PipeSpin[] row3;
    public PipeSpin[] row4;
    Dictionary<int, PipeSpin[]> rows = new Dictionary<int, PipeSpin[]>();
    
    private void Awake()
    {
        rows.Add(1, row1);
        rows.Add(2, row2);
        rows.Add(3, row3);
        rows.Add(4, row4);
        

    }
    public PipeSpin upPipe(int row, int column)
    {
        if (row < 4)
        {
            PipeSpin output = rows[row + 1][column];
            return output;
        }
        else
        {
            return null;
        }
        
    }
    public PipeSpin leftPipe(int row, int column)
    {
        if (column > 1)
        {
            PipeSpin output = rows[row][column - 1];
            return output;
        }
        else
        {
            return null;
        }
        
    }

    public PipeSpin downPipe(int row, int column)
    {
        if (row > 1)
        {
            PipeSpin output = rows[row - 1][column];
            return output;
        }
        else
        {
            return null;
        }
        
    }
    public PipeSpin rightPipe(int row, int column)
    {
        if (column < 6)
        {
            PipeSpin output = rows[row][column + 1];
            return output;
        }
        else
        {
            return null;
        }
        
    }

}
