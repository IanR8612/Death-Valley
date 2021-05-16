using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
    public Transform playerPrefab;
    public Transform playerSpawnPoint;

    public CanvasGroup restartMenu;

    float countdownSearch = 1f;

    public Text playerRespawn;


    void Start()
    {
        Hide(restartMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerIsAlive())
        {
            Show(restartMenu);
            Time.timeScale = 0;
        }
    }

    public void RespawnPlayer()
    {
        playerRespawn.text = "reset";
        Hide(restartMenu);
        Time.timeScale = 1;
        Debug.Log("Player respawned.");
        Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
    }

    bool PlayerIsAlive()
    {
        countdownSearch -= Time.deltaTime;
        if (countdownSearch <= 0f)
        {
            countdownSearch = 1f;
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                Debug.Log("Player dead.");
                return false;
            }
        }
        //Debug.Log("Player alive.");
        return true;
    }

    protected void Show(CanvasGroup canvasGroup)  // show a Canvas group  (3 properties)
    {
        canvasGroup.alpha = 1;   // 1 = opaque
        canvasGroup.interactable = true;  // allows interactions; functions
        canvasGroup.blocksRaycasts = true;  // catches mouse click
    }

    protected void Hide(CanvasGroup canvasGroup)  // hide a Canvas group  (3 properties)
    {
        canvasGroup.alpha = 0;   // 0 = transparent
        canvasGroup.interactable = false;   // will not allow interaction
        canvasGroup.blocksRaycasts = false;  // will not send ray or vector
    }
}
