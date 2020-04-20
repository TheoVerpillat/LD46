﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    enum buttonValue {ADD, REMOVE, SELL, DONTSELL};
    public GameObject LinkedItem;
    public ShopInventory shopInventory;
    public ShopButton sellButton;


    private int buttonStatus;

    public void AddItem()
    {
        //Debug.Log("_____________________");
        Debug.Log("BUTTON PRESSED: " + this);
        //Debug.Log("BUTTON STATUS: " + buttonStatus);
        if (buttonStatus != 0)
        {
            //Debug.Log("BUTTON STATUS: " + buttonStatus);
            RemoveItem();
            return;
        }
        if (LinkedItem.GetComponent<Item>().itemType.Equals(Item.ItemType.WEAPON))
        {
            //Debug.Log("PRECEDENT ITEM: " + shopInventory.precedentWeaponButtonPressed);
            if (shopInventory.precedentWeaponButtonPressed != null)
            {
                ChangeButton(shopInventory.precedentWeaponButtonPressed, buttonValue.ADD);
                shopInventory.totalCost -= shopInventory.precedentWeaponButtonPressed.LinkedItem.GetComponent<Item>().cost;
                //shopInventory.precedentWeaponButtonPressed.RemoveItem();
            }
            shopInventory.temporaryCharacterItems[0] = LinkedItem;
            shopInventory.selledItems[0] = transform.parent.GetComponent<ItemSlot>();
            shopInventory.precedentWeaponButtonPressed = this;

        }
        else if (LinkedItem.GetComponent<Item>().itemType.Equals(Item.ItemType.ARMOR))
        {
            if (shopInventory.precedentArmorButtonPressed != null)
            {
                ChangeButton(shopInventory.precedentArmorButtonPressed, buttonValue.ADD);
                shopInventory.totalCost -= shopInventory.precedentArmorButtonPressed.LinkedItem.GetComponent<Item>().cost;

            }
            shopInventory.temporaryCharacterItems[1] = LinkedItem;
            shopInventory.selledItems[1] = transform.parent.GetComponent<ItemSlot>();
            shopInventory.precedentArmorButtonPressed = this;

        }
        else if (LinkedItem.GetComponent<Item>().itemType.Equals(Item.ItemType.SPECIALS))
        {
            if (shopInventory.precedentSpecialsButtonPressed != null)
            {
                ChangeButton(shopInventory.precedentSpecialsButtonPressed, buttonValue.ADD);
                shopInventory.totalCost -= shopInventory.precedentSpecialsButtonPressed.LinkedItem.GetComponent<Item>().cost;
            }
            shopInventory.temporaryCharacterItems[2] = LinkedItem;
            shopInventory.selledItems[2] = transform.parent.GetComponent<ItemSlot>();
            shopInventory.precedentSpecialsButtonPressed = this;
        }
        else if (LinkedItem.GetComponent<Item>().itemType.Equals(Item.ItemType.POTION))
        {
            shopInventory.temporaryHealth += LinkedItem.GetComponent<Item>().potionEfficacity;
        }

        shopInventory.totalCost += LinkedItem.GetComponent<Item>().cost;
        ChangeButton(this, buttonValue.REMOVE);
        ChangeButton(sellButton, buttonValue.SELL);
    }


    public void RemoveItem()
    {
        //Debug.Log("REMOVE ITEM");
        if (LinkedItem.GetComponent<Item>().itemType.Equals(Item.ItemType.WEAPON))
        {
            //Debug.Log("PRECEDENT ITEM: " + shopInventory.precedentWeaponButtonPressed);
            shopInventory.temporaryCharacterItems[0] = null;
            shopInventory.selledItems[0] = null;
            shopInventory.precedentWeaponButtonPressed = null;

        }
        else if (LinkedItem.GetComponent<Item>().itemType.Equals(Item.ItemType.ARMOR))
        {
            shopInventory.temporaryCharacterItems[1] = null;
            shopInventory.selledItems[1] = null;
            shopInventory.precedentArmorButtonPressed = null;

        }
        else if (LinkedItem.GetComponent<Item>().itemType.Equals(Item.ItemType.SPECIALS))
        {
            shopInventory.temporaryCharacterItems[2] = null;
            shopInventory.selledItems[2] = null;
            shopInventory.precedentSpecialsButtonPressed = null;
        }
        else if (LinkedItem.GetComponent<Item>().itemType.Equals(Item.ItemType.POTION))
        {
            shopInventory.temporaryHealth -= LinkedItem.GetComponent<Item>().potionEfficacity;
        }

        shopInventory.totalCost -= LinkedItem.GetComponent<Item>().cost;
        ChangeButton(this, buttonValue.ADD);
        if(shopInventory.temporaryCharacterItems[0] == null &&
            shopInventory.temporaryCharacterItems[1] == null &&
            shopInventory.temporaryCharacterItems[2] == null &&
            shopInventory.temporaryHealth == 0)
        {
            ChangeButton(sellButton, buttonValue.DONTSELL);
        }
    }

    public void sellItems()
    {
        for (int i = 0; i < shopInventory.temporaryCharacterItems.Length; i++)
        {
            if (shopInventory.temporaryCharacterItems[i] != null)
            {
                //Debug.Log("EMBALLÉ C'EST PESÉ");
                shopInventory.character.GetComponent<Character>().itemList[i] = shopInventory.temporaryCharacterItems[i];
                shopInventory.temporaryCharacterItems[i] = null;
            }
            else
            {
                //Debug.Log("Y a rien :'(");
            }
        }

        shopInventory.character.GetComponent<Character>().health += shopInventory.temporaryHealth;
        if (shopInventory.character.GetComponent<Character>().health > 12) shopInventory.character.GetComponent<Character>().health = 12;

        if (shopInventory.character.GetComponent<Character>().isMainCharacter) {
            shopInventory.gameManager.permanentCharacterHealth = shopInventory.character.GetComponent<Character>().health;
            shopInventory.gameManager.permanentCharacterInventory = shopInventory.character.GetComponent<Character>().itemList;
         }
        //Refresh Health
        shopInventory.money += shopInventory.totalCost;
        shopInventory.precedentWeaponButtonPressed = null;
        shopInventory.precedentArmorButtonPressed = null;
        shopInventory.precedentWeaponButtonPressed = null;
        ChangeButton(this, buttonValue.DONTSELL);
        shopInventory.totalCost = 0;
        RemoveSelledItems();
        shopInventory.gameManager.NextClient();
    }

    public void dontSellAnything()
    {
        //Add to Count
        shopInventory.gameManager.NextClient();
    }

    private void ChangeButton (ShopButton button, buttonValue value)
    {
        if (value.Equals(buttonValue.ADD))
        {
            button.buttonStatus = 0;
            button.GetComponent<Image>().sprite = shopInventory.shopButtonSprites[0];
            //Debug.Log("CHANGED TO ADD BUTTON");
        }
        else if (value.Equals(buttonValue.REMOVE))
        {
            button.buttonStatus = 1;
            button.GetComponent<Image>().sprite = shopInventory.shopButtonSprites[1];
            //Debug.Log("CHANGED TO REMOVE BUTTON");
        }
        else if (value.Equals(buttonValue.SELL))
        {
            button.buttonStatus = 2;
            //button.GetComponent<Image>().sprite = shopInventory.shopButtonSprites[2];
            button.GetComponentInChildren<TMP_Text>().text = "SELL\n" + "$"+shopInventory.totalCost;
            //Debug.Log("CHANGED TO SELL BUTTON");
        }
        else if (value.Equals(buttonValue.DONTSELL))
        {
            button.buttonStatus = 3;
            //button.GetComponent<Image>().sprite = shopInventory.shopButtonSprites[3];
            button.GetComponentInChildren<TMP_Text>().text = "DON'T SELL";
            //Debug.Log("CHANGED TO DONTSELL BUTTON");
        }

    }

    public void RemoveSelledItems()
    {
        for(int i = 0; i < shopInventory.selledItems.Length; i++)
        {
            if(shopInventory.selledItems[i] != null)
            {
                shopInventory.selledItems[i].gameObject.SetActive(false);
            }
        }
    }

}
