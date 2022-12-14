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
    private STATE state = STATE.GAME;
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

    private void Start()
    {
        CURRENTSTATE = STATE.TITLE;
        GameStart += StartOilTimer;
    }
    public void ChangeState()
    {
        CURRENTSTATE++;
        if (CURRENTSTATE > STATE.UPGRADE)
        {
            CURRENTSTATE = STATE.TITLE;
        }
    }
    public void OnStateChanged()
    {
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
        TitleCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        UpgradeCanvas.SetActive(false);
        ResultCanvas.SetActive(false);
    }
    public void OnGameCanvas()
    {
        GameStart?.Invoke();

        TitleCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        UpgradeCanvas.SetActive(false);
        ResultCanvas.SetActive(false);

    }
    public void OnUpgradeCanvas()
    {
        TitleCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        UpgradeCanvas.SetActive(true);
        ResultCanvas.SetActive(false);
    }
    public void OnResultCanvas()
    {
        TitleCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        UpgradeCanvas.SetActive(false);
        ResultCanvas.SetActive(true);
    }
    public void ChangeSettingOn()
    {
        SettingOn = !SettingOn;
    }
    public void OnSettingCanvas()
    {
        settingCanvas.SetActive(SettingOn);
    }
    public void GameOver()
    {
        CURRENTSTATE = STATE.RESULT;
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
            ChangeState();
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

}