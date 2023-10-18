using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] private Button hintButton;
    [SerializeField] private float hintNumbers = 6;
    [SerializeField] private bool isHintused;
    [SerializeField] private Text hintText;
    public void HintUsed( float hintValue)
    {
        hintValue = 1;
        if(isHintused && hintButton == true)
        {
            hintNumbers -= hintValue;
            hintText.text = "Hint: " + hintNumbers.ToString();
            Debug.Log("One hint has been used");
        }
        else
        {
            Debug.Log("No hint has been used as of yettt");
        }
    }

    public void HintsUsedUp()
    {
        if(hintNumbers == 0)
        {
            hintButton.gameObject.SetActive(false);
            Debug.Log("All available hints have been used up");
        }
        else
        {
            hintButton.gameObject.SetActive(true);
        }
    }
}
