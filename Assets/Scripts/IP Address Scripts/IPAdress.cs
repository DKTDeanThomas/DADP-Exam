using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IPAdress : MonoBehaviour
{
    [Header("Dictionaires")]
    [SerializeField] private Dictionary<string, string> firstIPAddress = new Dictionary<string, string>();
    [SerializeField] private Dictionary<string, string> secondIPAddress = new Dictionary<string, string>();
    [SerializeField] private Dictionary<string, string> thirdIPAddress = new Dictionary<string, string>();
    [SerializeField] private Dictionary<string, string> fourthIPAddress = new Dictionary<string, string>();
   [SerializeField] private Dictionary<string, string> fifthIPAddress = new Dictionary<string, string>();
    [SerializeField] private Dictionary<string, string> sixthIPAddress = new Dictionary<string, string>();
    [SerializeField] private Dictionary<string, string> seventhIPAddress = new Dictionary<string, string>();

    [Header("Market Place Locations")]
    [SerializeField] private Image location0;
    [SerializeField] private Image location1;
    [SerializeField] private Image location2;
    [SerializeField] private Image location3;
    [SerializeField] private Image location4;
    [SerializeField] private Image location5;
    [SerializeField] private Image location6;
    [SerializeField] private Image wrongAnswer;
    [SerializeField] private AudioSource wrongBuzzerSound;
    [SerializeField] private float displayTime = 5f;
    [SerializeField] private bool isDisplayingImage = false;



    public void Start()
    {
        FirstIPAddress();
        SecondIPAddress();
        ThirdIPAddress();
        FourthIPAddress();
        FifthIPAddress();
        SixthIPAddress();
        SeventhIPAddress();
    }

    public void FirstIPAddress()
    {
        firstIPAddress.Add("V + i + c + e", "01010110 + 01101001 + 01100011 + 01100101");
      //Debug.Log("The first market place and its location have been uncovered");
    }
    public void SecondIPAddress()
    {
        secondIPAddress.Add("R + o + y + a + l", "01010010 + 01101111 + 01111001 + 01100001 + 01101100");
      //Debug.Log("The second market place and its location have been uncovered");
    }

    public void ThirdIPAddress()
    {
        thirdIPAddress.Add("F + o + x", "01000110 + 01101111 + 01111000");
      //Debug.Log("The third market place and its location have been uncovered");
    }

    public void FourthIPAddress()
    {
        fourthIPAddress.Add("D + r + e + a + m", "01000100 + 01110010 + 01100101 + 01100001 + 01101101");
      //Debug.Log("The fourth market place and its location have been uncovered");
    }

    public void FifthIPAddress()
    {
        fifthIPAddress.Add("N + i + l + e", "01001110 + 01101001 + 01101100 + 01100101");
      //Debug.Log("The fifth market place and its location have been uncovered");
    }

    public void SixthIPAddress()
    {
        sixthIPAddress.Add("V + i + c + t + o + r + i + a", "01010110 + 01101001 + 01100011 + 01110100 + 01101111 + 01110010" +
            " + 01101001 + 01100001");
      //Debug.Log("The sixth market place and its location have been uncovered");
    }

    public void SeventhIPAddress()
    {
        seventhIPAddress.Add("E + g + u + s + i", "01000101 + 01100111 + 01110101 + 01110011 + 01101001");
       //ebug.Log("The seventh market place and its location have been uncovered");
    }
    private IEnumerator HideImage(Image image)
    {
        yield return new WaitForSeconds(displayTime);
        image.gameObject.SetActive(false);
        isDisplayingImage = false;
    }


    public void FirstIPAddressSolutionCheck(string inputtedWord)
    {
        if (firstIPAddress.ContainsKey(inputtedWord))
        {
            Debug.Log("your code is working papa");
            string Word = firstIPAddress[inputtedWord];
            Debug.Log("word:" + Word);
            Debug.Log("Word stored in the dictionary:" + firstIPAddress[inputtedWord]);

            if(inputtedWord == "V + i + c + e")
            {
                Debug.Log("you're inputted word is very correct, here's the location of this market place");
                location0.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Invalid inputted word, try again");
            wrongAnswer.gameObject.SetActive(true);
            StartCoroutine(HideImage(wrongAnswer));
            wrongBuzzerSound.Play();
        }
    }

    public void SecondIPAddressSolutionCheck(string inputtedWord1)
    {
        if(secondIPAddress.ContainsKey(inputtedWord1))
        {
            Debug.Log("your code is working papa");
            string Word1 = secondIPAddress[inputtedWord1];
            Debug.Log("word:" + Word1);
            Debug.Log("Word stored in the dictionary:" + secondIPAddress[inputtedWord1]);

            if(inputtedWord1 == "R + o + y + a + l")
            {
                Debug.Log("you're inputted word is very correct, here's the location of this market place");
                location1.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Invalid inputted word, try again");
            wrongAnswer.gameObject.SetActive(true);
            StartCoroutine(HideImage(wrongAnswer));
            wrongBuzzerSound.Play();
        }
    }

    public void ThirdIPAddressSolutionCheck(string inputtedWord2)
    {
        if(thirdIPAddress.ContainsKey(inputtedWord2))
        {
            Debug.Log("your codeee is runnningggggg");
            string Word2 = thirdIPAddress[inputtedWord2];
            Debug.Log("word:" + Word2);
            Debug.Log("Word stored in the dictionary:" + thirdIPAddress[inputtedWord2]);

            if(inputtedWord2 == "F + o + x")
            {
                Debug.Log("you're inputted word is very correct, here's the location of this market place");
                location2.gameObject.SetActive(true);
            }

        }
        else
        {
            Debug.Log("Invalid inputted word, try again");
            wrongAnswer.gameObject.SetActive(true);
            StartCoroutine(HideImage(wrongAnswer));
            wrongBuzzerSound.Play();
        }
    }
    
    public void FourthIPAddressSolutionCheck(string inputtedWord3)
    {
        if (fourthIPAddress.ContainsKey(inputtedWord3))
        {
            Debug.Log("your code is running boyy");
            string Word3 = fourthIPAddress[inputtedWord3];
            Debug.Log("word:" + Word3);
            Debug.Log("Word stored in the dictionary:" + fourthIPAddress[inputtedWord3]);

            if(inputtedWord3 == "D + r + e + a + m")
            {
                Debug.Log("you're inputted word is very correct, here's the location of this market place");
                location3.gameObject.SetActive(true);
            }

        }
        else
        {
            Debug.Log("Invalid inputted word, try again");
            wrongAnswer.gameObject.SetActive(true);
            StartCoroutine(HideImage(wrongAnswer));
            wrongBuzzerSound.Play();
        }

    }

    public void FifthIPAddressSolutionCheck(string inputtedWord4)
    {
        if(fifthIPAddress.ContainsKey(inputtedWord4))
        {
            Debug.Log("your code is running bro");
            string Word4 = fifthIPAddress[inputtedWord4];
            Debug.Log("word:" + Word4);
            Debug.Log("Word stored in the dictionary:" + fifthIPAddress[inputtedWord4]);

            if(inputtedWord4 == "N + i + l + e")
            {
                Debug.Log("you're inputted word is very correct, here's the location of this market place");
                location4.gameObject.SetActive(true);
            }

        }
        else
        {
            Debug.Log("Invalid inputted word, try again");
            wrongAnswer.gameObject.SetActive(true);
            StartCoroutine(HideImage(wrongAnswer));
            wrongBuzzerSound.Play();
        }

    }

    public void SixthIPAddressSolutionCheck(string inputtedWord5)
    {
        if(sixthIPAddress.ContainsKey(inputtedWord5))
        {
            Debug.Log("its workinggg broo");
            string Word5 = sixthIPAddress[inputtedWord5];
            Debug.Log("word:" + Word5);
            Debug.Log("Word stored in the dictionary:" + sixthIPAddress[inputtedWord5]);

            if(inputtedWord5 == "V + i + c + t + o + r + i + a")
            {
                Debug.Log("you're inputted word is very correct, here's the location of this market place");
                location5.gameObject.SetActive(true);
            }

        }
        else
        {
            Debug.Log("Invalid inputted word, try again");
            wrongAnswer.gameObject.SetActive(true);
            StartCoroutine(HideImage(wrongAnswer));
            wrongBuzzerSound.Play();
        }
    }

    public void SeventhIPAddressSolutionCheck(string inputtedWord6)
    {
        if(seventhIPAddress.ContainsKey(inputtedWord6))
        {
            Debug.Log("it is running fr ");
            string Word6 = seventhIPAddress[inputtedWord6];
            Debug.Log("word:" + Word6);
            Debug.Log("Word stored in the dictionary:" + seventhIPAddress[inputtedWord6]);

            if(inputtedWord6 == "E + g + u + s + i")
            {
                Debug.Log("you're inputted word is very correct, here's the location of this market place");
                location6.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Invalid inputted word, try again");
            wrongAnswer.gameObject.SetActive(true);
            StartCoroutine(HideImage(wrongAnswer));
            wrongBuzzerSound.Play();
        }
    }
}


