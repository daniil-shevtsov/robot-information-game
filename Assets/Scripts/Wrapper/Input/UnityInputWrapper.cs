using UnityEngine;
using System;
using System.Collections.Generic;

public class UnityInputWrapper : InputWrapper
{
    private Action<List<KeyCode>> pressedKeys;

    public void subscribe(Action<List<KeyCode>> pressedKeys)
    {
        this.pressedKeys = pressedKeys;
    }

    public void update()
    {
        var inputString = Input.inputString;
        Debug.Log($"inputString {inputString}");
        if (inputString != "")
        {
            var keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), inputString.ToUpper());

            if (keyCode != null)
            {
                Debug.Log($"key pressed {keyCode}");
                if (pressedKeys != null)
                {
                    pressedKeys(new List<KeyCode>() { keyCode });
                }
                else
                {
                    Debug.Log("key pressed callback not set");
                }
            }
        }
    }
}
