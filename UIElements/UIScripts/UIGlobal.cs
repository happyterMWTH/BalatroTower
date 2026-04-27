using UnityEngine;

public class UIGlobal : MonoBehaviour
{
    [SerializeField] private RectTransform panel1;
    [SerializeField] private RectTransform panel2;
    
    private RectTransform[] panels;
    private int currentIndex = 0;

    // Visible and hidden positions
    private float visibleTop = 336f + 2f / 3f;
    private float visibleBottom = 0f;

    // Hidden positions
    private float hiddenTop = 506f + 1f / 3f;
    private float hiddenBottom = -169f - 2f / 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panels = new RectTransform[] { panel1, panel2 };
        UpdatePanelPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatePanelPositions()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == currentIndex)
            {
                SetPanelOffsets(panels[i], visibleTop, visibleBottom);
            }
            else
            {
                SetPanelOffsets(panels[i], hiddenTop, hiddenBottom);
            }
        }
    }

    private void SetPanelOffsets(RectTransform panel, float top, float bottom)
    {
        // Keep horizontal stretch (left/right) as is
        panel.offsetMax = new Vector2(panel.offsetMax.x, -top); // Top offset is negative
        panel.offsetMin = new Vector2(panel.offsetMin.x, bottom); // Bottom offset
    }


    public void CarouselPanels()
    {
        currentIndex++;
        if (currentIndex >= panels.Length)
        {
            currentIndex = 0;
        }

        UpdatePanelPositions();
    }
}
