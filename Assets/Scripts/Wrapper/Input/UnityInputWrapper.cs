using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class UnityInputWrapper : InputWrapper
{
    private Action<List<KeyCode>> pressedKeysAction;

    public void subscribe(Action<List<KeyCode>> pressedKeys)
    {
        pressedKeysAction = pressedKeys;
    }

    public void update()
    {
        var usedKeys = new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
        var pressedKeys = usedKeys.Where(key => Input.GetKey(key)).ToList();
        if (pressedKeys.Any())
        {
            pressedKeysAction(pressedKeys);
        }
    }
}
