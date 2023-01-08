using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState {
        RUNNING,
        STOPPED,
        PAUSED,
        RESTARTED
    }

    private static GameManager gameManager;

    public static GameManager instance
    {
        get
        {
            if (!gameManager)
            {
                gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (!gameManager)
                {
                    Debug.LogError("There needs to be one active GameManager script on a GameObject in your scene.");
                }
                else
                {
                    //  Sets this to not be destroyed when reloading scene
                    DontDestroyOnLoad(gameManager);
                }
            }
            return gameManager;
        }
    }

    [SerializeField]
    private GameObject enemySpawner;

    [SerializeField]
    private GameObject typeHandler;

    [SerializeField]
    private GameObject spaceStation;

    private List<GameObject> disableableGameObjects;

    private GameState curState = GameState.STOPPED;

    public GameState CurrentState() {
        return curState;
    }

    void Start() {
        disableableGameObjects = new List<GameObject>();
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.Event.GAME_STATE_CHANGED, onGameStateChange);
        Debug.Log("GameManager started listening for game state");
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.Event.GAME_STATE_CHANGED, onGameStateChange);
        Debug.Log("GameManager stopped listening for game state");
    }

    private void onGameStateChange(Dictionary<string, object> message) {
        var updGameState = (GameState)message["state"];
        if (updGameState == GameState.RESTARTED) {
            RestartGame();
            curState = GameState.STOPPED;
            updGameState = GameState.RUNNING;
        }

        switch(curState) {
            case GameState.STOPPED:
                HandleStopToNewState(updGameState);
                break;
            case GameState.RUNNING:
                HandleRunningToNewState(updGameState);
                break;
            case GameState.PAUSED:
                HandlePausedToNewState(updGameState);
                break;
        }
        curState = updGameState;
    }

    private void HandleStopToNewState(GameState newState) {
        switch(newState) {
            case GameState.RUNNING:
                StartNewGame();
                break;
            case GameState.PAUSED:
            case GameState.STOPPED:
                // do nothing
                break;
        }
    }

    private void HandleRunningToNewState(GameState newState) {
        switch(newState) {
            case GameState.PAUSED:
                PauseGame();
                break;
            case GameState.RUNNING:
            case GameState.STOPPED:
                // do nothing
                break;
        }
    }

    private void HandlePausedToNewState(GameState newState) {
        switch(newState) {
            case GameState.PAUSED:
                // do nothing
                break;
            case GameState.RUNNING:
                ContinueGame();
                break;
            case GameState.STOPPED:
                // do nothing
                break;
        }
    }

    private void StartNewGame() {
        disableableGameObjects.Add(Instantiate(enemySpawner, new Vector3(0, 0, 10), Quaternion.identity));
        disableableGameObjects.Add(Instantiate(typeHandler, new Vector3(0, 0, 0), Quaternion.identity));
        disableableGameObjects.Add(Instantiate(spaceStation, new Vector3(0, 0, 1), Quaternion.identity));
        ContinueGame();
    }

    private void RestartGame() {
        foreach(var cur in disableableGameObjects) {
            Destroy(cur);
        }
        disableableGameObjects = new List<GameObject>();
    }

    private void ContinueGame() {
        foreach (var cur in disableableGameObjects) {
            cur.SetActive(true);
        }
    }

    private void PauseGame() {
        foreach (var cur in disableableGameObjects) {
            cur.SetActive(false);
        }
    }
}
