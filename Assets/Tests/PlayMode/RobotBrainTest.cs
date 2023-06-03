using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class RobotBrainTest
{
    private GameObject gameObject;
    private Player player;

    private FakeTimeWrapper timeWrapper;
    private FakeInputWrapper inputWrapper;

    [OneTimeSetUp]
    public void TestSuitSetup()
    {
        gameObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Player"),
            new Vector3(0.0f, 0.0f, 0.0f),
            Quaternion.identity
        );
    }

    [SetUp]
    public void EachTestSetup()
    {
        timeWrapper = new FakeTimeWrapper();
        inputWrapper = new FakeInputWrapper();

        player = gameObject.GetComponent<Player>();
        player.timeWrapper = timeWrapper;
        player.inputWrapper = inputWrapper;

        player.fallingSpeed = 2.0f;
        player.walkingSpeed = 1.5f;
        player.jumpingSpeed = 3f;

        player.Init();
        player.transform.position = new Vector3(0f, 0f, 0f);
        player.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    [UnityTest]
    public IEnumerator ShouldCompile()
    {
        yield return null;
    }
}
