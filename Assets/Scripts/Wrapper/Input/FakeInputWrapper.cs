using UnityEngine;
using System;
using System.Collections.Generic;

public class FakeInputWrapper : InputWrapper
{
    public int useCounter = 0;
    private Action<List<KeyCode>> pressedKeysAction;

    public void subscribe(Action<List<KeyCode>> pressedKeys)
    {
        this.pressedKeysAction = pressedKeys;
    }

    public void update() { }

    public void pressKey(KeyCode pressedKey)
    {
        pressKeys(new List<KeyCode>() { pressedKey });
    }

    public void pressKeys(List<KeyCode> pressedKeys)
    {
        useCounter++;
        pressedKeysAction(pressedKeys);
    }
}
