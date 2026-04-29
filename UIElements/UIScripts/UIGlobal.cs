using UnityEngine;

public class UIGlobal : MonoBehaviour
{
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject panel3;

    private GameObject[] panels;

    void Start()
    {
        panels = new GameObject[] { panel1, panel2, panel3 };
        ShowPanel(0);
    }

    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index);
        }
    }
}
