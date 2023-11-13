using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BinaryInputField : MonoBehaviour
{
    [Header("Input fields")]
    [SerializeField] private InputField inputField;
    [SerializeField] private InputField inputField1;
    [SerializeField] private InputField inputField2;
    [SerializeField] private InputField inputField3;
    [SerializeField] private InputField inputField4;
    [SerializeField] private InputField inputField5;
    [SerializeField] private InputField inputField6;
    [SerializeField] private InputField inputField7;
    [SerializeField] private InputField inputField8;
    [SerializeField] private InputField inputField9;
    [SerializeField] private InputField inputField10;
    [SerializeField] private InputField inputField11;
    [SerializeField] private InputField inputField12;
    [SerializeField] private InputField inputField13;
    [SerializeField] private InputField inputField14;
    [SerializeField] private InputField inputField15;
    [SerializeField] private InputField inputField16;
    [SerializeField] private InputField inputField17;
    [SerializeField] private InputField inputField18;
    [SerializeField] private InputField inputField19;
    [SerializeField] private InputField inputField20;
   
    [Header("Script Reference")]
    [SerializeField] private Anograms anagrams;

    public void Start()
    {
        anagrams = GetComponent<Anograms>();
       
    }
    public void DisplayCodeInput()// will be attached to the EndEdit  event of the InputField
        {

        string inputText = inputField.text;//code will be entered as a string of binary numbers that will be displayed on screen
        Debug.Log("EnteredCode:" + inputText);
        string inputText1 = inputField1.text;
        Debug.Log("EnteredCode:" + inputText1);
        string inputText2 = inputField2.text;
        Debug.Log("enteredcode: " + inputText2);
        string inputText3 = inputField3.text;
        Debug.Log("enteredcode:" + inputText3);
        string inputText4 = inputField4.text;
        Debug.Log("entered code:" + inputText4);
        string inputText5 = inputField5.text;
        Debug.Log("entered code: " + inputText5);
        string inputText6 = inputField6.text;
        Debug.Log("entered code: " + inputText6);
        string inputText7 = inputField7.text;
        Debug.Log("EnteredCode:" + inputText7);
        string inputText8 = inputField8.text;
        Debug.Log("EnteredCode:" + inputText8);
        string inputText9 = inputField9.text;
        Debug.Log("enteredcode: " + inputText9);
        string inputText10 = inputField10.text;
        Debug.Log("enteredcode:" + inputText10);
        string inputText11 = inputField11.text;
        Debug.Log("entered code:" + inputText11);
        string inputText12 = inputField12.text;
        Debug.Log("entered code: " + inputText12);
        string inputText13 = inputField13.text;
        Debug.Log("entered code:" + inputText13);
        string inputText14 = inputField14.text;
        Debug.Log("EnteredCode:" + inputText14);
        string inputText15 = inputField15.text;
        Debug.Log("EnteredCode:" + inputText15);
        string inputText16 = inputField16.text;
        Debug.Log("enteredcode: " + inputText16);
        string inputText17 = inputField17.text;
        Debug.Log("enteredcode:" + inputText17);
        string inputText18 = inputField18.text;
        Debug.Log("entered code:" + inputText18);
        string inputText19 = inputField19.text;
        Debug.Log("entered code: " + inputText19);
        string inputText20 = inputField20.text;
        Debug.Log("entered code: " + inputText20);
       


    }
}



