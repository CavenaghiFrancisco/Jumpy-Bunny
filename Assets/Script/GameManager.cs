using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Menu,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.Menu;
    private static GameManager sharedInstance;
    public Canvas mainMenu;
    public Canvas gameMenu;
    public Canvas gameOver;
    int collectedCoins = 0;

    private void Awake()
    {
        sharedInstance = this;
        LevelGenerator.sharedInstance.createInitialBlocks();
    }

    public static GameManager GetInstance()
    {
        return sharedInstance;
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        PlayerControler.GetInstance().GetMaxScore();
        collectedCoins = 0;
        LevelGenerator.sharedInstance.RemoveAllBlocks();
        LevelGenerator.sharedInstance.createInitialBlocks();
        PlayerControler.GetInstance().StartGame();
        ChangeGameState(GameState.InGame);
    }

    public void Start()
    {
        currentGameState = GameState.Menu;
        mainMenu.enabled = true;
        gameMenu.enabled = false;
        gameOver.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("s") && currentGameState != GameState.InGame)
        {
            ChangeGameState(GameState.InGame);
            StartGame();
        }
    }

    // Update is called once per frame
    public void GameOver()
    {
        LevelGenerator.sharedInstance.RemoveAllBlocks();
        ChangeGameState(GameState.GameOver);
    }

    public void BackToMainMenu()
    {
        ChangeGameState(GameState.Menu);
    }

    void ChangeGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.InGame:
                currentGameState = GameState.InGame;
                mainMenu.enabled = false;
                gameMenu.enabled = true;
                gameOver.enabled = false;
                break;
            case GameState.GameOver:
                currentGameState = GameState.GameOver;
                mainMenu.enabled = false;
                gameMenu.enabled = false;
                gameOver.enabled = true;
                break;
            case GameState.Menu:
                currentGameState = GameState.Menu;
                mainMenu.enabled = true;
                gameMenu.enabled = false;
                gameOver.enabled = false;
                break;
            default:
                
                break;
        }
        currentGameState = newGameState;
    }

    public void CollectCoins()
    {
        collectedCoins++;
    }

    public int GetCollectedCoins()
    {
        return collectedCoins;
    }
}

