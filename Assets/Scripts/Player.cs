using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [NonSerialized]
    public float fallingSpeed = 3.9f;

    [NonSerialized]
    public float walkingSpeed = 5f;

    [NonSerialized]
    public float jumpingSpeed = 5f;
    public TimeWrapper timeWrapper = new UnityTimeWrapper();
    public InputWrapper inputWrapper = new UnityInputWrapper();

    private bool isJumpPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        float currentY = transform.position.y;
        float yChange = 0;
        if (currentY > 0)
        {
            yChange = -fallingSpeed;
        }
        if (isJumpPressed)
        {
            yChange += jumpingSpeed;
            isJumpPressed = false;
        }

        float newY = currentY + yChange * timeWrapper.deltaTime();

        if (newY < 0f)
        {
            newY = 0f;
        }
        Vector3 newPosition = new Vector3(transform.position.x, newY, transform.position.z);
        transform.position = newPosition;

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

        if (pressedKeys.Contains(KeyCode.Space))
        {
            isJumpPressed = true;
        }
    }

    private void onInput(float right, float forward)
    {
        var oldPosition = transform.position;
        var speed = walkingSpeed;
        if (right != 0.0f && forward != 0.0f)
        {
            speed = walkingSpeed / 2;
        }
        var newPosition =
            transform.position + new Vector3(right, 0f, forward) * speed * timeWrapper.deltaTime();

        transform.position = newPosition;
        Debug.Log(
            $"walking speed {walkingSpeed} right: {right} forward {forward} old: {oldPosition} new: {newPosition}"
        );
    }
}
