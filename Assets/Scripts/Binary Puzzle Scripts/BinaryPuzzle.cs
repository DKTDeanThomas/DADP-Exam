using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryPuzzle : MonoBehaviour
{
    [SerializeField] private Dictionary<string, string> binaryEncoding = new Dictionary<string, string>();//when players provide binary number to get specific letter
    [SerializeField] private Dictionary<string, string> binaryDecoding = new Dictionary<string, string>();//when players provide binary number to verify letter already
    [SerializeField] private Dictionary<string, string> binaryDecrypt = new Dictionary<string, string>();// used when players add the binary nu bers that will be stored in here to formulate the word.
    [SerializeField] private Image InvalidInputImage;
    [SerializeField] private Image SuccessfulImage;
    [SerializeField] private Image SuccessfulImage1;
    [SerializeField] private Image SuccessfulImage2;
    [SerializeField] private Image VerificationImageA1;
    [SerializeField] private Image VerificationImageA2;
    [SerializeField] private Image VerificationImageA3;
    [SerializeField] private Image arrowImage;
    [SerializeField] private Text ClueText;
    [SerializeField] private float displayTime = 5;
    [SerializeField] private bool isDisplayingImage = false;
    [SerializeField] private bool isFieldEmpty = false;
    [SerializeField] BinaryLetter binaryLetter; //reference of the script containing methods to call for enabling the correct letters
    [SerializeField] private int aCounter = 0; // To keep track of the order of "a" occurrences bc the letter "a" repaeats itself twice in the surname.
    [SerializeField] private int currentLetterIndex;
    [SerializeField] private InputField binaryInputField;
    [SerializeField] private InputField binaryInputField1;
    [SerializeField] private InputField binaryInputField2;
    [SerializeField] private InputField binaryInputField3;
    [SerializeField] private InputField binaryInputField4;
    [SerializeField] private InputField binaryInputField5;//for the letter i hint input field


    public void Start()
    {
        binaryInputField.text = " + 01001010";
        InvalidInputImage.gameObject.SetActive(false);
        SuccessfulImage.gameObject.SetActive(false);
        isFieldEmpty = true;//checks if the input fields are all empty first before giving out hints at start of game.
        AssignLetterstoBinaryNumber();
        AssignBinaryNumberstoLetters();
        AdditionofBinaryLetters();
        Clues();
        
    }
    public void RequestHint1()
    {
        GenerateHint1();
    }
    public void RequestHint2()
    {
        GenerateHint2();
    }
    public void RequestHint3()
    {
        GenerateHint3();
    }
    public void RequestHint4()
    {
        GenerateHint4();
    }
    public void RequestHint5()
    {
        GenerateHint5();
    }
    public void RequestHint6()
    {
        GenerateHint6();
    }
    public void AssignBinaryNumberstoLetters()
    {
        binaryEncoding.Add("01000001 + 01001010", "A J");// going to ber added with the existing binary number for the letter J.
        binaryEncoding.Add("01100001", "a");
        binaryEncoding.Add("01100001_2", "a");// this has an identifier bc if it doesn't , the dictionary will not be able to recognize it bc it would be the same as the line of  code abo
       
    }

    public void AssignLetterstoBinaryNumber()
    {
        binaryDecoding.Add("01100010", "b");
        binaryDecoding.Add("01110010", "r");
        binaryDecoding.Add("01101001", "i"); //last letter of the name. i.e letter i
        
    }
    public void AdditionofBinaryLetters()
    {
        binaryDecrypt.Add("Jabari + Abara", " Jabari Abara");//name + surname == reveals the location of the parents// fictionally :)

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
           
            if (letter == "A J")
            {
                binaryLetter.EnableLetter1();
                Debug.Log("Letter Capital A has been enabled successfully");
                SuccessfulImage.gameObject.SetActive(true);
                StartCoroutine(HideImage(SuccessfulImage));
            }
            else if (letter == "a")
            {
                aCounter++;// increases the counter for every occurence of "a" since there's 2 small letter a's in the surname to  be formed.

                if (aCounter == 1)// first occurence
                {
                    binaryLetter.EnableLetterA1();
                    Debug.Log("first small letter a has been enabled successfully");
                    SuccessfulImage1.gameObject.SetActive(true);
                    StartCoroutine(HideImage(SuccessfulImage1));
                }
                else if (aCounter == 2) // sendond occurrence 
                {
                    binaryLetter.EnableLetterA2();
                    Debug.Log("second small letter a has been enabled successfully");
                    SuccessfulImage2.gameObject.SetActive(true);
                    StartCoroutine(HideImage(SuccessfulImage2));
                    Debug.Log("the image is showing pooookie");
                }

            }

        }
        else
        {
            Debug.Log("Invalid Input!!!");
            InvalidInputImage.gameObject.SetActive(true);
            StartCoroutine(HideImage(InvalidInputImage));
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

    private IEnumerator HideText(Text text)
    {
        yield return new WaitForSeconds(displayTime);
        text.gameObject.SetActive(false);
        
    }

    public void NumberCheck(string inputBinary2)
    {

        Debug.Log("Entered NumberCheck with input: " + inputBinary2);

        if (binaryDecoding.ContainsKey(inputBinary2))
        {
            Debug.Log("Valid code found in dictionary");
            string binaryNumber = binaryDecoding[inputBinary2];
            Debug.Log("BinaryNumberInputted: " + inputBinary2);

            if (inputBinary2 == "01100010") // verifies the binary number inputted based on the letter provided.
            {
                Debug.Log("Valid binary code for 'b'");
                binaryLetter.EnableSmallB1();
                VerificationImageA1.gameObject.SetActive(true);
                StartCoroutine(HideImage(VerificationImageA1));
                
            }
            else if (inputBinary2 == "01110010") // verifies the binary number inputted based on the letter provided.
            {
                Debug.Log("Valid binary code for 'r'");
                binaryLetter.EnableLetterR();
                VerificationImageA2.gameObject.SetActive(true);
                StartCoroutine(HideImage(VerificationImageA2));
            }
            else if (inputBinary2 == "01101001")
            {
                Debug.Log("Valid code entered for letter i");
                VerificationImageA3.gameObject.SetActive(true);
                StartCoroutine(HideImage(VerificationImageA3));
            }
            else
            {
                Debug.Log("Invalid code entered!!");
                InvalidInputImage.gameObject.SetActive(true);
                StartCoroutine(HideImage(InvalidInputImage));
            }
        }
        else
        {
            Debug.Log("Binary code not found in dictionary");
        }
    }
    public void WordFormulate(string inputAddition)
    {
        if (binaryDecrypt.ContainsKey(inputAddition))
        {
            binaryLetter.EnableLocationImage();
            Debug.Log("binary letters have been combined or added up coreclty to reveal this location image");
        }
    }

    public void GenerateHint1()
    {
        List<string> hintsForA = new List<string> { "0", "1", "0", "0", "0", "0", "0", "1" };

        string currentLetter = currentLetterIndex == 0 ? "A" : "b";

        if (currentLetter == "A" && hintsForA.Count > 0)
        {
            if (isFieldEmpty)
            {
                // Add the next hint from the list to the input field.
                binaryInputField.text += hintsForA[0];
                // Remove the used hint from the list.
                hintsForA.RemoveAt(0);
            }
            else
            {
                Debug.Log("player has used all hints available for this letter");
            }
               
        }

    }
    public void GenerateHint2()
    {
        List<string> hintsForB = new List<string> { "0", "1", "1", "0", "0", "0", "1", "0" };//2nd letter list for all the binary digits that make it up

        string currentLetter = currentLetterIndex == 0 ? "b" : "a";

        if (currentLetter == "b" && hintsForB.Count > 0)
        {
            if (isFieldEmpty)
            {
                binaryInputField1.text += hintsForB[0];

                hintsForB.RemoveAt(0);
            }
            else
            {
                Debug.Log("player has used all hints available for this letter ");
            }

        }

    }
    public void GenerateHint3()
    {
        List<string> hintsFora = new List<string> { "0", "1", "1", "0", "0", "0", "0", "1" };

        string currentLetter = currentLetterIndex == 0 ? "a" : "r";

        if(currentLetter == "a" && hintsFora.Count > 0)
        {
            binaryInputField2.text += hintsFora[2];

            hintsFora.RemoveAt(2);
        }
        else
        {
            Debug.Log("player has exhausted all available hints for this letter");
        }

    }

    public void GenerateHint4()
    {
        List<string> HintsforR = new List<string> { "0", "1", "1", "1", "0", "0", "1", "0" };

        string currentLettter = currentLetterIndex == 0 ? "r" : "a";

        if(currentLettter == "r" && HintsforR.Count > 0)
        {
            binaryInputField3.text += HintsforR[2];

            HintsforR.RemoveAt(2);
        }
        else
        {
            Debug.Log("player has finished all hints available for this letter ");
        }
    }

    public void GenerateHint5()
    {
        List<string> HintsForLastLetter = new List<string> { "0", "1", "1", "0", "0", "0", "0", "1", "_2" };

        string currentletter = currentLetterIndex == 0 ? "a" : "A";

        if(currentletter == "a" && HintsForLastLetter.Count > 0)
        {
            binaryInputField4.text += HintsForLastLetter[8];

            HintsForLastLetter.RemoveAt(8);
        }
        else
        {
            Debug.Log("player has used up all hints available for this letttter");
        }
    }
    public void GenerateHint6()
    {
        List<string> HintsforI = new List<string> { "0", "1", "1", "0", "1", "0", "0", "1" };

        string currentletter = currentLetterIndex == 0 ? "i" : "j";

        if(currentletter == "i" && HintsforI.Count > 0)
        {
            binaryInputField5.text += HintsforI[3];
           binaryInputField5.text += HintsforI[4];

            HintsforI.RemoveAt(3);
           HintsforI.RemoveAt(4);
        }
        else
        {
            Debug.Log("player has used up all hints for the letter i unfortunately");
        }
    }

public void Clues()
    {
        arrowImage.gameObject.SetActive(true);
        ClueText.gameObject.SetActive(true);
        StartCoroutine(HideImage(arrowImage));
        StartCoroutine(HideText(ClueText));
    }
}

