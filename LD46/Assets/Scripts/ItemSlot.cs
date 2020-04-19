using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    public TMP_Text itemStatsPanel;
    public TMP_Text itemCostPanel;
    public ShopButton linkedItemPanel;

    public void Assign(GameObject itemObject)
    {
        itemStatsPanel.text = item.serial + "\n" + item.capacity1 + " - " + item.capacity2;
        itemCostPanel.text = "$" + item.cost;
        linkedItemPanel.LinkedItem = itemObject;
        transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
    }
}
