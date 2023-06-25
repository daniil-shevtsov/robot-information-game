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
    private RobotBrain robotBrain;

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

        robotBrain = robotBrainObject.GetComponent<RobotBrain>();
    }

    [UnityTest]
    public IEnumerator MainPanelShouldBeVisibleInitially()
    {
        assertVisible(robotBrain.mainPanel, true);
        assertActivePanel(robotBrain.menu, robotBrain.mainPanel.id);
        assertTitle(robotBrain.activePanel, "Main Menu");
        assertHasItemWith("Controls", robotBrain.activePanel);
        assertHasItemWith("Memory", robotBrain.activePanel);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldSwitchToControlsWhenControlsClicked()
    {
        robotBrain.click(robotBrain.activePanel.items[0].id);
        assertVisible(robotBrain.mainPanel, false);
        assertVisible(robotBrain.controls, true);
        assertActivePanel(robotBrain.menu, robotBrain.controls.id);
        assertTitle(robotBrain.activePanel, "Controls");
        assertHasItemWith("Controls Option 1", robotBrain.activePanel);
        assertHasItemWith("Controls Option 2", robotBrain.activePanel);

        yield return null;
    }

    private void assertActivePanel(Menu menu, long expectedPanelId)
    {
        Assert.That(menu.getActivePanelId(), Is.EqualTo(expectedPanelId));
        Assert.That(menu.activePanel.id, Is.EqualTo(expectedPanelId));
    }

    private void assertHasItemWith(string title, Panel panel)
    {
        MenuItem item = panel.items.Find(item => item.title == title);
        Assert.That(
            item,
            Is.Not.Null,
            $"Panel ({panel.id} {panel.title}) does not have item with title={title}"
        );
    }

    private void assertText(MenuItem view, string expected)
    {
        Assert.That(view.title, Is.EqualTo(expected));
    }

    private void assertTitle(Panel panel, string expected)
    {
        Assert.That(panel.title, Is.EqualTo(expected));
    }

    private void assertVisible(MyView view, bool expected)
    {
        Assert.That(view.isVisible(), Is.EqualTo(expected));
        //float expectedAlpha = isVisible ? 1 : 0;
        // Assert.That(gameObject.GetComponent<CanvasGroup>().alpha, Is.EqualTo(expectedAlpha));
    }
}
