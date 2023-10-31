using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{   [Header ( "Buttons")]
    [SerializeField] private Button hintButton;
    [SerializeField] private Button hintButton1;
    [SerializeField] private Button hintButton2;
    [SerializeField] private Button hintButton3;
    [SerializeField] private Button hintButton4;

    [Header("HintNumbers")]
    [SerializeField] private float hintNumber = 3;
    [SerializeField] private float hintNumber1 = 2;
    [SerializeField] private float hintNumber2 = 1;
    [SerializeField] private float hintNumber3 = 2;
    [SerializeField] private float hintNumber4 = 1;


    [Header ("Hints Texts")]
    [SerializeField] private Text hintText;
    [SerializeField] private Text hintText1;
    [SerializeField] private Text hintText2;
    [SerializeField] private Text hintText3;
    [SerializeField] private Text hintText4;



    public void Start()
    {
        hintText.text = "Hint: " + hintNumber.ToString();
        FirstHintsUsedUp();
        hintText1.text = "Hint: " + hintNumber1.ToString();
        SecondHintUsedUp();
        hintText2.text = "Hint: " + hintNumber2.ToString();
        ThirdHintUsedUp();
        hintText3.text = "Hint: " + hintNumber3.ToString();
        FourthHintUsedUp();
        hintText4.text = "Hint:" + hintNumber4.ToString();
        LastLetterHintUsedUp();
    }
    public void FirstHintUsed( float hintValue)
    {
        hintValue = 1;
        hintNumber -= hintValue;
        hintText.text = "Hint: " + hintNumber.ToString();
        Debug.Log("One hint has been used");
        FirstHintsUsedUp();
    }

    public void FirstHintsUsedUp()
    {
        if(hintNumber == 0)
        {
            hintButton.gameObject.SetActive(false);
            Debug.Log("All available hints for this specific letter  have been used up");
        }
        else
        {
            hintButton.gameObject.SetActive(true);
        }
    }


    public void SecondHintUsed(float hintVal)
    {
        hintVal = 1;
        hintNumber1 -= hintVal;
        hintText1.text = "Hint: " + hintNumber1.ToString();
        Debug.Log("One hint for the letter b hint has been used");
        SecondHintUsedUp();
    }

    public  void SecondHintUsedUp()
    {
       if(hintNumber1 == 0)
        {
            hintButton1.gameObject.SetActive(false);
            Debug.Log("All hints for this letter have been used up");
        }
        else
        {
            hintButton1.gameObject.SetActive(true);
        }
    }
    
    public void ThirdHintUsed(float hintNum)
    {
        hintNum = 1;
        hintNumber2 -= hintNum;
        hintText2.text = "Hint:" + hintNumber2.ToString();
        Debug.Log(" one hint for the letter a has been used");
        ThirdHintUsedUp();
    }

    public void ThirdHintUsedUp()
    {
        if(hintNumber2 == 0)
        {
            hintButton2.gameObject.SetActive(false);
            Debug.Log("all hints for this letter have been used up ");
        }
    }

    public void FourthHintUsed(float hintValuee)
    {
        hintValuee = 1;
        hintNumber3 -= hintValuee;
        hintText3.text = "Hint :" + hintNumber3.ToString();
        Debug.Log("one hint for the letter r has been used");
        FourthHintUsedUp();
    }

    public void FourthHintUsedUp()
    {
        if(hintNumber3 == 0)
        {
            hintButton3.gameObject.SetActive(false);
            Debug.Log("all hints for this letter have been used up");
        }
    }
    
    public void LastLetterHintUsed(float LastLetterVal)
    {
        LastLetterVal = 1;
        hintNumber4 -= LastLetterVal;
        hintText4.text = "Hint: " + hintNumber4.ToString();
        Debug.Log("one hint for the letter a 2.0 has been used");
        LastLetterHintUsedUp();
    }

    public void LastLetterHintUsedUp()
    {
        if(hintNumber4 == 0)
        {
            hintButton4.gameObject.SetActive(false);
            Debug.Log("all hints for letter have been used up");
        }
    }
}
  