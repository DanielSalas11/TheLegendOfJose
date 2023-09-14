using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health,baseAttack,xp;
    public string enemyName;
    public float moveSpeed;
    public bool enemyDied = false;
    [Space (10)]

    [Header("References")]
    public PlayerMovement JoseScript;
    public ScreenShakeController cameraInstance;

    void Start()
    {
       
    }

    void Update()
    {
        if (enemyDied)
        {
            cameraInstance.shakeScreen();
            enemyDied = false;
        }
    }
}
