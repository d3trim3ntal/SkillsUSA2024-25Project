using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public SpinnyPipe[] row1;
    public SpinnyPipe[] row2;
    public SpinnyPipe[] row3;
    public SpinnyPipe[] row4;
    public GameObject pipeCam;
    public GameObject mainCam;
    public bool active;
    Dictionary<int, SpinnyPipe[]> rows = new Dictionary<int, SpinnyPipe[]>();
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            toggleCam();
        }
    }
    private void Awake()
    {
        rows.Add(1, row1);
        rows.Add(2, row2);
        rows.Add(3, row3);
        rows.Add(4, row4);
        
    }
    public SpinnyPipe upPipe(int row, int column)
    {
        if (row < 4)
        {
            SpinnyPipe output = rows[row + 1][column];
            return output;
        }
        else
        {
            return null;
        }
        
    }
    public SpinnyPipe leftPipe(int row, int column)
    {
        if (column > 1)
        {
            SpinnyPipe output = rows[row][column - 1];
            return output;
        }
        else
        {
            return null;
        }
        
    }

    public SpinnyPipe downPipe(int row, int column)
    {
        if (row > 1)
        {
            SpinnyPipe output = rows[row - 1][column];
            return output;
        }
        else
        {
            return null;
        }
        
    }
    public SpinnyPipe rightPipe(int row, int column)
    {
        if (column < 6)
        {
            SpinnyPipe output = rows[row][column + 1];
            return output;
        }
        else
        {
            return null;
        }
        
    }

    public void toggleCam()
    {
        if (active)
        {
            active = false;
            mainCam.SetActive(true);
            pipeCam.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            active = true;
            mainCam.SetActive(false);
            pipeCam.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
