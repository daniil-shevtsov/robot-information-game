using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    private GameObject playerObject;
    private Player player;

    private FakeTimeWrapper timeWrapper;
    private FakeInputWrapper inputWrapper;

    [OneTimeSetUp]
    public void TestSuitSetup()
    {
        playerObject = MonoBehaviour.Instantiate(
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

        player = playerObject.GetComponent<Player>();
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
    public IEnumerator ShouldStayOnTheGround()
    {
        playerObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        yield return null;
        assertFloats(0f, playerObject.transform.position.y);
        yield return null;
        assertFloats(0f, playerObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator ShouldFallWhenInAir()
    {
        playerObject.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
        yield return null;
        assertFloats(8f, playerObject.transform.position.y);
        yield return null;
        assertFloats(6f, playerObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator ShouldFallUntilOnTheGround()
    {
        playerObject.transform.position = new Vector3(0.0f, 3.0f, 0.0f);
        yield return null;
        assertFloats(1f, playerObject.transform.position.y);
        yield return null;
        assertFloats(0f, playerObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator ShouldFallWithConfiguredSpeedWhenInAir()
    {
        player.fallingSpeed = 4f;
        playerObject.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
        yield return null;
        assertFloats(6f, playerObject.transform.position.y);
        yield return null;
        assertFloats(2f, playerObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator ShouldStepForward()
    {
        inputWrapper.pressKey(KeyCode.W);
        yield return null;
        assertFloats(1.5f, playerObject.transform.position.z);
        inputWrapper.pressKey(KeyCode.W);
        yield return null;
        assertFloats(3f, playerObject.transform.position.z);
    }

    [UnityTest]
    public IEnumerator ShouldStepRight()
    {
        inputWrapper.pressKey(KeyCode.D);
        yield return null;
        assertFloats(1.5f, playerObject.transform.position.x);
        inputWrapper.pressKey(KeyCode.D);
        yield return null;
        assertFloats(3f, playerObject.transform.position.x);
    }

    [UnityTest]
    public IEnumerator ShouldStepDiagonallyWithHalfSpeed()
    {
        inputWrapper.pressKeys(new List<KeyCode>() { KeyCode.W, KeyCode.D });
        yield return null;
        Assert.That(inputWrapper.useCounter, Is.EqualTo(1));
        assertFloats(0.75f, playerObject.transform.position.x);
        assertFloats(0.75f, playerObject.transform.position.z);
        inputWrapper.pressKeys(new List<KeyCode>() { KeyCode.W, KeyCode.D });
        yield return null;
        assertFloats(1.5f, playerObject.transform.position.x);
        assertFloats(1.5f, playerObject.transform.position.z);
    }

    [UnityTest]
    public IEnumerator ShouldJump()
    {
        inputWrapper.pressKey(KeyCode.Space);
        yield return null;
        assertFloats(3.0f, playerObject.transform.position.y);
        //inputWrapper.pressKey(KeyCode.Space);
        yield return null;
        assertFloats(1.0f, playerObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator ShouldRotateCameraHorizontallyClockwise()
    {
        inputWrapper.moveMouse(60f, 0f);
        yield return null;
        assertFloats(60.0f, playerObject.transform.rotation.eulerAngles.y);
    }

    [UnityTest]
    public IEnumerator ShouldRotateCameraHorizontallyCounterclockwise()
    {
        inputWrapper.moveMouse(-60f, 0f);
        yield return null;
        assertFloats(300.0f, playerObject.transform.rotation.eulerAngles.y);
    }

    [UnityTest]
    public IEnumerator ShouldWalkInDirectionOfCamera()
    {
        inputWrapper.moveMouse(90f, 0f);
        yield return null;
        inputWrapper.pressKey(KeyCode.W);
        yield return null;
        assertFloats(1.5f, playerObject.transform.position.x);
    }

    private void assertFloats(float expected, float actual)
    {
        Assert.That(actual, Is.EqualTo(expected).Within(0.02));
    }
}
