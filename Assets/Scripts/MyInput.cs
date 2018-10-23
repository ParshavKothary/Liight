using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyKeyCode
{
    Up,
    Down,
    Right,
    Left
}

public class MyInput : MonoBehaviour {

    private enum MyKeyState
    {
        None,
        Down,
        Held,
        Up
    }
    
    private static Dictionary<MyKeyCode, MyKeyState> mKeyStateMap;
    private static Dictionary<MyKeyCode, bool> shouldUpdate;

	void Awake () {
        mKeyStateMap = new Dictionary<MyKeyCode, MyKeyState>();
        shouldUpdate = new Dictionary<MyKeyCode, bool>();

#if UNITY_EDITOR
        MyInput[] temp = GameObject.FindObjectsOfType<MyInput>();
        Debug.Assert(temp.Length == 1);
#endif
    }
	
    public static void SetKeyDown(MyKeyCode c)
    {
        if (!mKeyStateMap.ContainsKey(c))
        {
            mKeyStateMap.Add(c, MyKeyState.Down);
            shouldUpdate.Add(c, false);
        }
        mKeyStateMap[c] = MyKeyState.Down;
        shouldUpdate[c] = false;
    }

    public static void SetKeyUp(MyKeyCode c)
    {
        if (!mKeyStateMap.ContainsKey(c))
        {
            mKeyStateMap.Add(c, MyKeyState.Up);
            shouldUpdate.Add(c, false);
        }
        mKeyStateMap[c] = MyKeyState.Up;
        shouldUpdate[c] = false;
    }

    public static bool GetKeyDown(MyKeyCode c)
    {
        if (!mKeyStateMap.ContainsKey(c)) return false;
        return (mKeyStateMap[c] == MyKeyState.Down);
    }

    public static bool GetKeyUp(MyKeyCode c)
    {
        if (!mKeyStateMap.ContainsKey(c)) return false;
        return (mKeyStateMap[c] == MyKeyState.Up);
    }

    public static bool GetKey(MyKeyCode c)
    {
        if (!mKeyStateMap.ContainsKey(c)) return false;
        return (mKeyStateMap[c] == MyKeyState.Down || mKeyStateMap[c] == MyKeyState.Held);
    }

    // Update is called once per frame
    void Update () {
        List<MyKeyCode> keys = new List<MyKeyCode>(mKeyStateMap.Keys);
		foreach (MyKeyCode c in keys)
        {
            switch(mKeyStateMap[c])
            {
                case MyKeyState.Down:
                    if (shouldUpdate[c]) mKeyStateMap[c] = MyKeyState.Held;
                    shouldUpdate[c] = !shouldUpdate[c];
                    break;
                case MyKeyState.Up:
                    if (shouldUpdate[c]) mKeyStateMap[c] = MyKeyState.None;
                    shouldUpdate[c] = !shouldUpdate[c];
                    break;
            }
        }
	}
}
