using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public Vector3 rangeOffset;
    private Vector2 originalPosition;

    public GameObject JoseObject;
    public GameObject deathParticles;
    public GameObject coin;
    public GameObject zombieFlesh;

    private float waitTime;
    public float startWaitTime;

    public GameObject moveSpot;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private bool patrol;

    private void Start()
    {
        originalPosition = gameObject.transform.position;

        minX = originalPosition.x + -2.5f;
        maxX = originalPosition.x + 2.5f;
        minY = originalPosition.y + -2.5f;
        maxY = originalPosition.y + 2.5f;

        waitTime = startWaitTime;
        moveSpot.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void FixedUpdate()
    {
        if (!GetComponent<Animator>().GetBool(("Death")))
        {
            targetPlayer();
            startPatrol();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") && health > 0)
        {
            health -= 1;
            if (health > 0)
            {
                GetComponent<Animator>().SetBool("gotHit", true);
                Invoke("hitEnd", 0.5f);
                gotHit();
            }
            else if (health == 0)
            {
                gotHit();
                dies();
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            JoseScript.takeDamage(baseAttack);
        }
    }

    private void hitEnd()
    {
        GetComponent<Animator>().SetBool("gotHit", false);
    }

    private void dies()
    {
        Destroy(moveSpot);
        int randomValue = Random.Range(1, 11);
        enemyDied = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Animator>().SetBool("Death", true);
        JoseScript.takeXP(xp);
        if (randomValue >= 1 && randomValue <= 5) //50% change of Spawning a coin after Death
        {

            Instantiate(coin, transform.position, Quaternion.identity);
        }
        if (randomValue >= 5 && randomValue <= 9) //50% change of Spawning a coin after Death
        {

            Instantiate(zombieFlesh, transform.position, Quaternion.identity);
        }

    }
    void gotHit()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity); //Instantiates on death particless
    }

    void startPatrol()
    {
        if (patrol)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.transform.position, moveSpeed * Time.deltaTime);
            waitTime -= Time.deltaTime;

            if (transform.position.x > moveSpot.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                gameObject.GetComponent<Animator>().SetBool("Running", true);
            }
            else if (transform.position.x < moveSpot.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<Animator>().SetBool("Running", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Running", false);
            }
            {
                if (waitTime <= 0)
                {
                    moveSpot.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    waitTime = startWaitTime;
                }
            }
        }
    }

    private void targetPlayer()
    {
        Vector3 playerPosition = JoseObject.transform.position;

        if (((transform.position.x + rangeOffset.x) > playerPosition.x && (transform.position.x - rangeOffset.x) < playerPosition.x) && (transform.position.y + rangeOffset.y) > playerPosition.y && (transform.position.y - rangeOffset.y) < playerPosition.y) //If player is in range
        {
            patrol = false;
            if (transform.position.x > playerPosition.x) //Makes enemy look towards the player if in range to chase
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (!GetComponent<Animator>().GetBool(("Death"))) // Makes enemy run towards player
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.x, playerPosition.y), moveSpeed * Time.deltaTime);
                GetComponent<Animator>().SetBool("Running", true);
            }
        }
        else
        {
            patrol = true;
        }

    }
}