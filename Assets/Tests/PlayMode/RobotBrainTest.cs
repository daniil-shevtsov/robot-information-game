using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class RobotBrainTest
{
    private GameObject gameObject;
    private RobotBrain robotBrain;

    [OneTimeSetUp]
    public void TestSuitSetup()
    {
        gameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/RobotBrain"),
            new Vector3(0.0f, 0.0f, 0.0f),
            Quaternion.identity
        );
    }

    [SetUp]
    public void EachTestSetup() { }

    [UnityTest]
    public IEnumerator ShouldCompile()
    {
        yield return null;
    }
}
