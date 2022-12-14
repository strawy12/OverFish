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

    public static Vector3 MAX_POS => new Vector3(20f, 2.5f,4f);
    public static Vector3 MIN_POS => new Vector3(-9.5f, 2.5f, -11f);
}
