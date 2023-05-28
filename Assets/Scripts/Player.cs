using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fallingSpeed = 0.002f;
    public float walkingSpeed = 0.05f;
    public TimeWrapper timeWrapper = new UnityTimeWrapper();

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition =
            transform.position - new Vector3(0f, fallingSpeed, 0f) * timeWrapper.deltaTime();
        if (newPosition.y > 0f)
        {
            transform.position = newPosition;
        }
    }

    public void onInput(float right, float forward)
    {
        transform.position =
            transform.position
            + new Vector3(right, 0f, forward) * walkingSpeed * timeWrapper.deltaTime();
    }
}
