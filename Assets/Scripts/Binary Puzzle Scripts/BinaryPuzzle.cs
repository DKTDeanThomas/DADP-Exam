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
    [SerializeField] private Image VerificationImageA1;
    [SerializeField] private Image VerificationImageA2;
    [SerializeField] private float displayTime = 5;
    [SerializeField] private bool isDisplayingImage = false;
    [SerializeField] BinaryLetter binaryLetter; //reference of the script containing methods to call for enabling the correct letters
    [SerializeField] private int aCounter = 0; // To keep track of the order of "a" occurrences bc the letter "a" repaeats itself twice in the surname.

    public void Start()
    {
        InvalidInputImage.gameObject.SetActive(false);
        SuccessfulImage.gameObject.SetActive(false);
    }
    public void AssignBinaryNumberstoLetters()
    {
        binaryEncoding.Add("01000001", "A");
        binaryEncoding.Add("01100001", "a");
        binaryEncoding.Add("01100001_2", "a");// this has an identifier bc if it doesn't , the dictionary will not be able to recognize it bc it would be the same as the line of  code abo
        //Debug.Log("yessirrr");
    }

    public void AssignLetterstoBinaryNumber()
    {
        binaryDecoding.Add("01100010", "b");
        binaryDecoding.Add("01110010", "r");

    }

    public void Lettercheck(string inputBinary1)
    {

        if (binaryEncoding.ContainsKey(inputBinary1))
        {
            Debug.Log("your code is working");
            string letter = binaryEncoding[inputBinary1];
            Debug.Log("Letter:" + letter);
            Debug.Log("Input: " + inputBinary1);
            Debug.Log("Binary code from dictionary: " + binaryEncoding[inputBinary1]);
            ShowImage(SuccessfulImage);
            if (letter == "A")
            {
                binaryLetter.EnableLetter1();
                Debug.Log("Letter Capital A has been enabled successfully");
            }
            else if (letter == "a")
            {
                aCounter++;// increases the counter for every occurence of "a" since there's 2 small letter a's in the surname to  be formed.

                if (aCounter == 1)// first occurence
                {
                    binaryLetter.EnableLetterA1();
                    Debug.Log("first small letter a has been enabled successfully");
                }
                else if (aCounter == 2) // sendond occurrence 
                {
                    binaryLetter.EnableLetterA2();
                    Debug.Log("second small letter a has been enabled successfully");
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
            return; //goes according to the specific image being shown depending on the players' binary number inputs' validity.
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
       
        Debug.Log("Entered NumberCheck with input: " + inputBinary2);

        if (binaryDecoding.ContainsValue(inputBinary2))
        {
            inputBinary2 = inputBinary2.Trim();
            Debug.Log("Valid code found in dictionary");

            string binaryNumber = binaryDecoding[inputBinary2];
            Debug.Log("Letter: " + binaryNumber);

            if (binaryNumber == "01100010") // verifies the binary number inputted based on the letter provided.
            {
                Debug.Log("Valid binary code for 'b'");
                ShowImage(VerificationImageA1);
            }
            else if (binaryNumber == "01110010") // verifies the binary number inputted based on the letter provided.
            {
                Debug.Log("Valid binary code for 'r'");
                ShowImage(VerificationImageA2);
            }
            else
            {
                Debug.Log("Invalid code entered!!");
                ShowImage(InvalidInputImage);
            }
        }
        else
        {
            Debug.Log("Binary code not found in dictionary");
        }
    }




    public void WordFormulate()
    {
        string targetWord = "Abara"; // word to formulated after letters have been retrieved
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

        // Convert binaryResult to letters using 8-bit chunks bc 1 byte = 8 binary digits.
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

        Debug.Log("Resulting word: " + finalWord);// this should be the same if the player has the inputted the correct binary codes i.e so Abara.
        binaryLetter.EnableFullWord();
    }

}
