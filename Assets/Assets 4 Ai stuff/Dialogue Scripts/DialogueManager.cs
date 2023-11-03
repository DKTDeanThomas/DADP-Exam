using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public TMP_Text npcText;
    public GameObject[] playerButtons;
    public TMP_Text[] playerResponses;

    private NPCInteractable currentNPC;
    private int currentDialogueIndex = 0;

    private bool isNPCTextCompleted = false;

    public float typingSpeed = 0.05f; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(NPCInteractable npc)
    {
        currentNPC = npc;
        currentDialogueIndex = 0;
        DisplayDialogue();
    }

    public void DisplayDialogue()
    {
    dialoguePanel.SetActive(true);
    npcText.gameObject.SetActive(true);
    npcText.text = currentNPC.dialogue[currentDialogueIndex].npcLine;

    StartCoroutine(TypeSentence(currentNPC.dialogue[currentDialogueIndex].npcLine));

    if (currentNPC.dialogue[currentDialogueIndex].playerResponses.Length == 0)
    {
        StartCoroutine(EndDialogueAfterDelay());
    }
    else
    {
        foreach (var button in playerButtons)
        {
            button.SetActive(false);
        }
    }
    }

    public void PlayerResponse(int index)
    {
        currentDialogueIndex = currentNPC.dialogue[currentDialogueIndex].nextDialogueIndices[index];
        if (currentDialogueIndex != -1)
        {
            DisplayDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
    isNPCTextCompleted = false;
    dialoguePanel.SetActive(false);
    currentNPC = null;
    currentDialogueIndex = 0;
    }

    private void Update()
    {
    if (Input.GetKeyDown(KeyCode.Space) && dialoguePanel.activeInHierarchy)
    {
        if (!isNPCTextCompleted)
        {
            StopAllCoroutines();
            npcText.text = currentNPC.dialogue[currentDialogueIndex].npcLine;
            isNPCTextCompleted = true;
        }
        else
        {
            ShowPlayerResponses();
        }
    }
    }

    private IEnumerator TypeSentence(string sentence)
    {
    npcText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            npcText.text = sentence;
            isNPCTextCompleted = true;
            ShowPlayerResponses();
            yield break;
        }
        npcText.text += letter;
        yield return new WaitForSeconds(typingSpeed);
    }
    isNPCTextCompleted = true;
    }

    private void ShowPlayerResponses()
    {
    npcText.gameObject.SetActive(false);

    for (int i = 0; i < playerButtons.Length; i++)
    {
        if (i < currentNPC.dialogue[currentDialogueIndex].playerResponses.Length)
        {
            playerButtons[i].SetActive(true);
            playerResponses[i].text = currentNPC.dialogue[currentDialogueIndex].playerResponses[i];
        }
        else
        {
            playerButtons[i].SetActive(false);
        }
    }
    }

    IEnumerator EndDialogueAfterDelay()
    {
    yield return new WaitForSeconds(2.0f);
    EndDialogue();
    }
}
