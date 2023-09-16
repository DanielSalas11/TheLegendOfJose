using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private Rigidbody2D weaponRb;
    private bool canAttack;
    private Vector3 mousePos;
    private PlayerMovement player;
    private bool limit;
    private bool attacking;

    public int damage = 1;

    private void Awake()
    {
        //player = PlayerMovement.instance;
    }
    void Start()
    {
        weaponRb = GetComponent<Rigidbody2D>();
        canAttack = true;
        player = PlayerMovement.instance;
    }

    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = cursorPos - weaponRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        stickToPlayer();

        if (canAttack)
        {
            weaponRb.rotation = angle;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            canAttack = false;
            mousePos = Input.mousePosition;
            limit = false;
            attacking = true;
        }

        if (!canAttack)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
            attack(mousePos);
        }

    }

    void attack(Vector3 mousePos)
    {
        if(player.currentHP == 0){
            canAttack=false;
            return;
        }
        if (attacking)
        {
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            if (this.transform.position.x > player.transform.position.x + 1f || this.transform.position.x < player.transform.position.x - 1f || this.transform.position.y > player.transform.position.y + 1f || this.transform.position.y < player.transform.position.y - 1f || limit)
            {
                limit = true;

                if (player.GetComponent<SpriteRenderer>().flipX)
                {
                    Vector2 playerPosition = player.GetComponent<Transform>().position; ;
                    playerPosition = new Vector2(playerPosition.x - 0.15f, playerPosition.y - 0.3f);
                    this.transform.position = Vector2.MoveTowards(this.transform.position, playerPosition, 5f * Time.deltaTime);
                    if ((Vector2)this.transform.position == playerPosition)
                    {
                        attacking = false;
                    }
                }
                else if (!player.GetComponent<SpriteRenderer>().flipX)
                {
                    Vector2 playerPosition = player.GetComponent<Transform>().position; ;
                    playerPosition = new Vector2(playerPosition.x + 0.15f, playerPosition.y - 0.3f);
                    this.transform.position = Vector2.MoveTowards(this.transform.position, playerPosition, 5f * Time.deltaTime);
                    if ((Vector2)this.transform.position == playerPosition)
                    {
                        attacking = false;
                    }
                }



            }
            else if (!limit)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, mousePos, 5f * Time.deltaTime);
            }
        }
        else if (!attacking)
        {
            canAttack = true;
        }
    }

    void stickToPlayer()
    {
        if (player.GetComponent<SpriteRenderer>().flipX && canAttack)
        {
            Vector2 playerPosition = player.GetComponent<Transform>().position; ;
            this.transform.position = new Vector2(playerPosition.x - 0.15f, playerPosition.y - 0.3f);
        }
        else if (!player.GetComponent<SpriteRenderer>().flipX && canAttack)
        {
            Vector2 playerPosition = player.GetComponent<Transform>().position; ;
            this.transform.position = new Vector2(playerPosition.x + 0.15f, playerPosition.y - 0.3f);
        }

        this.GetComponent<SpriteRenderer>().color = player.GetComponent<SpriteRenderer>().color;
    }
}
