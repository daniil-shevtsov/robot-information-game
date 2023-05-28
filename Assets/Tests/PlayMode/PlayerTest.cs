using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    private GameObject playerObject;
    private Player player;

    private FakeTimeWrapper fakeTimeWrapper = new FakeTimeWrapper();

    [OneTimeSetUp]
    public void Setup()
    {
        playerObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Player"),
            new Vector3(0.0f, 0.0f, 0.0f),
            Quaternion.identity
        );
        player = playerObject.GetComponent<Player>();
        player.timeWrapper = fakeTimeWrapper;

        player.fallingSpeed = 2.0f;
        player.walkingSpeed = 1.5f;
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
        player.onInput(0f, 1f);
        yield return null;
        assertFloats(1.5f, playerObject.transform.position.z);
        player.onInput(0f, 1f);
        yield return null;
        assertFloats(3f, playerObject.transform.position.z);
    }

    [UnityTest]
    public IEnumerator ShouldStepRight()
    {
        player.onInput(1f, 0f);
        yield return null;
        assertFloats(1.5f, playerObject.transform.position.x);
        player.onInput(1f, 0f);
        yield return null;
        assertFloats(3f, playerObject.transform.position.x);
    }

    private void assertFloats(float expected, float actual)
    {
        Assert.That(actual, Is.EqualTo(expected).Within(0.02));
    }
}
