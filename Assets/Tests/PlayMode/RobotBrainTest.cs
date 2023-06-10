using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class RobotBrainTest
{
    private GameObject gameObject;
    private GameObject robotBrainObject;
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
        robotBrainObject = MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/RobotBrain"),
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
        player.robotBrain = robotBrainObject.GetComponent<RobotBrain>();
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
    public IEnumerator MainPanelShouldBeVisibleInitially()
    {
        assertVisible(player.robotBrain.mainPanel, true);

        yield return null;
    }

    private void assertVisible(GameObject gameObject, bool isVisible)
    {
        float expectedAlpha = isVisible ? 1 : 0;
        Assert.That(gameObject.GetComponent<CanvasGroup>().alpha, Is.EqualTo(expectedAlpha));
    }
}
