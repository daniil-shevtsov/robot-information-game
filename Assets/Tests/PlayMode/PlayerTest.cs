using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    private GameObject playerObject;
    private Player player;

    [OneTimeSetUp]
    public void Setup()
    {
        playerObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Player"),
            new Vector3(0.0f, 0.0f, 0.0f),
            Quaternion.identity
        );
        player = playerObject.GetComponent<Player>();
    }

    [UnityTest]
    public IEnumerator PlayerShouldStayOnTheGround()
    {
        playerObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        yield return null;
        Assert.That(0f, Is.EqualTo(playerObject.transform.position.y).Within(0.01));
        yield return null;
        Assert.That(0f, Is.EqualTo(playerObject.transform.position.y).Within(0.01));
    }

    [UnityTest]
    public IEnumerator PlayerShouldFallWhenInAir()
    {
        playerObject.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
        yield return null;
        Assert.That(8f, Is.EqualTo(playerObject.transform.position.y).Within(0.01));
        yield return null;
        Assert.That(6f, Is.EqualTo(playerObject.transform.position.y).Within(0.01));
    }
}
