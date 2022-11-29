using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define 
{
    private static Player currentPlayer;

    public static Player CurrentPlayer
    {
        get
        {
            if(currentPlayer == null)
            {
                currentPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            return currentPlayer;
        }
    }
}
