using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField] public List<int> CorrectOrder = new List<int>();
    [SerializeField] public List<int> FlippedOrder = new List<int>();
    [SerializeField] public List<GameObject> Switches = new List<GameObject>();

    public Animator doorAnim;
    public GameObject MetalDoor;

    private void Start()
    {
        ResetFlips();
    }

    public void AddLever(int val)
    {
        FlippedOrder.Add(val);
        ListCompare();
    }

    private void ListCompare()
    {
        int minCount = Mathf.Min(CorrectOrder.Count, FlippedOrder.Count);

        bool isCorrect = true;

        for (int x = 0; x < minCount; x++)
        {
            if (CorrectOrder[x] != FlippedOrder[x])
            {
                isCorrect = false;
                ResetFlips();
                Debug.Log("failed");
                break;
            }
        }

        if (isCorrect && CorrectOrder.Count == FlippedOrder.Count)
        {
            doorAnim.SetBool("isOpen", true);
            MetalDoor.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void ResetFlips()
    {
        FlippedOrder.Clear();

        foreach (GameObject switchObj in Switches)
        {
            Animator switchAnimator = switchObj.GetComponent<Animator>();
            if (switchAnimator != null)
            {
                Debug.Log(switchObj.name);
                // Assuming you have a boolean parameter named "isActive" in your animator
                switchAnimator.SetBool("isActive", false);
            }
        }
    }
}
