using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RobotBrain : MonoBehaviour
{
    private Menu menu = createMenu();

    public GameObject codePanel;
    public GameObject image;

    public Panel mainPanel
    {
        get { return menu.panels[0]; }
    }
    public Panel controls
    {
        get { return menu.panels[1]; }
    }

    // Start is called before the first frame update
    void Start()
    {
        renderMenu(menu);
    }

    // Update is called once per frame
    void Update() { }

    public void click(long buttonId)
    {
        Panel currentPanel = menu.panels.Find(panel => panel.id == menu.getActivePanelId());
        MenuItem clickedItem = currentPanel.items.Find(item => item.id == buttonId);
        menu.setActivePaneId(clickedItem.openPanelId);
        renderMenu(menu);
    }

    private void renderMenu(Menu menu)
    {
        Panel currentPanel = menu.panels.Find(panel => panel.id == menu.getActivePanelId());

        float scaler = 0.0125f;
        Vector3 change = new Vector3(20 * scaler, 0, 0);
        int index = 0;

        currentPanel.items.ForEach(item =>
        {
            renderItem(item, index);
            index++;
        });
    }

    private void renderItem(MenuItem item, int index)
    {
        GameObject controls = GameObject.Instantiate(image, transform.position, transform.rotation);
        controls.transform.SetParent(codePanel.transform, false);
        RectTransform rectTransform = controls.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(index * rectTransform.rect.width, 0);
        controls.GetComponent<TextMeshProUGUI>().text = item.title;
    }

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
                        new MenuItem(0L, "Controls", 1L),
                        new MenuItem(1L, "Memory", 2L)
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
    private long activePanelId;

    public Menu(List<Panel> panels)
    {
        this.panels = panels;
    }

    public long getActivePanelId()
    {
        return activePanelId;
    }

    public void setActivePaneId(long activePanelId)
    {
        this.activePanelId = activePanelId;
        panels.ForEach(panel => panel.setVisible(panel.id == activePanelId));
    }
}

public class Panel : MyView
{
    public long id;
    string title;
    public List<MenuItem> items;
    private bool isVisible = true;

    public Panel(long id, string title, List<MenuItem> items)
    {
        this.id = id;
        this.title = title;
        this.items = items;
    }

    bool MyView.isVisible()
    {
        return isVisible;
    }

    public void setVisible(bool isVisible)
    {
        this.isVisible = isVisible;
    }
}

public class MenuItem
{
    public long id;
    public string title;

    public long openPanelId;

    public MenuItem(long id, string title, long openPanelId = -1L)
    {
        this.id = id;
        this.title = title;
        this.openPanelId = openPanelId;
    }
}

public interface MyView
{
    bool isVisible();
    void setVisible(bool isVisible);
}
