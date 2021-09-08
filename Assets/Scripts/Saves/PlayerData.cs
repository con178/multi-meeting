using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int genderID;
    public PlayerData(GameManager GameManager)
    {
        genderID = GameManager.genderID;
    }
    
}
