using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BinaryInputField : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private BinaryPuzzle binaryPuzzle;

    public void Start()
    {
        binaryPuzzle.GetComponent<BinaryPuzzle>();
    }
    public void DisplayCodeInput()// will be attached to the EndEdit  event of the InputField
        {
            string inputText = inputField.text;//code will be entered as a string of binary numbers that will be displayed on screen
            Debug.Log("EnteredCode:" + inputText);
       
        }
    }



