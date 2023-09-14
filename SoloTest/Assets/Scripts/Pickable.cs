using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable
{
    public string pickableName;
    public int quantity;
    public Sprite itemSprite;

    public Pickable()
    {

    }
    public Pickable(string name, int quantity, Sprite itemSprite)
    {
        pickableName = name;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
    }

    public string getPickableName()
    {
        return pickableName;
    }

    public void setPickableName(string pickableName)
    {
        this.pickableName = pickableName;
    }

    public int getQuantity()
    {
        return quantity;
    }

    public void setQuantity(int quantity)
    {
        this.quantity = quantity;
    }

    public Sprite getItemSprite()
    {
        return itemSprite;
    }

    public void setItemSprite(Sprite itemSprite)
    {
        this.itemSprite = itemSprite;
    }
}
