using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BinaryLetter : MonoBehaviour
{
    [SerializeField] private Image CapitalA;
    [SerializeField] private Image smallA1;
    [SerializeField] private Image smallA2;


    public void EnableLetter1()
    {
        CapitalA.gameObject.SetActive(true);

    }

    public void EnableLetterA1()
    {
        smallA1.gameObject.SetActive(true);
    }

    public void EnableLetterA2()
    {
        smallA2.gameObject.SetActive(true);
    }

}
