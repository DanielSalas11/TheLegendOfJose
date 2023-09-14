using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    /*
    public int scene;
    public GameObject joseObject;
    public PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = PlayerMovement.instance;
            player.savePlayer();
            SceneManager.LoadScene(scene);
            player.getRespawnPosition();
        }
    }*/

    public int sceneBuildIndex;

    //Level move zoned enter, if collider is a player
    // Move game to another scene

    private void OnTriggerEnter2D(Collider2D other){
        //Could use other.getComponent<Player>() to see if the game object has a Player component
        // Tags work too, Maybe some players have different script components?
        if(other.tag =="Player"){
            print("Switched Scene");
            //Player entered, so move level
            print("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
