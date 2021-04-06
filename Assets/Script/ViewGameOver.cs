using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewGameOver : MonoBehaviour
{

    public TMP_Text coinsLabel;
    public TMP_Text distanceLabel;

    public void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.GetInstance().currentGameState == GameState.GameOver)
        {
            coinsLabel.text = GameManager.GetInstance().GetCollectedCoins().ToString();
            distanceLabel.text = PlayerControler.GetInstance().GetDistance().ToString();
        }

    }
}