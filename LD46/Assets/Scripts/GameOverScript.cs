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
    [SerializeField] Text heroDeadText;
    [SerializeField] Text noMoneyText;

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
            heroDeadText.gameObject.SetActive(true);
            heroDead = false;
        } else if (noMoney)
        {
            gameOverCanvas.SetActive(true);
            anim.SetTrigger("Appear");
            noMoneyText.gameObject.SetActive(true);
            noMoney = false;
        }
    }
}
