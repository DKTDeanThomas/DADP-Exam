using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BinaryHA : MonoBehaviour
{
    [Header("Input fields")]
    [SerializeField] private InputField inputField;
    [SerializeField] private InputField inputField1;
    [SerializeField] private InputField inputField2;
    [SerializeField] private InputField inputField3;
    [SerializeField] private InputField inputField4;
    [SerializeField] private InputField inputField5;
    [SerializeField] private InputField inputField6;//for the location search field

    [Header("Script Reference")]
    [SerializeField] private BinaryPuzzle binaryPuzzle;

    public void Start()
    {
        binaryPuzzle.GetComponent<BinaryPuzzle>();
       
    }
    public void DisplayCodeInput()// will be attached to the EndEdit  event of the InputField
        {

        string inputText = inputField.text;//code will be entered as a string of binary numbers that will be displayed on screen
        Debug.Log("EnteredCode:" + inputText);
        string inputText1 = inputField1.text;//input field for 2nd letter
        Debug.Log("EnteredCode:" + inputText1);
        string inputText2 = inputField2.text;
        Debug.Log("enteredcode: " + inputText2);//input field for 3rd letter 
        string inputText3 = inputField3.text;
        Debug.Log("enteredcode:" + inputText3);
        string inputText4 = inputField4.text;
        Debug.Log("entered code:" + inputText4);//input field for last letter for the surname
        string inputText5 = inputField5.text;
        Debug.Log("entered code: " + inputText5);//input field for the letter i of the name
        string inputText6 = inputField6.text;
        Debug.Log("entered name & surname:" + inputText6); //input field for the location reveal 


    }
}



