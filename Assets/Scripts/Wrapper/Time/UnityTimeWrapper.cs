using UnityEngine;

public class UnityTimeWrapper : TimeWrapper
{
    public float deltaTime()
    {
        return Time.deltaTime;
    }
}
