using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public GameObject[] itemList;
    public ItemSlot[] itemSlotList;
    public GameObject[] temporaryCharacterItems;
    public GameObject character;
    public int temporaryHealth;
    public int totalCost;
    public ShopButton precedentWeaponButtonPressed;
    public ShopButton precedentArmorButtonPressed;
    public ShopButton precedentSpecialsButtonPressed;
    public Sprite[] shopButtonSprites;
    public GameManager gameManager;
    public ItemSlot[] selledItems;
    public int money;


    private void Start()
    {
        ShopInventoryRandomizer sir = GetComponent<ShopInventoryRandomizer>();
        sir.RandomizeShopInventory();
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            itemSlotList[i].gameObject.SetActive(true);
            itemSlotList[i].item = itemList[i].GetComponent<Item>();
            itemSlotList[i].Assign(itemList[i]);
        }
    }
}
