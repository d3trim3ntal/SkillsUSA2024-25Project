using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyPipe : MonoBehaviour
{
    private PipeManager manager;
    private SpinnyPipe spin;
    public Material filledMat;
    public Material emptyMat;
    private MeshRenderer[] mRenderer;
    [Header("Conditions")]
    public int rotation;
    public bool filled;
    public bool permaFull;

    [Header("Directions")]
    public bool up;
    public bool left;
    public bool down;
    public bool right;

    [Header("Placement")]
    public int row;
    public int column;
    public bool immovable;


    void Start()
    {
        manager = GetComponentInParent<PipeManager>();
        mRenderer = GetComponentsInChildren<MeshRenderer>();

    }

    void RotateClockwise()
    {
        transform.Rotate(0, 0, -90);
        //Figure out new active directions
        bool newUp = left;
        bool newLeft = down;
        bool newDown = right;
        bool newRight = up;

        up = newUp;
        left = newLeft;
        down = newDown;
        right = newRight;

        //Detect if adjacent pipes can fill up with water;

        fillCheck(0);


    }

    public void fillCheck(int except)
    {
        //Figure out whether this pipe should be filled or not
        if (permaFull)
        {
            filled = true;

        }
        else
        {
            filled = false;

        }
        if (up)
        {
            spin = manager.upPipe(row, column);
            if (spin != null)
            {
                if (spin.down && spin.filled)
                {
                    filled = true;
                }
            }
        }
        if (left)
        {
            spin = manager.leftPipe(row, column);
            if (spin != null)
            {
                if (spin.right && spin.filled)
                {
                    filled = true;
                }
            }
        }
        if (down)
        {
            spin = manager.downPipe(row, column);
            if (spin != null)
            {
                if (spin.up && spin.filled)
                {
                    filled = true;
                }
            }
        }
        if (right)
        {
            spin = manager.rightPipe(row, column);
            if (spin != null)
            {
                if (spin.left && spin.filled)
                {
                    filled = true;
                }
            }
        }

        //Visualize being filled or not
        if (mRenderer != null)
        {
            if (filled)
            {
                foreach(MeshRenderer i in mRenderer)
                {
                    i.material = filledMat;
                }
                fillAdjacent(except);

            }
            else
            {
                foreach (MeshRenderer i in mRenderer)
                {
                    i.material = emptyMat;
                }
            }
        }


    }

    private void fillAdjacent(int except)
    {
        //Fill up from down
        spin = manager.upPipe(row, column);
        if (spin != null && except != 3)
        {
            spin.fillCheck(1);
        }
        //Fill left from right
        spin = manager.leftPipe(row, column);
        if (spin != null && except != 4)
        {
            spin.fillCheck(2);
        }
        //Fill down from up
        spin = manager.downPipe(row, column);
        if (spin != null && except != 1)
        {
            spin.fillCheck(3);
        }
        //Fill right from left
        spin = manager.rightPipe(row, column);
        if (spin != null && except != 2)
        {
            spin.fillCheck(4);
        }



    }

    private void OnMouseDown()
    {
        if (!immovable)
        {
            RotateClockwise();
        }
    }
}
