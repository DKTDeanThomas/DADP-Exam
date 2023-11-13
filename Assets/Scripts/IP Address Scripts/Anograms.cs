using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anograms : MonoBehaviour
{
    [Header("Dictionaries")]
    [SerializeField] private Dictionary<string, string> anagramEasy = new Dictionary<string, string>();
    [SerializeField] private Dictionary<string, string> anagramMedium = new Dictionary<string, string>();
    [SerializeField] private Dictionary<string, string> anagramHard = new Dictionary<string, string>();

    [Header("Valid Inputs Images")]
    [SerializeField] private Image successImage;
    [SerializeField] private Image successImage1;
    [SerializeField] private Image successImage2;
    [SerializeField] private Image successImage3;
    [SerializeField] private Image successImage4;
    [SerializeField] private Image successImage5;
    [SerializeField] private Image successImage6;
    [SerializeField] private Image unsuccessfulImage;


    [Header("Bools and Floats")]
    [SerializeField] private bool isFieldEmpty = false;
    [SerializeField] private bool isDisplayingImage = false;
    [SerializeField] private float displayTime = 5f;

    [Header("Binary Input Fields")]
    [SerializeField] private InputField binaryInputField;
    [SerializeField] private InputField binaryInputField1;
    [SerializeField] private InputField binaryInputField2;
    [SerializeField] private InputField binaryInputField3;
    [SerializeField] private InputField binaryInputField4;
    [SerializeField] private InputField binaryInputField5;
    [SerializeField] private InputField binaryInputField6;


    public void Start()
    {
        isFieldEmpty = true;//checks if the input fields are all empty first before giving out hints at start of game.
        AssignWordToEasy();
        AssignWordToMedium();
        AssignWordToHard();
    }


    public void AssignWordToEasy()
    {
        anagramEasy.Add("Meat", "Team");
        anagramEasy.Add("Near", "Earn");
    }

    public void AssignWordToMedium()
    {
        anagramMedium.Add("Brite", "Tribe");
        anagramMedium.Add("Glean", "Angel");
    }

    public void AssignWordToHard()
    {
        anagramHard.Add("Centralas", "Ancestral");
        anagramHard.Add("Rip its", "Spirit");
        anagramHard.Add("Canafri", "African");
    }

    public void EasySolutionCheck(string inputLetters0)
    {
        // Check if the value exists in the dictionary
        if (anagramEasy.ContainsValue(inputLetters0))
        {
            Debug.Log("Your code seems to be working because the letters you have inputted are in the dictionary");

            // Iterate over the dictionary to find the corresponding key for the given value
            foreach (var pair in anagramEasy)
            {
                if (pair.Value == inputLetters0)
                {
                    string solutionWord = pair.Key;
                    Debug.Log("solutionWord: " + solutionWord);
                    Debug.Log("Word stored in the dictionary: " + pair.Value);

                    // Rest of your code remains the same
                    if (inputLetters0 == "Team")
                    {
                        Debug.Log("You're correct, this is indeed the anagram word that corresponds with the one you've been given");
                        binaryInputField.text = "01010110 + 01101001 + 01100011 + 01100101";
                        successImage.gameObject.SetActive(true);
                        StartCoroutine(HideImage(successImage));
                    }
                    else if (inputLetters0 == "Earn")
                    {
                        Debug.Log("You're also correct, that's the anagram word that corresponds with the one you've been given!!");
                        binaryInputField1.text = "01010010 + 01101111 + 01111001 + 01100001 + 01101100";
                        successImage1.gameObject.SetActive(true);
                        StartCoroutine(HideImage(successImage1));
                    }

                    return; // Exit the method since you found a match
                }
            }
        }
        else
        {
            Debug.Log("Invalid Input!!!");
            unsuccessfulImage.gameObject.SetActive(true);
            StartCoroutine(HideImage(unsuccessfulImage));
        }
    }

    public void MediumSolutionChecker(string inputLetters1)
    {
        if (anagramMedium.ContainsValue(inputLetters1))
        {
            Debug.Log("your code is working bro");

            foreach (var pair in anagramMedium)
            {
                if (pair.Value == inputLetters1)
                {
                    string solutionWord1 = pair.Key;//  we look for the corresponding key to the given value that we input
                    Debug.Log("solutionWord1: " + solutionWord1);
                    Debug.Log("word stored in the dictionary:" + pair.Value);
                }
            }

            if (inputLetters1 == "Tribe")
            {
                Debug.Log("You're correct, this is indeed the anagram word that corresponds with the one your left that you've been given");
                binaryInputField2.text = "01000110 + 01101111 + 01111000";
                successImage2.gameObject.SetActive(true);
                StartCoroutine(HideImage(successImage2));

            }
            else if (inputLetters1 == "Angel")
            {
                Debug.Log("You're also correct, thats the anagram word that corresponds with the one you've been given!!");
                binaryInputField3.text = "01000100 + 01110010 + 01100101 + 01100001 + 01101101";
                successImage3.gameObject.SetActive(true);
                StartCoroutine(HideImage(successImage3));
            }
            return; //exits the function after match is found.
        }

        else
        {
            Debug.Log("Invalid Input!!!");
            unsuccessfulImage.gameObject.SetActive(true);
            StartCoroutine(HideImage(unsuccessfulImage));
        }

    }

    public void HardSolutionChecker(string inputLetters2)
    {
        if(anagramHard.ContainsValue(inputLetters2))
        {
            Debug.Log("your code is working papii");
           foreach( var pair in anagramHard)
            {
                if( pair.Value == inputLetters2)
                {
                    string solutionWord2 = pair.Key;
                    Debug.Log("solutionWord2:" + solutionWord2);
                    Debug.Log("word stored in the dictionary:" + pair.Value);

                }
            }

            if(inputLetters2 == "Ancestral")
            {
                Debug.Log("you're veryyy correct, indeed that's the anagram word that corresponds with the one you've been given");
                binaryInputField4.text = "01001110 + 01101001 + 01101100 + 01100101";
                successImage4.gameObject.SetActive(true);
                StartCoroutine(HideImage(successImage4));
            }

            else if(inputLetters2 == "Spirit")
            {
                Debug.Log("you're very corrrect, well done, thats the anagram word that correspond with the one you've been given");
                binaryInputField5.text = "01010110 + 01101001 + 01100011 + 01110100 + 01101111 + 01110010 + 01101001 + 01100001";
                successImage5.gameObject.SetActive(true);
                StartCoroutine(HideImage(successImage5));
            }
            else if(inputLetters2 == "African")
            {
                Debug.Log("you're very correct, well done, thats the anagram word that corresponds with the one you've been given");
                binaryInputField6.text = "01000101 + 01100111 + 01110101 + 01110011 + 01101001";
                successImage6.gameObject.SetActive(true);
                StartCoroutine(HideImage(successImage6));
            }

            return; // exits the function when the match has been found;

        }
        else
        {
            Debug.Log("Invalid Input!!!");
            unsuccessfulImage.gameObject.SetActive(true);
            StartCoroutine(HideImage(unsuccessfulImage));
        }
    }
    private IEnumerator HideImage(Image image)
    {
        yield return new WaitForSeconds(displayTime);
        image.gameObject.SetActive(false);
        isDisplayingImage = false;
    }



}

