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
    private STATE state = STATE.NONE;
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
    private void Start()
    {
        CURRENTSTATE = STATE.TITLE;
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
    public void GameOver()
    {
        CURRENTSTATE = STATE.RESULT;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            CURRENTSTATE++;
            if (CURRENTSTATE > STATE.RESULT)
            {
                CURRENTSTATE = STATE.TITLE;
            }
        }
    }
}