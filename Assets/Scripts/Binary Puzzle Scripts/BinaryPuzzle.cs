using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryPuzzle : MonoBehaviour
{
    [SerializeField] private Dictionary<string, string> binaryEncoding = new Dictionary<string, string>();//when players provide binary number to get specific letter
    [SerializeField] private Dictionary<string, string> binaryDecoding = new Dictionary<string, string>();//when players provide binary number to verify letter already 
    [SerializeField] private Image InvalidInputImage;
    [SerializeField] private Image SuccessfulImage;
    [SerializeField] private float displayTime = 5;
    [SerializeField] private bool isDisplayingImage = false;
    [SerializeField] BinaryLetter binaryLetter;
    private int aCounter = 0; // To keep track of the order of "a" occurrences

    public void Start()
    {
        InvalidInputImage.gameObject.SetActive(false);
        SuccessfulImage.gameObject.SetActive(false);
    }
    public void AssignBinaryNumberstoLetters()
    {
        binaryEncoding.Add("01000001", "A");
        binaryEncoding.Add("01100001", "a");
        binaryDecoding.Add("01100001", "a");

    }

    public void AssignLetterstoBinaryNumber()
    {
        binaryEncoding.Add("01100010", "b");
        binaryDecoding.Add("01110010", "r");

    }

    public void Lettercheck(string inputBinary1)
    {

        if (binaryEncoding.ContainsKey(inputBinary1))
        {
            Debug.Log("your code is working");
            string letter = binaryEncoding[inputBinary1];
            Debug.Log("Letter:" + letter);
            ShowImage(SuccessfulImage);
            // Call the BinaryLetter method to enable the corresponding letter image
            if (letter == "A")
            {
                binaryLetter.EnableLetter1();
            }
            else if (letter == "a")
            {
                aCounter++;// increases the counter for every occurence of "a"

                if (aCounter == 1)
                {
                    binaryLetter.EnableLetterA1();
                }
                else if(aCounter == 2)
                {
                    binaryLetter.EnableLetterA2();
                }
            }
            
        }
        else
        {
            Debug.Log("Invalid Input!!!");
            ShowImage(InvalidInputImage);
        }
   
    }
    private void ShowImage(Image image)
    {
        if (isDisplayingImage)
        {
            return; //goes according to the specific image being shown depending on the players' binary number inputss validity.
        }

        isDisplayingImage = true;
        image.gameObject.SetActive(true);
        StartCoroutine(HideImage(image));
    }

    private IEnumerator HideImage(Image image)
    {
        yield return new WaitForSeconds(displayTime);
        image.gameObject.SetActive(false);
        isDisplayingImage = false;
    }



    public void NumberCheck(string inputBinary2)
    {
        if (binaryDecoding.ContainsValue(inputBinary2))
        {
            Debug.Log("your code is working");
            string binaryNumber = binaryDecoding[inputBinary2];
            Debug.Log("Letter:" + binaryNumber);
        }
        else
        {
            Debug.Log("Invalid code entered");
        }
    }

    public void WordFormulate()
    {
        string targetWord = "Abara"; // word to formuylated after letters have been retrieved
        string binaryResult = ""; // Initialize an empty binary numbers stored as a string when inputted by player

        foreach (char letter in targetWord)
        {
            if (binaryDecoding.TryGetValue(letter.ToString(), out string binaryCode))
            {
                binaryResult += binaryCode;
            }
            else
            {
                Debug.Log("Invalid letter found in the target word.");
                return; 
            }
        }

        string finalWord = ""; //initialization of  the result word

        // Convert binaryResult to letters using 8-bit chunks
        for (int i = 0; i < binaryResult.Length; i += 8)
        {
            string chunk = binaryResult.Substring(i, 8);
            if (binaryEncoding.TryGetValue(chunk, out string letter))
            {
                finalWord += letter;//add the letters to the final word
            }
            else
            {
                Debug.Log("Invalid binary chunk found in the binary representation.");
                return; 
            }
        }

        Debug.Log("Resulting word: " + finalWord);// this should be the same if the player has the correct codes put in
    }
}
