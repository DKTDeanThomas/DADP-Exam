using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BinaryLetter : MonoBehaviour
{
    [SerializeField] private Image CapitalA;
    [SerializeField] private Image smallA1;
    [SerializeField] private Image smallA2;
    [SerializeField] private Image fullWord;
    [SerializeField] private Image Trophy;
    [SerializeField] private bool lettersEnabled = false; //this is a flag that checks if the image to be nabled has been called by the Binary Puzzle scrip


    public void EnableLetter1()
    {
        if (lettersEnabled)
        {
            CapitalA.gameObject.SetActive(true);
        }

    }
    public void EnableLetterA1()
    {
        if (lettersEnabled)
        {
            smallA1.gameObject.SetActive(true);
        }
       
    }
    public void EnableLetterA2()
    {
        if (lettersEnabled)
        {
            smallA2.gameObject.SetActive(true);
        }
    }


    public void EnableFullWord()
    {
        if (lettersEnabled)
        {
            fullWord.gameObject.SetActive(true);
            Trophy.gameObject.SetActive(true);
        }
    }

}
