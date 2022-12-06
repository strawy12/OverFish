using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    [SerializeField]
    private List<PanelButton> panelBtns = new List<PanelButton>();
    [SerializeField]
    private List<UpgradePanelContent> contents = new List<UpgradePanelContent>();

    private UpgradePanelContent currentContent;

    private void Start()
    {
        foreach(PanelButton btn in panelBtns)
        {
            EPanelType type = btn.PanelType;

           UpgradePanelContent content = contents.Find(x => x.panelType == type);

            btn.OnSelected += () => OnClickPanelBtn(content);
        }
    }

    private void OnClickPanelBtn(UpgradePanelContent content)
    {
        if (content == currentContent) return;

        foreach (UpgradePanelContent c in contents)
        {
            if (c == content) continue;

            c.gameObject.SetActive(false);
        }

        content.gameObject.SetActive(true);

        currentContent = content;
    }
}
