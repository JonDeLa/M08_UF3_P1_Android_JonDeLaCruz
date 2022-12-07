using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchObject : MonoBehaviour
{
    public int mTouchID;
    public GameObject pointer;

    //creamos el constructor
    public touchObject(int _touchID, GameObject _newPointer)
    {
        mTouchID = _touchID;
        pointer = _newPointer;
    }
}
