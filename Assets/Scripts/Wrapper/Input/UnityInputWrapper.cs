using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class UnityInputWrapper : InputWrapper
{
    private Action<List<KeyCode>> pressedKeysAction;
    private Action<Vector2> mouseRotationAction;

    public void subscribe(Action<List<KeyCode>> pressedKeys)
    {
        pressedKeysAction = pressedKeys;
    }

    public void subscribeMouse(Action<Vector2> mouseRotation)
    {
        mouseRotationAction = mouseRotation;
    }

    public void update()
    {
        var usedKeys = new List<KeyCode>()
        {
            KeyCode.W,
            KeyCode.A,
            KeyCode.S,
            KeyCode.D,
            KeyCode.Space
        };
        var pressedKeys = usedKeys.Where(key => Input.GetKey(key)).ToList();
        if (pressedKeys.Any())
        {
            pressedKeysAction(pressedKeys);
        }

        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");
        mouseRotationAction(new Vector2(horizontalRotation, verticalRotation));
    }
}
