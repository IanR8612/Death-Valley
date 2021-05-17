using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup MainMenu;
    public CanvasGroup Leaderboard;
    public CanvasGroup DeathPanel;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        Show(MainMenu);
        Hide(Leaderboard);
        Hide(DeathPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        Hide(MainMenu);
        Time.timeScale = 1;
    }

    public void ShowLeaderboard()
    {
        Show(Leaderboard);
        Hide(MainMenu);
        Hide(DeathPanel);
    }

    public void ReturnToMain()
    {
        Show(MainMenu);
        Hide(Leaderboard);
        Hide(DeathPanel);
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
