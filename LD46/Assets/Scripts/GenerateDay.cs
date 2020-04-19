using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDay : MonoBehaviour
{

    public GameObject[] characterObjects;
    private GameObject tmp;
    public GameObject[] monsterObjects;

    public GameObject[] generatedCharacterObjects;
    public GameObject[] generatedMonsterObjects;

    [SerializeField] string[] raceList = new string[] { "ZOMBIE", "DEMON", "SPECTRE", "MECA" };
    [SerializeField] string[] classList = new string[] { "TANK", "WITCH", "CRAWLER", "PRIEST" };
    [SerializeField] string[] elementList = new string[] { "FIRE", "POISON", "ELECTRIC", "ICE" };



    private void Awake()
    {
        RandomizeClients();
        //DebugList();
    }

    public GameObject[] RandomizeClients()
    {
        generatedCharacterObjects = new GameObject[characterObjects.Length];
        generatedMonsterObjects = new GameObject[monsterObjects.Length];
        for (int i = 0; i < characterObjects.Length; i++)
        {
            int rnd = Random.Range(0, characterObjects.Length-1);
            tmp = characterObjects[rnd];
            characterObjects[rnd] = characterObjects[i];
            characterObjects[i] = tmp;
        }

        for(int i = 0; i < characterObjects.Length; i++)
        {
            generatedCharacterObjects[i] = Instantiate(characterObjects[i]);
            if (!generatedCharacterObjects[i].GetComponent<Character>().isMainCharacter)
            {
                generatedCharacterObjects[i].GetComponent<CharacterItemsRandomizer>().RandomizeItems();
                generatedCharacterObjects[i].GetComponent<CharacterItemsRandomizer>().RandomizeMoney();
                generatedCharacterObjects[i].GetComponent<CharacterItemsRandomizer>().RandomizeHealth();
            }
        }
        return generatedCharacterObjects;
    }


    void DebugList()
    {
        for(int i = 0; i < characterObjects.Length; i++)
        {
            Debug.Log(characterObjects[i]);
        }
    }

    public GameObject[] GenerateMonster(int numbToGenerate)
    {
        for (int i = 0; i < numbToGenerate; i++)
        {
            int rnd = Random.Range(0, 4);
            monsterObjects[i].GetComponent<Monster>().monsterType = raceList[rnd];

            rnd = Random.Range(0, 4);
            monsterObjects[i].GetComponent<Monster>().monsterClass = classList[rnd];

            rnd = Random.Range(0, 4);
            monsterObjects[i].GetComponent<Monster>().monsterElement = elementList[rnd];
        }

        for (int i = 0; i < monsterObjects.Length; i++)
        {
            generatedMonsterObjects[i] = Instantiate(monsterObjects[i]);
        }

        return generatedMonsterObjects;
    }

}
