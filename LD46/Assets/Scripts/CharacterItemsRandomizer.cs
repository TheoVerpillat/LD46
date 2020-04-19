using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemsRandomizer : MonoBehaviour
{
    [SerializeField] GameObject[] weaponList;
    [SerializeField] GameObject[] armorList;
    [SerializeField] GameObject[] specialsList;

    public void RandomizeItems()
    {
        int random = (int)Random.Range(0, weaponList.Length - 1);
        GetComponent<Character>().itemList[0] = weaponList[random];

        random = (int)Random.Range(0, armorList.Length - 1);
        GetComponent<Character>().itemList[1] = armorList[random];

        random = (int)Random.Range(0, specialsList.Length - 1);
        GetComponent<Character>().itemList[2] = specialsList[random];
    }

    public void RandomizeHealth()
    {
        int random = (int)Random.Range(1, 12);
        GetComponent<Character>().health = random;
    }

    public void RandomizeMoney()
    {
        int random = (int)Random.Range(100, 900);
        random /= 10;
        random *= 10;
        GetComponent<Character>().money = random;
    }

}
