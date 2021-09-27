using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo 
{
    public static PlayerInfo instance = new PlayerInfo();

    public string Name;
    public int Currency;

    public PlayerInfo()
    {
        Name = "Cat";
        Currency = 0;
    }
}
