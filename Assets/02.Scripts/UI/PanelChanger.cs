using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelChanger : MonoBehaviour
{
    [SerializeField]
    private List<PanelButton> panelBtns = new List<PanelButton>();
    [SerializeField]
    private List<UpgradePanelContent> contents = new List<UpgradePanelContent>();

    [SerializeField]
    private ScrollRect scrollRect;

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

            c.canvasGroup.alpha = 0f;
            c.canvasGroup.blocksRaycasts = false;
            c.canvasGroup.interactable = false;
            c.gameObject.SetActive(false);
        }

        content.canvasGroup.alpha = 1f;
        content.canvasGroup.blocksRaycasts = true;
        content.canvasGroup.interactable = true;
        content.gameObject.SetActive(true);

        currentContent = content;

        scrollRect.content = currentContent.rectTransform;
    }
}
