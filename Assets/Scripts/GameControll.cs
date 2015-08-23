using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameControll : MonoBehaviour {
    public static int scores = 0;
    public static int money = 200;
    public static int playerHP = 100;

    public Text scoreText;
    public Text moneyText;
    public Text health;
    
	void Start()
	{

	}
    public static void ChangePlayerHP(int diff)
    {
        playerHP += diff;
            
    }
    public static void ScoreEngage(int sc)
    {
        scores += sc;
    }
    public static void Money(int mon)
    {
        money += mon;
    }
	// Update is called once per frame
	void Update()
	{
        scoreText.text = "Scores :" + scores;
        moneyText.text = "Money : " + money;
        health.text = "Health : " + playerHP;
    }
}
