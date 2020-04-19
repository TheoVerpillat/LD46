using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    [SerializeField] GameObject[] itemList;
    [SerializeField] ItemSlot[] itemSlotList;
    public GameObject[] temporaryCharacterItems;
    public GameObject character;
    public int temporaryHealth;
    public int totalCost;
    public ShopButton precedentWeaponButtonPressed;
    public ShopButton precedentArmorButtonPressed;
    public ShopButton precedentSpecialsButtonPressed;
    public Sprite[] shopButtonSprites;


    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            itemSlotList[i].item = itemList[i].GetComponent<Item>();
            itemSlotList[i].Assign(itemList[i]);
        }
    }
}
