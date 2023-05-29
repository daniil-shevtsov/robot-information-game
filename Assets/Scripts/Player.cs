using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [NonSerialized]
    public float fallingSpeed = 0.002f;

    [NonSerialized]
    public float walkingSpeed = 5f;
    public TimeWrapper timeWrapper = new UnityTimeWrapper();
    public InputWrapper inputWrapper = new UnityInputWrapper();

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition =
            transform.position - new Vector3(0f, fallingSpeed, 0f) * timeWrapper.deltaTime();
        if (newPosition.y > 0f)
        {
            transform.position = newPosition;
        }

        inputWrapper.update();
    }

    public void Init()
    {
        inputWrapper.subscribe(handleKeyPressed);
    }

    private void handleKeyPressed(List<KeyCode> pressedKeys)
    {
        float forward = 0f;
        float right = 0f;
        if (pressedKeys.Contains(KeyCode.W))
        {
            forward = 1f;
        }
        else if (pressedKeys.Contains(KeyCode.S))
        {
            forward = -1f;
        }

        if (pressedKeys.Contains(KeyCode.A))
        {
            right = -1f;
        }
        else if (pressedKeys.Contains(KeyCode.D))
        {
            right = 1f;
        }
        onInput(right, forward);
    }

    private void onInput(float right, float forward)
    {
        var oldPosition = transform.position;
        var newPosition =
            transform.position
            + new Vector3(right, 0f, forward) * walkingSpeed * timeWrapper.deltaTime();

        transform.position = newPosition;
        Debug.Log(
            $"walking speed {walkingSpeed} right: {right} forward {forward} old: {oldPosition} new: {newPosition}"
        );
    }
}
