using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SentencesGenerator : MonoBehaviour
{
    public List<string> sentencesStart;
    public List<string> sentencesEnd;
    public List<string> mainCharacterSentences;

    public TMP_Text dialogPanel;

    IEnumerator coroutine;
    int precedentSentence = 0;

    public string GenerateCharacterSentence(string monsterType)
    {
        string fullSentence = "";
        int rnd = Random.Range(0, sentencesStart.Count);
        while(rnd == precedentSentence)
        {
            rnd = Random.Range(0, sentencesStart.Count);
        }
        precedentSentence = rnd;
        fullSentence = sentencesStart[rnd] + monsterType + sentencesEnd[rnd];

        return fullSentence;
    }

    public void SaySentence(string sentence)
    {
        coroutine = DisplaySentence(sentence);
        StartCoroutine(coroutine);
    }

    public IEnumerator DisplaySentence (string sentence)
    {
        string s = "";
        dialogPanel.text = s;
        for (int i = 0; i < sentence.Length; i++)
        {
            float rndPitch = Random.Range(0.9f, 1.1f);
            int rndMod = Random.Range(2, 5);
            if (i%rndMod == 0)
            {
                dialogPanel.GetComponent<AudioSource>().pitch = rndPitch;
                dialogPanel.GetComponent<AudioSource>().Play();
            }
            dialogPanel.text += sentence[i];
            yield return new WaitForSeconds(0.005f);
        }
    }

    public void StopSentence()
    {
        StopCoroutine(coroutine);
    }


}
