using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string npcLine;
    public string[] playerResponses;
    public int[] nextDialogueIndices;
}

public class NPCInteractable : MonoBehaviour
{
    public DialogueNode[] dialogue;

    [SerializeField] private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
            gameObject.GetComponent<Outline>().enabled = true;
            GameObject.FindWithTag("Player").GetComponent<Interactor>().talkUI.SetActive(true);

            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E Pressed");
                

                StartDialogue();

            }

        }

        if (!playerInRange)
        {
            gameObject.GetComponent<Outline>().enabled = false;
           
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            GameObject.FindWithTag("Player").GetComponent<Interactor>().talkUI.SetActive(false);
        }
    }

    public void StartDialogue()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        DialogueManager.Instance.playerCamera.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<Interactor>().talkUI.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<Interactor>().minicrosshairUI.SetActive(false);


        //playerInRange = false;

        DialogueManager.Instance.StartDialogue(this);

    }
}
