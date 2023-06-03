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

    public RobotBrain robotBrain;

    [NonSerialized]
    public float jumpingSpeed = 5f;
    public TimeWrapper timeWrapper = new UnityTimeWrapper();
    public InputWrapper inputWrapper = new UnityInputWrapper();

    private Vector3 currentForce = new Vector3(0f, 0f, 0f);
    private Vector3 acceleration = new Vector3(0f, 0f, 0f);

    private bool isJumpPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        float yChange = 0;
        if (transform.position.y > 0)
        {
            yChange = -fallingSpeed;
        }
        if (isJumpPressed)
        {
            yChange += jumpingSpeed;
            isJumpPressed = false;
        }

        Vector3 newForce = new Vector3(0f, yChange, 0f);
        applyForce(newForce);

        inputWrapper.update();
    }

    public void Init()
    {
        inputWrapper.subscribe(handleKeyPressed);
        inputWrapper.subscribeMouse(handleMouseMoved);
    }

    private void handleKeyPressed(List<KeyCode> pressedKeys)
    {
        float forward = 0f;
        float right = 0f;
        if (isPressed(pressedKeys, KeyCode.W))
        {
            forward = 1f;
        }
        else if (isPressed(pressedKeys, KeyCode.S))
        {
            forward = -1f;
        }

        if (isPressed(pressedKeys, KeyCode.A))
        {
            right = -1f;
        }
        else if (isPressed(pressedKeys, KeyCode.D))
        {
            right = 1f;
        }
        onInput(right, forward);

        if (isPressed(pressedKeys, KeyCode.Space))
        {
            isJumpPressed = true;
        }
    }

    private void handleMouseMoved(Vector2 mouseRotation)
    {
        Vector3 currentAngles = transform.eulerAngles;
        currentAngles.y = transform.eulerAngles.y + mouseRotation.x;
        transform.eulerAngles = currentAngles;
    }

    private void onInput(float right, float forward)
    {
        var oldPosition = transform.position;
        var speed = walkingSpeed;
        if (right != 0.0f && forward != 0.0f)
        {
            speed = walkingSpeed / 2;
        }

        applyForce(
            Quaternion.Euler(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                transform.eulerAngles.z
            )
                * new Vector3(right, 0f, forward)
                * speed
        );
    }

    private void applyCurrentForce()
    {
        applyForce(currentForce);
    }

    private void applyForce(Vector3 force)
    {
        Vector3 newPosition = transform.position + force * timeWrapper.deltaTime();

        if (newPosition.y < 0f)
        {
            newPosition.y = 0f;
        }

        transform.position = newPosition;
    }

    private bool isPressed(List<KeyCode> pressedKeys, KeyCode key)
    {
        return pressedKeys.Contains(key);
    }
}
