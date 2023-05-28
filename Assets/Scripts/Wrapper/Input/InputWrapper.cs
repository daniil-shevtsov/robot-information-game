using System;
using UnityEngine;

public interface InputWrapper
{
    void subscribe(Action<KeyCode> keyPressed);
    void update();
}
