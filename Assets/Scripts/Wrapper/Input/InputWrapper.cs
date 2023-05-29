using System;
using System.Collections.Generic;
using UnityEngine;

public interface InputWrapper
{
    void subscribe(Action<List<KeyCode>> pressedKeys);
    void update();
}
