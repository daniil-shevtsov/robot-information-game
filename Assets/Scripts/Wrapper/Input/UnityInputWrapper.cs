using UnityEngine;
using System;

public class UnityInputWrapper : InputWrapper
{
    private Action<KeyCode> keyPressed;

    public void subscribe(Action<KeyCode> keyPressed)
    {
        this.keyPressed = keyPressed;
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
                if (keyPressed != null)
                {
                    keyPressed(keyCode);
                }
                else
                {
                    Debug.Log("key pressed callback not set");
                }
            }
        }
    }
}
