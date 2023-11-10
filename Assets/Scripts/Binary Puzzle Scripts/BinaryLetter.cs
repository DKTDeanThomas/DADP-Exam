using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BinaryLetter : MonoBehaviour
{
    [SerializeField] private Image CapitalA;
    [SerializeField] private Image CapitalJ;
    [SerializeField] private Image SmallB1;
    [SerializeField] private Image smallA1;
    [SerializeField] private Image smallA2;
    [SerializeField] private Image smallR;
    [SerializeField] private Image locationImage;
    [SerializeField] private Image Trophy;
    [SerializeField] private bool lettersEnabled = false; //this is a flag that checks if the image to be nabled has been called by the Binary Puzzle scrip
    

    public void EnableLetter1()
    {
        if (lettersEnabled)
        {
            CapitalA.gameObject.SetActive(true);
            CapitalJ.gameObject.SetActive(true);
        }

    }
    public void EnableLetterA1()
    {
        if (lettersEnabled)
        {
            smallA1.gameObject.SetActive(true);
            
        }
       
    }

    public void EnableSmallB1()
    {
        if (lettersEnabled)
        {
            SmallB1.gameObject.SetActive(true);
        }
    }
    public void EnableLetterA2()
    {
        if (lettersEnabled)
        {
            smallA2.gameObject.SetActive(true);
            
        }
    }


    public void EnableLetterR()
    {
        if (lettersEnabled)
        {
            smallR.gameObject.SetActive(true);
        }
    }


    public void EnableLocationImage()
    {
        if (lettersEnabled)
        {
            locationImage.gameObject.SetActive(true);
            Trophy.gameObject.SetActive(true);
        }
    }

}
