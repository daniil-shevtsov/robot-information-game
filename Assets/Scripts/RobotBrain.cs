using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBrain : MonoBehaviour
{
    private Menu menu = createMenu();
    public Panel mainPanel
    {
        get { return menu.panels[0]; }
        set { }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void click(string buttonId) { }

    private static Menu createMenu()
    {
        return new Menu(
            new List<Panel>()
            {
                new Panel(
                    0L,
                    "Main Menu",
                    new List<MenuItem>()
                    {
                        new MenuItem(0L, "Controls"),
                        new MenuItem(1L, "Memory")
                    }
                ),
                new Panel(
                    1L,
                    "Controls",
                    new List<MenuItem>()
                    {
                        new MenuItem(0L, "Controls Option 1"),
                        new MenuItem(1L, "Controls Option 2")
                    }
                ),
                new Panel(
                    2L,
                    "Memory",
                    new List<MenuItem>()
                    {
                        new MenuItem(0L, "Memory Option 1"),
                        new MenuItem(1L, "Memory Option 2")
                    }
                )
            }
        );
    }
}

public class Menu
{
    public List<Panel> panels { get; }
    long activePanelId;

    public Menu(List<Panel> panels)
    {
        this.panels = panels;
    }
}

public class Panel : MyView
{
    long id;
    string title;
    List<MenuItem> items;

    public Panel(long id, string title, List<MenuItem> items)
    {
        this.id = id;
        this.title = title;
        this.items = items;
    }

    bool MyView.isVisible()
    {
        return true;
    }
}

public class MenuItem
{
    long id;
    string title;

    public MenuItem(long id, string title)
    {
        this.id = id;
        this.title = title;
    }
}

public interface MyView
{
    bool isVisible();
}
