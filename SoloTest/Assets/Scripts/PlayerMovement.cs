using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 2f;
    Rigidbody2D rb2D;
    public SpriteRenderer PlayerSpriteR;
    public Transform attackPosition;
    public Animator playerAnimator;
    public int maxHP = 3;
    public int currentHP = 3;
    public RadialBar playerRadialBar;
    public int money = 0;
    public int currentXP = 0;
    public int maxXP = 3;
    public int level = 1;

    public static PlayerMovement instance;
    public Vector2 respawnPosition;

    public static Inventory joseInventory = new Inventory(5);
    public Inventory joseInventoryCopy = new Inventory(5);
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnabled;

    public void ToggleEncryption(bool EncryptionEnabled)
    {
        this.EncryptionEnabled = EncryptionEnabled;
    }

    public void SerializeJson(){
        if(DataService.SaveData("/player-stats.json", new PlayerData(instance),EncryptionEnabled)){
            Debug.Log("Data Saved.");
        }else{
            Debug.LogError("Could not save file! Show something on the UI about it!");
            Debug.Log("Error");
        }
    }

    private void Awake()
    {
        instance = this;
        if(maxXP == 0){
            maxXP = 3;
        };
    }

    void Start() //Defines rigidBody2D
    {
        getRespawnPosition();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.transform.position = respawnPosition;
        //joseInventory = new Inventory(5);
        //joseInventoryCopy = new Inventory(5);
        //loadPlayer();
        updateRadialBar();
        Debug.Log(DataService.LoadData<PlayerData>("/player-stats.json",EncryptionEnabled));
    }

    private void updateRadialBar()
    {
        playerRadialBar.maxValue = maxHP;
        playerRadialBar.currentValue = currentHP;
        playerRadialBar.Add(0); //Updates playerRadialBar, for some reason it starts with 0/0 so we have to update it as soon as it starts.
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (currentHP < 1) //Checks if player's HP is less than 0 and kills it if true.
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            playerAnimator.SetBool("Death", true);
            return;
        }
        faceMouse();
        movement();
    }

    void faceMouse() //Method that checks Mouse position so player will always be facing that direction.
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if ((mousePos.x)>transform.position.x)
        {
            PlayerSpriteR.flipX = false;
        }
        else if ((mousePos.x) < transform.position.x)
        {
            PlayerSpriteR.flipX = true;
        }
    }

    public void movement() //Character manual movement script
    {
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
        }
        else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, runSpeed);
        }
        else if (Input.GetKey("s") || Input.GetKey("down"))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, -runSpeed);
        }
        else
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }
        if (rb2D.velocity.x != 0 || rb2D.velocity.y != 0)
        {
            playerAnimator.SetBool("Running", true);
        }
        else
        {
            playerAnimator.SetBool("Running", false);
        }
    }

    public void takeDamage(int damageTaken) //TakesDamage depending on enemy's damage
    {
        playerAnimator.SetTrigger("GotHit");
        currentHP -= damageTaken;
        playerRadialBar.Add(-damageTaken);
    }
    public void takeHealing(int healing) //TakesHealing depending on healing value.
    {
        transform.GetChild(0).gameObject.SetActive(true);
        if ((currentHP + healing)> maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += healing;
        }
        playerRadialBar.Add(healing);
    }

    public void takeMoney(int newMoney) //TakesMoney depending on coin's value.
    {
        money += newMoney;
        Debug.Log("You received " + newMoney + " coins. " + "\n Total money: " + money);
    }

    public void takeXP(int xpValue) //TakesXP depending on xp value.
    {
        if ((currentXP + xpValue) >= maxXP) //When character gets XP to level UP
        {
            level += 1;
            maxHP += 1;
            currentHP += 1;
            updateRadialBar();

            currentXP -= maxXP;
            maxXP = (maxXP * 2);
            transform.GetChild(1).gameObject.SetActive(true);
            Debug.Log("You leveled UP! Current Level: " + level);
            Invoke("turnOffLevelAnim", 1.250f);
        }
        currentXP += xpValue;
        Debug.Log("You received " + xpValue + " xp. " + "\n Current XP: " + currentXP);
    }

    public void turnOffLevelAnim()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void savePlayer()
    {
        //SaveSystem.SavePlayer(this);
    }

    /*public void loadPlayer()
    {
        PlayerData data = SaveSystem.loadData();

        maxHP = data.maxHP;
        currentHP = data.currentHP;
        money = data.money;
        currentXP = data.currentXP;
        maxXP = data.maxXP;
        level = data.level;
        for (int i = 0; i < joseInventory.itemSlots.Length; i++)
        {
            joseInventory.itemSlots[i].setPickableName(data.pickableName[i]);
            joseInventory.itemSlots[i].setQuantity(data.quantity[i]);
        }
    }*/

    public void getRespawnPosition()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //Falta a�adir transici�n
        if (currentScene.buildIndex == 0)
        {
            respawnPosition = new Vector2(16.2f, -1f);
        }
        else if (currentScene.buildIndex == 1)
        {
            respawnPosition = new Vector2(-6.57f, -0.82f);
        }
    }
}