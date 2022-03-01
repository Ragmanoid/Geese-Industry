using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public Vector2 iconSpacing;
    public Button NextButton;
    public Button PrevButton;
    public Button BackButton;

    private const int NumberOfLevels = 15;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int amountPerPage;
    private int currentLevelCount;
    private PageSwiper swiper;

    private void Start()
    {
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        var maxInARow =
            Mathf.FloorToInt((panelDimensions.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        var maxInACol =
            Mathf.FloorToInt((panelDimensions.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));
        amountPerPage = maxInARow * maxInACol;
        var totalPages = Mathf.CeilToInt((float) NumberOfLevels / amountPerPage);
        
        LoadPanels(totalPages);

        // Setup button handlers
        NextButton.GetComponent<Button>().onClick.AddListener(() => swiper.MovePage(1));
        PrevButton.GetComponent<Button>().onClick.AddListener(() => swiper.MovePage(-1));
        BackButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Home"));
    }

    private void LoadPanels(int numberOfPanels)
    {
        var panelClone = Instantiate(levelHolder);

        swiper = levelHolder.AddComponent<PageSwiper>();
        swiper.totalPages = numberOfPanels;

        for (var i = 1; i <= numberOfPanels; i++)
        {
            var panel = Instantiate(panelClone);
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Page-" + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i - 1), 0);
            SetUpGrid(panel);
            var numberOfIcons = i == numberOfPanels ? NumberOfLevels - currentLevelCount : amountPerPage;
            LoadIcons(numberOfIcons, panel);
        }

        Destroy(panelClone);
    }

    private void SetUpGrid(GameObject panel)
    {
        var grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = iconSpacing;
    }

    private void LoadIcons(int numberOfIcons, GameObject parentObject)
    {
        for (var i = 1; i <= numberOfIcons; i++)
        {
            currentLevelCount++;
            var icon = Instantiate(levelIcon);
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level " + i;
            var img = icon.GetComponentsInChildren<Image>()[1];
            // img.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Levels/{i.ToString()}");
            img.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Levels/1");
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Уровень " + currentLevelCount);

            var trigger = icon.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            var levelNumber = i;
            entry.callback.AddListener(eventData =>
            {
                SceneManager.LoadScene($"Scenes/Levels/{levelNumber}");
            });
            trigger.triggers.Add(entry);
        }
    }
}