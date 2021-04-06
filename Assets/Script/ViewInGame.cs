using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewInGame : MonoBehaviour
{

    public TMP_Text coinsLabel;
    public TMP_Text distanceLabel;
    public TMP_Text highestScoreLabel;

    public void Start()
    {
        
    }

    void Update()
    {
        coinsLabel.text = GameManager.GetInstance().GetCollectedCoins().ToString();
        distanceLabel.text = PlayerControler.GetInstance().GetDistance().ToString();
        highestScoreLabel.text = PlayerControler.GetInstance().GetMaxScore().ToString();

    }
}
