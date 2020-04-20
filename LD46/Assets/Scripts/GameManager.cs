using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int numberOfFloor;
    public GenerateDay advListGenerator;
    public GenerateDay monsterListGenerator;
    public int actualFloor;
    public int actualClient;
    public List<GameObject[]> floorList;
    public List<GameObject[]> monsterList;
    public Animator fadeAnim;
    public TMP_Text floorPanel;
    public TMP_Text shopMoneyPanel;
    public TMP_Text forecastPanel;

    public Monster[] monstersInFloor;
    public Monster[] monstersInNextFloor;

    public UI_DisplayStats statsPanel;
    public ShopInventory shopInventory;

    public GameObject[] permanentCharacterInventory;
    public int permanentCharacterHealth;

    public SentencesGenerator sentencesGenerator;
    public TaxesScript taxesManager;
    public GameOverScript gameOverManager;
    public VictoryScript victoryManager;

    public bool canGoToNextFloor = false;
    public int numberOfPenalties = 0;

    public int failState = 0;

    // Start is called before the first frame update
    void Start()
    {
        permanentCharacterHealth = 12;
        permanentCharacterInventory = new GameObject[3];
        RandomizeFirstCharacterInventory();
        monstersInFloor = new Monster[1];
        monstersInNextFloor = new Monster[1];
        actualFloor = 0;
        actualClient = 0;
        floorList = new List<GameObject[]>();
        monsterList = new List<GameObject[]>();

        for (int i = 0; i < numberOfFloor; i++)
        {
            floorList.Add(advListGenerator.RandomizeClients());
            //floorList[i] = advListGenerator.RandomizeClients();
            if (i < 6)
            {
                monsterList.Add(monsterListGenerator.GenerateMonster(1));
                //monsterList[i] = monsterListGenerator.GenerateMonster(1);
            }
            else
            {
                monsterList.Add(monsterListGenerator.GenerateMonster(2));
                //monsterList[i] = monsterListGenerator.GenerateMonster(2);
            }
        }
        shopInventory.GetComponent<ShopInventoryRandomizer>().floor = actualFloor;
        monstersInFloor[0] = monsterList[0][0].GetComponent<Monster>();
        monstersInNextFloor[0] = monsterList[1][0].GetComponent<Monster>();
        floorPanel.text = "FLOOR " + actualFloor;
        updateForecast();
        GameLoop();
        //DebugFloorList();
    }

    void DebugFloorList()
    {
        Debug.Log("FLOOR LIST SIZE:" + floorList.Count);
        Debug.Log("MONSTER LIST SIZE:" + monsterList.Count);
        for (int i = 0; i < floorList.Count; i++)
        {
            for(int j = 0; j < floorList[i].Length; j++)
            {
                Debug.Log(floorList[i][j]);
            }

            for (int j = 0; j < monsterList[i].Length; j++)
            {
                Debug.Log(monsterList[i][j].GetComponent<Monster>().DebugMonster());
            }

        }
    }

    void GameLoop()
    {
        Debug.Log("Actual Floor: " + actualFloor + " - Actual Client: " + actualClient);
        shopMoneyPanel.text = "$" + shopInventory.money;
        Debug.Log("PCH: " + permanentCharacterHealth + " IS MAIN: " + floorList[actualFloor][actualClient].GetComponent<Character>().isMainCharacter);
        if (permanentCharacterHealth <= 0 && floorList[actualFloor][actualClient].GetComponent<Character>().isMainCharacter)
        {
            Debug.Log("Je n'ai pas de vie");
            if (actualClient + 1 < 4)
            {
                Debug.Log("Je passe au client suivant");
                actualClient++;
            }
            else
            {
                Debug.Log("En fait il y a trop de client, je termine la loop");
                NextClient();
                return;
            }
        }

        if (floorList[actualFloor][actualClient].GetComponent<Character>().isMainCharacter)
        {
            for(int i = 0; i < permanentCharacterInventory.Length; i++)
            {
                floorList[actualFloor][actualClient].GetComponent<Character>().itemList[i] = permanentCharacterInventory[i];
            }
            floorList[actualFloor][actualClient].GetComponent<Character>().health = permanentCharacterHealth;
        }
        statsPanel.characterObject = floorList[actualFloor][actualClient];
        statsPanel.Refresh();
        shopInventory.character = floorList[actualFloor][actualClient];

        string actualClientSentence = "";
        if (floorList[actualFloor][actualClient].GetComponent<Character>().isMainCharacter) actualClientSentence = sentencesGenerator.mainCharacterSentences[actualFloor];
        else actualClientSentence = ClientSentence();

        shopInventory.character.GetComponent<Character>().dialogueLine = actualClientSentence;
        sentencesGenerator.SaySentence(actualClientSentence);
    }

    public string ClientSentence()
    {
        List<string> monsterCapacitiesList = new List<string>();
        monsterCapacitiesList.Add(monstersInFloor[0].GetComponent<Monster>().monsterType);
        monsterCapacitiesList.Add(monstersInFloor[0].GetComponent<Monster>().monsterClass);
        monsterCapacitiesList.Add(monstersInFloor[0].GetComponent<Monster>().monsterElement);

        if (actualFloor > 5)
        {
            monsterCapacitiesList.Add(monstersInFloor[1].GetComponent<Monster>().monsterType);
            monsterCapacitiesList.Add(monstersInFloor[1].GetComponent<Monster>().monsterClass);
            monsterCapacitiesList.Add(monstersInFloor[1].GetComponent<Monster>().monsterElement);
        }

        monsterCapacitiesList.Add(monstersInNextFloor[0].GetComponent<Monster>().monsterType);
        monsterCapacitiesList.Add(monstersInNextFloor[0].GetComponent<Monster>().monsterClass);
        monsterCapacitiesList.Add(monstersInNextFloor[0].GetComponent<Monster>().monsterElement);

        int rndMonster = Random.Range(0, monsterCapacitiesList.Count - 1);
        string s = sentencesGenerator.GenerateCharacterSentence(monsterCapacitiesList[rndMonster]);
        return s;
    }

    public void NextClient()
    {
        actualClient++;
        if ( actualClient > 4)
        {
            StartCoroutine(FadeToNextFloor());
        }
        else
        {
            GameLoop();
        }
    }

    void NextFloor()
    {
        actualFloor++;
        if (actualFloor <= 9)
        {
            taxesManager.total = shopInventory.money;
            canGoToNextFloor = false;
            numberOfPenalties = 0;
            actualClient = 0;
            Debug.Log("NEXT FLOOR");
            floorPanel.text = "FLOOR " + actualFloor;
            shopInventory.GetComponent<ShopInventoryRandomizer>().floor = actualFloor;
            shopInventory.GetComponent<ShopInventoryRandomizer>().RandomizeShopInventory();
            shopInventory.Refresh();

            if (actualFloor + 1 > 5 && actualFloor + 1 < 10) monstersInNextFloor = new Monster[2];

            if (actualFloor > 5)
            {
                monstersInFloor = new Monster[2];

                if (actualFloor + 1 < 10) monstersInNextFloor[1] = monsterList[actualFloor + 1][1].GetComponent<Monster>();

                monstersInFloor[1] = monsterList[actualFloor][1].GetComponent<Monster>();
            }
            monstersInFloor[0] = monsterList[actualFloor][0].GetComponent<Monster>();
            if (actualFloor + 1 < 10) monstersInNextFloor[0] = monsterList[actualFloor + 1][0].GetComponent<Monster>();
            updateForecast();

            GameLoop();
        }
        else
        {
            victoryManager.victory = true;
            return;
        }

    }

    IEnumerator FadeToNextFloor()
    {
        LinkTaxes();
        fadeAnim.SetBool("playAnim", true);
        yield return new WaitForSeconds(2f);
        taxesManager.calculateTaxes = true;
        //yield return new WaitForSeconds(2f);
        while (!canGoToNextFloor)
        {
            yield return new WaitForEndOfFrame();
        }
        if (permanentCharacterHealth <= 0)
        {
            gameOverManager.heroDead = true;
            //yield break;
        }
        else
        {
            CalculateCombat();
            fadeAnim.SetBool("playAnim", false);
            if (taxesManager.total <= 0) gameOverManager.noMoney = true;
            NextFloor();
        }
    }


    void CalculateCombat()
    {
        int livesLost = 0;
        List<string> characterCapacityList = new List<string>();
        List<string> monsterCapacitiesList = new List<string>();

        for (int i = 0; i < permanentCharacterInventory.Length; i++)
        {
            characterCapacityList.Add(permanentCharacterInventory[i].GetComponent<Item>().capacity1);
            characterCapacityList.Add(permanentCharacterInventory[i].GetComponent<Item>().capacity2);
        }

        monsterCapacitiesList.Add(monstersInFloor[0].GetComponent<Monster>().monsterType);
        monsterCapacitiesList.Add(monstersInFloor[0].GetComponent<Monster>().monsterClass);
        monsterCapacitiesList.Add(monstersInFloor[0].GetComponent<Monster>().monsterElement);

        if (actualFloor > 5)
        {
            monsterCapacitiesList.Add(monstersInFloor[1].GetComponent<Monster>().monsterType);
            monsterCapacitiesList.Add(monstersInFloor[1].GetComponent<Monster>().monsterClass);
            monsterCapacitiesList.Add(monstersInFloor[1].GetComponent<Monster>().monsterElement);
        }

        for(int i = 0; i < characterCapacityList.Count; i++)
        {
            if (!monsterCapacitiesList.Contains(characterCapacityList[i]))
            {
                livesLost++;
            }
        }

        permanentCharacterHealth -= livesLost;
    }

    void LinkTaxes()
    {
        taxesManager.savings = shopInventory.money;
        taxesManager.penaltiesNbr = numberOfPenalties;
        taxesManager.penaltyValue = -50;
        taxesManager.rent = -200;
    }

    public void updateForecast()
    {
        forecastPanel.text = "FLOOR " + actualFloor + ": "
            + monstersInFloor[0].monsterType + " - "
            + monstersInFloor[0].monsterClass + " - "
            + monstersInFloor[0].monsterElement;
        if(actualFloor > 5)
        {
            floorPanel.text += " || "
            + monstersInFloor[1].monsterType + " - "
            + monstersInFloor[1].monsterClass + " - "
            + monstersInFloor[1].monsterElement;
        }

        if (actualFloor + 1 < 10)
        {
            int infoToBlur = Random.Range(0, 2);
            if (infoToBlur == 0)
            {
                forecastPanel.text += "\nFLOOR " + (int)(actualFloor + 1) + ": "
                + "■■■ - "
                    + monstersInNextFloor[0].monsterClass + " - "
                    + monstersInNextFloor[0].monsterElement;
            }
            else if (infoToBlur == 1)
            {
                forecastPanel.text += "\nFLOOR " + (int)(actualFloor + 1) + ": "
                    + monstersInNextFloor[0].monsterType + " - "
                    + "■■■ - "
                    + monstersInNextFloor[0].monsterElement;
            }
            else if (infoToBlur == 2)
            {
                forecastPanel.text += "\nFLOOR " + (int)(actualFloor + 1) + ": "
                    + monstersInNextFloor[0].monsterType + " - "
                    + monstersInNextFloor[0].monsterClass + " - "
                    + "■■■";
            }

            if (actualFloor + 1 > 5)
            {
                infoToBlur = Random.Range(0, 2);
                //forecastPanel.text += " || ";
                if (infoToBlur == 0)
                {
                    forecastPanel.text += " || "
                        + "■■■ - "
                        + monstersInNextFloor[1].monsterClass + " - "
                        + monstersInNextFloor[1].monsterElement;
                }
                else if (infoToBlur == 1)
                {
                    forecastPanel.text += " || "
                        + monstersInNextFloor[1].monsterType + " - "
                        + "■■■ - "
                        + monstersInNextFloor[1].monsterElement;
                }
                else if (infoToBlur == 2)
                {
                    forecastPanel.text += " || "
                        + monstersInNextFloor[1].monsterType + " - "
                        + monstersInNextFloor[1].monsterClass + " - "
                        + "■■■";
                }
            }
        }


    }

    void RandomizeFirstCharacterInventory()
    {
        int random = (int)Random.Range(0, shopInventory.gameObject.GetComponent<ShopInventoryRandomizer>().weaponList.Length - 1);
        permanentCharacterInventory[0] = shopInventory.gameObject.GetComponent<ShopInventoryRandomizer>().weaponList[random];

        random = (int)Random.Range(0, shopInventory.gameObject.GetComponent<ShopInventoryRandomizer>().armorList.Length - 1);
        permanentCharacterInventory[1] = shopInventory.gameObject.GetComponent<ShopInventoryRandomizer>().armorList[random];

        random = (int)Random.Range(0, shopInventory.gameObject.GetComponent<ShopInventoryRandomizer>().specialsList.Length - 1);
        permanentCharacterInventory[2] = shopInventory.gameObject.GetComponent<ShopInventoryRandomizer>().specialsList[random];
    }


    public void authorizeGoingToNextFloor()
    {
        canGoToNextFloor = true;
    }
}
