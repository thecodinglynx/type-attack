using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;
    
    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private GameObject playButton;
    
    [SerializeField]
    private GameObject continueButton;

    [SerializeField]
    private GameObject restartButton;

    public void ShowMainMenu() {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
       ConfigureMenuButtons();
    }

    public void ShowGamePanel() {
        EventManager.TriggerEvent(EventManager.Event.GAME_STATE_CHANGED, new Dictionary<string, object> { { "state", GameManager.GameState.RUNNING } });
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void RestartGame() {
        EventManager.TriggerEvent(EventManager.Event.GAME_STATE_CHANGED, new Dictionary<string, object> { { "state", GameManager.GameState.RESTARTED } });
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void Start()
    {
        EventManager.TriggerEvent(EventManager.Event.GAME_STATE_CHANGED, new Dictionary<string, object> { { "state", GameManager.GameState.STOPPED } });
        ShowMainMenu();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            EventManager.TriggerEvent(EventManager.Event.GAME_STATE_CHANGED, new Dictionary<string, object> { { "state", GameManager.GameState.PAUSED } });
            ShowMainMenu();
        }
    }

    private void ConfigureMenuButtons() {
        if (GameManager.instance.CurrentState() == GameManager.GameState.PAUSED) {
            playButton.SetActive(false);
            continueButton.SetActive(true);
        } else {
            playButton.SetActive(true);
            continueButton.SetActive(false);
        }
    }
}
