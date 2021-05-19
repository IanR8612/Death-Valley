using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup MainMenu;
    public CanvasGroup Leaderboard;
    public CanvasGroup DeathPanel;
    public CanvasGroup PauseMenu;

    public AudioSource buttonSFX;

    bool playing;


    // Start is called before the first frame update
    void Start()
    {
        playing = false;
        Time.timeScale = 0;
        Show(MainMenu);
        Hide(Leaderboard);
        Hide(DeathPanel);
        Hide(PauseMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (playing == true && Input.GetKeyDown("escape"))
        {
            Time.timeScale = 0;
            Show(PauseMenu);
            Hide(Leaderboard);
            Hide(MainMenu);
            Hide(DeathPanel);
        }
    }

    public void ButtonSound()
    {
        buttonSFX.Play();
    }

    public void PlayGame()
    {
        Hide(MainMenu);
        Hide(PauseMenu);
        Time.timeScale = 1;
        playing = true;
    }

    public void ShowLeaderboard()
    {
        playing = false;
        Show(Leaderboard);
        Hide(MainMenu);
        Hide(DeathPanel);
        Hide(PauseMenu);
    }

    public void ReturnToMain()
    {
        playing = false;
        Show(MainMenu);
        Hide(Leaderboard);
        Hide(DeathPanel);
        Hide(PauseMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
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
