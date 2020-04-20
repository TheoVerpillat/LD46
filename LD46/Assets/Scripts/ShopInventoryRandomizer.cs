using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventoryRandomizer : MonoBehaviour
{
    public GameObject[] weaponList;
    public GameObject[] armorList;
    public GameObject[] specialsList;
    public GameObject[] potionList;
    public int floor;

    public ShopInventory shopInventory;

public void RandomizeShopInventory()
    {
        int random = 0;
        if (floor < 3)
        {
            // ------ WEAPON LOW TIER------ //
            random = (int)Random.Range(0, 7);
            shopInventory.itemList[0] = weaponList[random];

            random = (int)Random.Range(0, 7);
            shopInventory.itemList[4] = weaponList[random];


            // ------ ARMOR LOW TIER------ //
            random = (int)Random.Range(0, 7);
            shopInventory.itemList[1] = armorList[random];

            random = (int)Random.Range(0, 7);
            shopInventory.itemList[5] = armorList[random];


            // ------ SPECIALS LOW TIER------ //
            random = (int)Random.Range(0, 7);
            shopInventory.itemList[2] = specialsList[random];

            random = (int)Random.Range(0, 7);
            shopInventory.itemList[6] = specialsList[random];

            // ------ POTION LOW TIER------ //
            random = (int)Random.Range(0, 1);
            shopInventory.itemList[8] = potionList[random];

        }
        else if (floor == 3)
        {
            // ------ WEAPON ------ //
            random = (int)Random.Range(8, weaponList.Length-1);
            shopInventory.itemList[0] = weaponList[random];

            random = (int)Random.Range(0, 7);
            shopInventory.itemList[4] = weaponList[random];


            // ------ ARMOR ------ //
            random = (int)Random.Range(8, armorList.Length-1);
            shopInventory.itemList[1] = armorList[random];

            random = (int)Random.Range(0, 7);
            shopInventory.itemList[5] = armorList[random];


            // ------ SPECIALS LOW TIER------ //
            random = (int)Random.Range(0, 7);
            shopInventory.itemList[2] = specialsList[random];

            random = (int)Random.Range(0, 7);
            shopInventory.itemList[6] = specialsList[random];


            // ------ POTION LOW TIER------ //
            random = (int)Random.Range(0, 1);
            shopInventory.itemList[8] = potionList[random];
        } 
        else if (floor == 4)
        {
            // ------ WEAPON ------ //
            random = (int)Random.Range(8, weaponList.Length-1);
            shopInventory.itemList[0] = weaponList[random];

            random = (int)Random.Range(8, weaponList.Length - 1);
            shopInventory.itemList[4] = weaponList[random];


            // ------ ARMOR ------ //
            random = (int)Random.Range(8, armorList.Length-1);
            shopInventory.itemList[1] = armorList[random];

            random = (int)Random.Range(8, armorList.Length - 1);
            shopInventory.itemList[5] = armorList[random];


            // ------ SPECIALS ------ //
            random = (int)Random.Range(0, specialsList.Length-1);
            shopInventory.itemList[2] = specialsList[random];

            random = (int)Random.Range(0, 7);
            shopInventory.itemList[6] = specialsList[random];


            // ------ POTION LOW TIER------ //
            random = (int)Random.Range(0, 1);
            shopInventory.itemList[8] = potionList[random];
        }
        else if (floor > 4 && floor < 9)
        {
            // ------ WEAPON ------ //
            random = (int)Random.Range(8, weaponList.Length - 1);
            shopInventory.itemList[0] = weaponList[random];

            random = (int)Random.Range(8, weaponList.Length - 1);
            shopInventory.itemList[4] = weaponList[random];


            // ------ ARMOR ------ //
            random = (int)Random.Range(8, armorList.Length - 1);
            shopInventory.itemList[1] = armorList[random];

            random = (int)Random.Range(8, armorList.Length - 1);
            shopInventory.itemList[5] = armorList[random];


            // ------ SPECIALS ------ //
            random = (int)Random.Range(8, specialsList.Length - 1);
            shopInventory.itemList[2] = specialsList[random];

            random = (int)Random.Range(8, specialsList.Length - 1);
            shopInventory.itemList[6] = specialsList[random];


            // ------ POTION TIER------ //
            random = (int)Random.Range(0, potionList.Length - 1);
            shopInventory.itemList[8] = potionList[random];
        }
        else
        {
            // ------ WEAPON ------ //
            random = (int)Random.Range(8, weaponList.Length - 1);
            shopInventory.itemList[0] = weaponList[random];

            random = (int)Random.Range(8, weaponList.Length - 1);
            shopInventory.itemList[4] = weaponList[random];


            // ------ ARMOR ------ //
            random = (int)Random.Range(8, armorList.Length - 1);
            shopInventory.itemList[1] = armorList[random];

            random = (int)Random.Range(8, armorList.Length - 1);
            shopInventory.itemList[5] = armorList[random];


            // ------ SPECIALS ------ //
            random = (int)Random.Range(8, specialsList.Length - 1);
            shopInventory.itemList[2] = specialsList[random];

            random = (int)Random.Range(8, specialsList.Length - 1);
            shopInventory.itemList[6] = specialsList[random];

            random = (int)Random.Range(2, potionList.Length - 1);
            shopInventory.itemList[8] = potionList[random];
        }

        random = (int)Random.Range(0, weaponList.Length -1);
        shopInventory.itemList[7] = weaponList[random];

        random = (int)Random.Range(0, armorList.Length - 1);
        shopInventory.itemList[3] = armorList[random];

        shopInventory.Refresh();
    }
}
