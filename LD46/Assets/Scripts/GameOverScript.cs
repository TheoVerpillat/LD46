using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public bool heroDead;
    public bool noMoney;

    [SerializeField] Animator anim;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject heroDeadText;
    [SerializeField] GameObject noMoneyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (heroDead)
        {
            gameOverCanvas.SetActive(true);
            anim.SetTrigger("Appear");
            heroDeadText.SetActive(true);
            heroDead = false;
        } else if (noMoney)
        {
            gameOverCanvas.SetActive(true);
            anim.SetTrigger("Appear");
            noMoneyText.SetActive(true);
            noMoney = false;
        }
    }
}
