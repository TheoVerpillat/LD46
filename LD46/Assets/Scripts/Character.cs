using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int health = 12;
    public int money = 100;
    public Sprite portrait;
    public GameObject[] itemList;
    public bool isMainCharacter = false;
    public string dialogueLine;
    public bool isAlive = true;
}
