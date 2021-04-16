using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class plot : MonoBehaviour
{
    public static plot instance;
    private void Awake()
    {
        instance = this;
    }
    public bool endedGame;


    public void  endGame()
    {
        endedGame = true;
    }
}
