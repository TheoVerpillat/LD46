using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType {WEAPON, ARMOR, SPECIALS, POTION }
    //public enum Cap1 {X = 0, ZOMBIE = 1, DEMON = 2, SPECTRE = 3, MECA = 4, TANK = 5, WITCH = 6, CRAWLER = 7, PRIEST = 8, FIRE = 9, POISON = 10, ELECTRIC = 11, ICE = 12}
    //public enum Cap2 { X = 0, ZOMBIE = 1, DEMON = 2, SPECTRE = 3, MECA = 4, TANK = 5, WITCH = 6, CRAWLER = 7, PRIEST = 8, FIRE = 9, POISON = 10, ELECTRIC = 11, ICE = 12 }
    //public enum PotionEfficacity { X = 0, ONE = 1, TWO = 2, FOUR = 4, SIX =6}
    public string serial;
    public int cost;
    public ItemType itemType;
    public string capacity1;
    public string capacity2;
    public int potionEfficacity;
    public Sprite icon;
}
