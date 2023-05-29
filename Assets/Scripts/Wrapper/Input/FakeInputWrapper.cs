using UnityEngine;
using System;
using System.Collections.Generic;

public class FakeInputWrapper : InputWrapper
{
    private Action<List<KeyCode>> pressedKeys;

    public void subscribe(Action<List<KeyCode>> pressedKeys)
    {
        this.pressedKeys = pressedKeys;
    }

    public void update() { }

    public void pressKey(KeyCode keyCode)
    {
        pressedKeys(new List<KeyCode>() { keyCode });
    }
}
