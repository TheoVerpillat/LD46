using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterType;
    public string monsterClass;
    public string monsterElement;

    public string DebugMonster()
    {
        return monsterType + " - " + monsterClass + " - " + monsterElement;
    }
}
