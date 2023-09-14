using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableMovement : MonoBehaviour
{
    public Material flashMaterial;
    protected Vector2 target;
    protected float speed = 0.2f;
    public GameObject pickUpParticles;
    public bool followPlayer = true;
    private PlayerMovement player = PlayerMovement.instance;

    public Pickable pickable;
    public Sprite itemSprite;
    public string pickableName;

    void Start()
    {
        target = PlayerMovement.instance.transform.position;
        if (this.CompareTag("Drop"))
        {
            pickable = new Pickable(pickableName, 1, itemSprite);
        }

    }

    public void FixedUpdate()
    {
        targetPlayer();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().material = flashMaterial;
            gameObject.GetComponent<Animator>().SetBool("pickedUp", true);
            Instantiate(pickUpParticles, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.23f);
        }

        if (collision.CompareTag("Player") && pickable != null)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            PlayerMovement.joseInventory.addItem(pickable);
        }
    }

    public void targetPlayer()
    {
        if (followPlayer)
        {
            target = PlayerMovement.instance.transform.position;
            speed += 0.08f;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, target.y), speed * Time.deltaTime);
        }
    }
}