using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private int value;

    [Header("References")]
    private PlayerMovement JoseScript;

    private void Start()
    {
        JoseScript = PlayerMovement.instance;
    }
    public Money()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            value = Random.Range(1, 3);
            JoseScript.takeMoney(value);
        }
    }
}
