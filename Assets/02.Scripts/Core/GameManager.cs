using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum STATE
    {
        NONE,
        TITLE,
        GAME,
        RESULT,
        UPGRADE,
    }
    public STATE state = STATE.NONE;
    bool settingOn = false;
    public bool SettingOn
    {
        get
        {
            return settingOn;
        }
        set
        {
            if (settingOn != value)
            {
                settingOn = value;
                OnSettingCanvas();
                Time.timeScale = settingOn ? 0f : 1f;
            }
        }
    }
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
    [SerializeField] GameObject ResultCanvas;
    [SerializeField] GameObject UpgradeCanvas;
    [SerializeField] GameObject settingCanvas;
    [SerializeField] float defaultOilTime;
    float maxOilTime;
    float currentOilTimer;

    public Action GameStart;
    public Action GameEnd;

    private void Start()
    {
        OnTitleCanvas();
        GameStart += StartOilTimer;
    }
    public void ChangeState(int state)
    {
        CURRENTSTATE = (STATE)state;
    }
    public void OnStateChanged()
    {
        SoundManager.Inst.TurnScene(CURRENTSTATE);
        if (state == STATE.TITLE)
        {
            OnTitleCanvas();
        }
        else if (state == STATE.GAME)
        {
            OnGameCanvas();
        }
        else if (state == STATE.UPGRADE)
        {
            OnUpgradeCanvas();
        }
        else if (state == STATE.RESULT)
        {
            OnResultCanvas();
        }
    }
    public void OnTitleCanvas()
    {
        ChangeState((int)STATE.TITLE);
        TitleCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        UpgradeCanvas.SetActive(false);
        ResultCanvas.SetActive(false);
    }
    public void OnGameCanvas()
    {
        GameStart?.Invoke();

        ChangeState((int)STATE.GAME);
        TitleCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        UpgradeCanvas.SetActive(false);
        ResultCanvas.SetActive(false);
    }
    public void OnUpgradeCanvas()
    {
        ChangeState((int)STATE.UPGRADE);
        UIManager.Inst.SetGoldText();
        TitleCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        UpgradeCanvas.SetActive(true);
        ResultCanvas.SetActive(false);
    }
    public void OnResultCanvas()
    {
        ChangeState((int)STATE.RESULT);
        TitleCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        UpgradeCanvas.SetActive(false);
        ResultCanvas.SetActive(true);
    }
    public void ChangeSettingOn()
    {
        SettingOn = !settingOn;

    }

    public void OnSettingCanvas()
    {
        settingCanvas.SetActive(SettingOn);
    }
    public void GameOver()
    {
        CURRENTSTATE = STATE.RESULT;
        GameEnd?.Invoke();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            foreach (Fish fish in FindObjectOfType<Aquarium>().containFish)
                Debug.Log($"Price: {fish.price}");
        }
        if (CURRENTSTATE == STATE.RESULT && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState((int)STATE.UPGRADE);
            {
                CURRENTSTATE = STATE.UPGRADE;
            }
        }
    }
    public void StartOilTimer()
    {
        maxOilTime = defaultOilTime + DataManager.Inst.FindUpgradeData(EUpgradeDataType.MaxOilAmount).level * 10;
        currentOilTimer = maxOilTime;
        StopAllCoroutines();
        StartCoroutine(OilTimerCoroutine());
    }

    private IEnumerator OilTimerCoroutine()
    {
        while (currentOilTimer > 0f)
        {
            UIManager.Inst.ChangeTimePanelAmount(currentOilTimer / maxOilTime);
            currentOilTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GameOver();
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}