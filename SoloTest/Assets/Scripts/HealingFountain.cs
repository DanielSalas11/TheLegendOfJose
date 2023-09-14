using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingFountain : MonoBehaviour
{
    public GameObject Jose;
    public PlayerMovement JoseScript;
    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialog;
    public bool canHeal;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Jose.GetComponent<Transform>().position.y < 3.175f)
        {
            canHeal = true;
            dialogueBox.SetActive(true);
            dialogueText.text = dialog;
        }
        else if (collision.CompareTag("Player"))
        {
            float transparente = 0.58f;
            Jose.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparente);
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparente);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canHeal = false;
            float noTransparente = 1f;
            Jose.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, noTransparente);
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, noTransparente);
            dialogueBox.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("e") && canHeal)
        {
            useFountainHeal();
        }
    }
    public void useFountainHeal()
    {
        if (canHeal)
        {
            Debug.Log("Fountain Used");
            canHeal = false;
            JoseScript.takeHealing(JoseScript.maxHP);
            Invoke("turnOffHeal", 1.250f);
        }
    }

    public void turnOffHeal()
    {
        Jose.transform.GetChild(0).gameObject.SetActive(false);
        canHeal = true;
    }
}
