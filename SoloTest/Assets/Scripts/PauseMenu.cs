using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryMenuUI;
    public PlayerMovement joseScript;
    public Text hpText;
    public Text xpText;
    public Text lvlText;
    private GameObject[] slotTexts;
    public GameObject slot1Text;
    public GameObject slot2Text;
    public GameObject slot3Text;
    public GameObject slot4Text;
    public GameObject slot5Text;
    public GameObject coinsText;
    private Image[] slotImages;
    public Image slot1Image;
    public Image slot2Image;
    public Image slot3Image;
    public Image slot4Image;
    public Image slot5Image;

    private TextMeshProUGUI textMesh;

    void Start()
    {
        slotTexts = new GameObject[5] {slot1Text,slot2Text,slot3Text,slot4Text,slot5Text};
        slotImages = new Image[5] { slot1Image, slot2Image, slot3Image, slot4Image, slot5Image };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            hpText.text = "HP: " + joseScript.currentHP + "/" + joseScript.maxHP;
            xpText.text = "XP: " + joseScript.currentXP + "/" + joseScript.maxXP;
            lvlText.text = "Level " + joseScript.level;
            joseScript.SerializeJson();
            if (gameIsPaused)
            {
                resume(pauseMenuUI);
            }
            else
            {
                pause(pauseMenuUI);
            }
        }
        if (Input.GetKeyDown("i"))
        {
            textMesh = coinsText.GetComponent<TextMeshProUGUI>();
            textMesh.text = joseScript.money.ToString();

            for (int i = 0; i < PlayerMovement.joseInventory.itemSlots.Length; i++)
            {
                if (PlayerMovement.joseInventory.itemSlots[i] != null)
                {
                    textMesh = slotTexts[i].GetComponent<TextMeshProUGUI>();
                    textMesh.text = "" + PlayerMovement.joseInventory.itemSlots[i].getQuantity();
                    slotImages[i].sprite = PlayerMovement.joseInventory.itemSlots[i].itemSprite;
                    slotTexts[i].SetActive(true);
                    slotImages[i].gameObject.SetActive(true);
                    Debug.Log("Item slot" + i + ":" + PlayerMovement.joseInventory.itemSlots[i].getPickableName() + PlayerMovement.joseInventory.itemSlots[i].getQuantity());
                }
            }

            if (gameIsPaused)
            {
                resume(inventoryMenuUI);
            }
            else
            {
                pause(inventoryMenuUI);
            }
        }
    }

    public void resume(GameObject menu)
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void pause(GameObject menu)
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
