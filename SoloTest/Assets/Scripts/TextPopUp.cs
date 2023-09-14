using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextPopUp : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialog;
    public int fontSize;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueBox.SetActive(true);
            dialogueText.fontSize = fontSize;
            dialogueText.text = dialog;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
        }
    }
}
