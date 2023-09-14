using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    [Header("Shake Properties")]
    public float shakeTimeRemaining;
    private float shakePower;

    public static ScreenShakeController instance;


    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount,yAmount, -8.999f);
        }
    }

    public void shakeScreen()
    {
        shakeTimeRemaining = .1f;
        shakePower = 0.045f;
    }
}
