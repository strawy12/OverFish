using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GAMESTATE
    {
        TITLE,
        GAME,
        UPGRADE,
        RESULT,
    }
    private GAMESTATE state;
    public GAMESTATE STATE
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
    public void OnStateChanged()
    {
        if (state == GAMESTATE.TITLE)
        {
            TitleCanvas.SetActive(true);
            GameCanvas.SetActive(false);
            UpgradeCanvas.SetActive(false);
            ResultCanvas.SetActive(false);
        }
        else if (state == GAMESTATE.GAME)
        {
            TitleCanvas.SetActive(false);
            GameCanvas.SetActive(true);
            UpgradeCanvas.SetActive(false);
            ResultCanvas.SetActive(false);
        }
        else if (state == GAMESTATE.UPGRADE)
        {
            TitleCanvas.SetActive(false);
            GameCanvas.SetActive(false);
            UpgradeCanvas.SetActive(true);
            ResultCanvas.SetActive(false);
        }
        else if (state == GAMESTATE.RESULT)
        {
            TitleCanvas.SetActive(false);
            GameCanvas.SetActive(false);
            UpgradeCanvas.SetActive(false);
            ResultCanvas.SetActive(true);
        }
    }
}