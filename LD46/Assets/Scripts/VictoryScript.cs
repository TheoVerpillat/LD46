using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    public bool victory;

    [SerializeField] GameObject victoryCanvas;
    [SerializeField] Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (victory)
        {
            victoryCanvas.SetActive(true);
            anim.SetTrigger("Appear");

            victory = false;
        }
    }
}
