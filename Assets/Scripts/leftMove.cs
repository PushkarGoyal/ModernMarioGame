using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class leftMove : MonoBehaviour
{


    bool ismoveL;
    bool ismoveR;

    public float setMovement()
    {
        if (ismoveL)
            return -1;
        else
             if (ismoveR)
            return 1;
        return 0;
    }


    public void setMoveL()
    {
        ismoveL = true;
    }

    public void unSetMoveL()
    {
        ismoveL = false;
    }

    public void setMoveR()
    {
        ismoveR = true;
    }

    public void unSetMoveR()
    {
        ismoveR = false;
    }
}
