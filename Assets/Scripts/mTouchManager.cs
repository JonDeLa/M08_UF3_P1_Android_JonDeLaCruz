using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mTouchManager : MonoBehaviour
{
    public GameObject Pointer;
    public List<touchObject>touches = new List<touchObject>();

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                Debug.Log("touch began");
                touches.Add(new touchObject(t.fingerId, createCircle(t)));
            }
            else if (t.phase == TouchPhase.Ended)
            {
                Debug.Log("touch ended");
                touchObject thisTouch = touches.Find(touchLocation => touchLocation.mTouchID == t.fingerId);
                Destroy(thisTouch.pointer);
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            else if (t.phase == TouchPhase.Moved)
            {
                Debug.Log("touch is moving");
                touchObject thisTouch = touches.Find(touchLocation => touchLocation.mTouchID == t.fingerId);
                thisTouch.pointer.transform.position = getTouchPosition(t.position);
            }
            ++i;
        }
    }
    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }
    GameObject createCircle(Touch t)
    {
        GameObject c = Instantiate(Pointer) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = getTouchPosition(t.position);
        return c;
    }
}

