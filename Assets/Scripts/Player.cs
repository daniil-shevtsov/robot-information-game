using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fallingSpeed = 2.0f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position - new Vector3(0f, fallingSpeed, 0f);
        if (newPosition.y > 0f)
        {
            transform.position = newPosition;
        }
    }
}
