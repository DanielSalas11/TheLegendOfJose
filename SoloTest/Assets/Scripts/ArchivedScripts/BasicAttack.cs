using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{

    public Transform firePoint;
    public GameObject basicAttackPrefab;
    private SpriteRenderer attackSpriteRenderer;
    public GameObject pauseMenuUI;

    public float basicAttackForce = 3f;
    public float damage = 1f;
    bool canShoot;
    public float coolDownTime = 1.2f;

    private void Start()
    {
        canShoot = true;
    }
    void Update()
    {
            if (Input.GetMouseButtonDown(0) && pauseMenuUI.activeInHierarchy == false && !GetComponentInParent<Animator>().GetBool("Death") && canShoot)
            {
                shoot();
                canShoot = false;
                Invoke("canShootTrue",coolDownTime);
                
            }
    }
    void shoot()
    {
        Quaternion quaternion = new Quaternion();
        GameObject attack = Instantiate(basicAttackPrefab,firePoint.position,quaternion);
        attackSpriteRenderer = attack.GetComponent<SpriteRenderer>();
        Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if ((mousePos.x) > transform.position.x)
        {
            attackSpriteRenderer.flipX = true;
        }
        else if ((mousePos.x) < transform.position.x)
        {
            attackSpriteRenderer.flipX = false;
        }
        rb.AddForce(firePoint.up * basicAttackForce, ForceMode2D.Impulse);

        Destroy(attack,0.22f);
    }

    void canShootTrue()
    {
        Debug.Log("Attack Ready");
        canShoot = true;
    }
}
