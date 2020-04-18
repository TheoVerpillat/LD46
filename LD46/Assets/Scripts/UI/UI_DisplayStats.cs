using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_DisplayStats : MonoBehaviour
{

    public GameObject characterObject;
    public Sprite[] healthSprites;

    //public GameObject[] itemObjects;

    [SerializeField] private UI_PercentageOfScreen portraitPanel;
    [SerializeField] private UI_PercentageOfScreen[] healthPanels;


    [SerializeField] private UI_PercentageOfScreen[] itemsSpritePanel;
    [SerializeField] private TMP_Text[] itemsTexts;

    // Start is called before the first frame update
    void Start()
    {
        //Display Character
        Character character = characterObject.GetComponent<Character>();
        GameObject[] itemObjects = character.itemList;
        Debug.Log(character.health);
        portraitPanel.GetComponent<Image>().sprite = character.portrait;

        float healthValue = (float)character.health/2;
        int healthInt = (int)healthValue;
        Debug.Log(healthValue + " | " + healthInt);

        for(int i = 0; i < healthInt; i++)
        {
            healthPanels[i].GetComponent<Image>().sprite = healthSprites[0];
            if ((float)healthInt != healthValue && i == healthInt -1)
            {
                healthPanels[healthInt].GetComponent<Image>().sprite = healthSprites[1];
            }
        }

        //Display Items
        for(int i = 0; i < itemsSpritePanel.Length; i++)
        {
            Item item = itemObjects[i].GetComponent<Item>();
            itemsSpritePanel[i].GetComponent<Image>().sprite = item.icon;
            itemsTexts[i].text = item.serial + "\n" + item.capacity1 + " - " + item.capacity2;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
