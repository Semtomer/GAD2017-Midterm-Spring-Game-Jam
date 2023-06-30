using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool PlayerSpeakingFirst;

    [Header("Dialogue TMP Text")]
    [SerializeField] private TextMeshProUGUI playerDialogueText;
    [SerializeField] private TextMeshProUGUI momDialogueText;

    [Header("Continue Buttons")]
    [SerializeField] private GameObject playerContinueButton;
    [SerializeField] private GameObject momContinueButton;

    [Header("SpeechBubble")]
    [SerializeField] private GameObject momSpeechBubble;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;
    [TextArea]
    [SerializeField] private string[] momDialogueSentences;

    private bool dialogueStarted;

    private int playerIndex;
    private int momIndex;

    private void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        if (PlayerSpeakingFirst)
        {
            StartCoroutine(TypePlayerDialouge());
        }
        else
        {
            StartCoroutine(TypeMomDialouge());
        }
    }
    private IEnumerator TypePlayerDialouge()
    {
        foreach (char letter in playerDialogueSentences[playerIndex].ToCharArray())
        {
            playerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        playerContinueButton.SetActive(true);
    }

    private IEnumerator TypeMomDialouge()
    {
        foreach (char letter in momDialogueSentences[momIndex].ToCharArray())
        {
            momDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }


        //momContinueButton.SetActive(true);
    }

    public void ContinuePlayerDialogue()
    {
        momContinueButton.SetActive(false);

        if (playerIndex < playerDialogueSentences.Length - 1)
        {
            if (dialogueStarted)
            {
                playerIndex++;
            }
            else
            {
                dialogueStarted = true;
            }
            

            playerDialogueText.text = string.Empty;
            StartCoroutine(TypePlayerDialouge());
        }
    }

    public void ContinueMomDialogue()
    {
        playerContinueButton.SetActive(false);

        if (momIndex < momDialogueSentences.Length - 1)
        {
            if (dialogueStarted)
            {
                momIndex++;
            }
            else
            {
                dialogueStarted = true;
            }

            momSpeechBubble.SetActive(true);
            momDialogueText.text = string.Empty;
            StartCoroutine(TypeMomDialouge());
        }
    }
}
