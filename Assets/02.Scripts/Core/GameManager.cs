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

    [SerializeField] float maxTime;
    float currentTimer;

    private void Start()
    {
        StartTimer();
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

    public void StartTimer()
    {
        currentTimer = maxTime;
        StopAllCoroutines();
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (currentTimer > 0f)
        {
            UIManager.Inst.ChangeTimePanelAmount(currentTimer/maxTime);
            currentTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}