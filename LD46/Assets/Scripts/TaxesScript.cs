using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaxesScript : MonoBehaviour
{
    public int savings;
    public int rent;
    public int penaltyValue;
    public int penaltiesNbr;
    public int total;

    public bool calculateTaxes;

    [SerializeField] GameObject TaxCanvas;

    [SerializeField] Text savingsText;
    [SerializeField] Text rentText;
    [SerializeField] Text penaltyValueText;
    [SerializeField] Text penaltiesNbrText;
    [SerializeField] Text totalText;

    [SerializeField] Animator anim;

    [SerializeField] AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (calculateTaxes)
        {
            total = savings + rent + (penaltyValue * penaltiesNbr);

            savingsText.text = savings.ToString();
            rentText.text = rent.ToString();
            penaltiesNbrText.text = penaltyValue.ToString();
            penaltiesNbrText.text = penaltiesNbr.ToString();
            totalText.text = total.ToString();

            TaxCanvas.SetActive(true);
            anim.SetTrigger("Appear");
            calculateTaxes = false;
        }
    }

    public void NextFloor()
    {
        buttonSound.Play();
        TaxCanvas.SetActive(false);
        Debug.Log("Next Floor Called on TaxesManager Script");
    }
}
