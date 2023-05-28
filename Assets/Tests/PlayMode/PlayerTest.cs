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
    }

    [UnityTest]
    public IEnumerator PlayerShouldStayOnTheGround()
    {
        playerObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        yield return null;
        assertFloats(0f, playerObject.transform.position.y);
        yield return null;
        assertFloats(0f, playerObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator PlayerShouldFallWhenInAir()
    {
        playerObject.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
        yield return null;
        assertFloats(8f, playerObject.transform.position.y);
        yield return null;
        assertFloats(6f, playerObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator PlayerShouldFallWithConfiguredSpeedWhenInAir()
    {
        player.fallingSpeed = 4f;
        playerObject.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
        yield return null;
        assertFloats(6f, playerObject.transform.position.y);
        yield return null;
        assertFloats(2f, playerObject.transform.position.y);
    }

    private void assertFloats(float expected, float actual)
    {
        Assert.That(actual, Is.EqualTo(expected).Within(0.02));
    }
}
