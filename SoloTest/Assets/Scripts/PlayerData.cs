using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Player Stats Variables
    public int maxHP;
    public int currentHP;
    public int money;
    public int currentXP;
    public int maxXP;
    public int level;
    private PlayerMovement player = PlayerMovement.instance;

    //Player's
    public string[] pickableName;
    public int[] quantity;

    //public float[] position;

    public PlayerData()
    {
        maxHP = player.maxHP;
        currentHP = player.currentHP;
        money = player.money;
        currentXP = player.currentXP;
        maxXP = player.maxXP;
        level = player.level;

        /*pickableName = new string[5];
        quantity = new int[5];

        for (int i = 0; i < Player.joseInventoryCopy.itemSlots.Length; i++)
        {
            pickableName[i] = Player.joseInventoryCopy.itemSlots[i].getPickableName();
            quantity[i] = Player.joseInventoryCopy.itemSlots[i].getQuantity();
        }*/
    }
}
