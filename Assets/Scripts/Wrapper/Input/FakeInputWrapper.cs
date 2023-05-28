using UnityEngine;
using System;

public class FakeInputWrapper : InputWrapper
{
    private Action<KeyCode> keyPressed;

    public void subscribe(Action<KeyCode> keyPressed)
    {
        this.keyPressed = keyPressed;
    }

    public void update() { }

    public void pressKey(KeyCode keyCode)
    {
        keyPressed(keyCode);
    }
}
