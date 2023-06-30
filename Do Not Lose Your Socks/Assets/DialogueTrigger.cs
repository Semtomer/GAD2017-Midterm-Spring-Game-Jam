using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject playerSpeechBubble;
    public GameObject momSpeechBubble;
    public GameObject nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerSpeechBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerSpeechBubble.SetActive(false);
            momSpeechBubble.SetActive(false);
            nextLevel.SetActive(true);
        }
    }
}
