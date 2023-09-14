using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    //public GameObject jose;
    float transparente = 0.58f;
    float noTransparente = 1f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Tas adentro");
            collision.GetComponent<SpriteRenderer>().color = new Color (1,1,1,transparente);
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparente);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Tas afuera");
            collision.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, noTransparente);
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, noTransparente);
        }
    }
}
