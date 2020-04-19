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

    // Start is called before the first frame update
    void Start()
    {
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
        statsPanel.characterObject = floorList[actualFloor][actualClient];
        statsPanel.Refresh();
        shopInventory.character = floorList[actualFloor][actualClient];
    }

    public void NextClient()
    {
        actualClient++;
        if( actualClient > 4)
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
        actualClient = 0;
        Debug.Log("NEXT FLOOR");
        actualFloor++;
        floorPanel.text = "FLOOR " + actualFloor;
        shopInventory.GetComponent<ShopInventoryRandomizer>().floor = actualFloor;
        shopInventory.GetComponent<ShopInventoryRandomizer>().RandomizeShopInventory();
        shopInventory.Refresh();

        if (actualFloor + 1 > 5) monstersInNextFloor = new Monster[2];

        if (actualFloor > 5)
        {
            monstersInFloor = new Monster[2];
            monstersInFloor[1] = monsterList[actualFloor][1].GetComponent<Monster>();
            monstersInNextFloor[1] = monsterList[actualFloor + 1][1].GetComponent<Monster>();
        }
        monstersInFloor[0] = monsterList[actualFloor][0].GetComponent<Monster>();
        monstersInNextFloor[0] = monsterList[actualFloor + 1][0].GetComponent<Monster>();
        updateForecast();

        GameLoop();
    }

    IEnumerator FadeToNextFloor()
    {
        fadeAnim.SetBool("playAnim", true);
        yield return new WaitForSeconds(1.5f);
        NextFloor();
        fadeAnim.SetBool("playAnim", false);
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


        int infoToBlur = Random.Range(0, 2);
        if (infoToBlur == 0)
        {
            forecastPanel.text += "\nFLOOR " + (int)(actualFloor + 1) + ": "
            +"■■■ - "
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

        if (actualFloor + 1> 5)
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
