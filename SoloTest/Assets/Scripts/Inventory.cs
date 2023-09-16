using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    // This class will simulate the inventory, which will be instantiated with the player, the inventory attributes will be
    // an array of gameObjects (items) and the inventory must have a CRUD (Create,Read,Update,Delete) system the items will be added
    // to the inventory once they get pickedUp, if the item is not stackable it must show a text with the quantity of items stacked
    // instead of adding the same item in another slot.

    //Note: If the inventory is full or if the player drops an item, we have to make the "Pickable" class not absorb the items nor pick'em up. 
    //The inventory, once working must be able to be stored using the playerData class

    public Pickable[] itemSlots;

    public Inventory(int numberOfSlots)
    {
        itemSlots = new Pickable[numberOfSlots];
    }

    public void addItem(Pickable pickable) //Will add item to inventory
    {
        for (int i = 0; i < PlayerMovement.joseInventory.itemSlots.Length; i++)
        {
            if (PlayerMovement.joseInventory.itemSlots[i] == null)
            {
                PlayerMovement.joseInventory.itemSlots[i] = new Pickable(pickable.getPickableName(), pickable.getQuantity(), pickable.getItemSprite());
                arrayPrinter(PlayerMovement.joseInventory);
                return;
            }
            else if (PlayerMovement.joseInventory.itemSlots[i].getPickableName().Equals(pickable.getPickableName()))
            {
                PlayerMovement.joseInventory.itemSlots[i].quantity += pickable.getQuantity();
                arrayPrinter(PlayerMovement.joseInventory);
                return;
            }
        }
    }

    public void removeItem(string pickableName)
    {
        for (int i = 0; i < PlayerMovement.joseInventory.itemSlots.Length; i++)
        {
            if (PlayerMovement.joseInventory.itemSlots[i].getPickableName().Equals(pickableName))
            {
                PlayerMovement.joseInventory.itemSlots[i] = null;
            }
        }
    }

    public void arrayPrinter(Inventory joseInventory)
    {
        for (int i = 0; i < joseInventory.itemSlots.Length; i++)
        {
            if (joseInventory.itemSlots[i] != null)
            {
                Debug.Log(joseInventory.itemSlots[i].getPickableName() + " " + joseInventory.itemSlots[i].getQuantity());
            }
        }
    }
}
