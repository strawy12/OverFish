using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum STATE
    {
        TITLE,
        GAME,
        UPGRADE,
        RESULT,
    }
    private STATE state = STATE.TITLE;
    public STATE CURRENTSTATE
    {
        get
        {
            return state;
        }
        set
        {
            if (state != value)
            {
                state = value;
                OnStateChanged();
            }
        }
    }

    [SerializeField] GameObject TitleCanvas;
    [SerializeField] GameObject GameCanvas;
    [SerializeField] GameObject UpgradeCanvas;
    [SerializeField] GameObject ResultCanvas;

    [SerializeField] float maxOilTime;
    float currentOilTimer;

    private void Start()
    {
        StartOilTimer();
    }

    public void OnStateChanged()
    {
        if (state == STATE.TITLE)
        {
            TitleCanvas.SetActive(true);
            GameCanvas.SetActive(false);
            UpgradeCanvas.SetActive(false);
            ResultCanvas.SetActive(false);
        }
        else if (state == STATE.GAME)
        {
            TitleCanvas.SetActive(false);
            GameCanvas.SetActive(true);
            UpgradeCanvas.SetActive(false);
            ResultCanvas.SetActive(false);
        }
        else if (state == STATE.UPGRADE)
        {
            TitleCanvas.SetActive(false);
            GameCanvas.SetActive(false);
            UpgradeCanvas.SetActive(true);
            ResultCanvas.SetActive(false);
        }
        else if (state == STATE.RESULT)
        {
            TitleCanvas.SetActive(false);
            GameCanvas.SetActive(false);
            UpgradeCanvas.SetActive(false);
            ResultCanvas.SetActive(true);
        }
    }

    public void StartOilTimer()
    {
        currentOilTimer = maxOilTime;
        StopAllCoroutines();
        StartCoroutine(OilTimerCoroutine());
    }

    private IEnumerator OilTimerCoroutine()
    {
        while (currentOilTimer > 0f)
        {
            UIManager.Inst.ChangeTimePanelAmount(currentOilTimer/maxOilTime);
            currentOilTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}