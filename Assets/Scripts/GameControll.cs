using UnityEngine;
using UnityEngine.UI;

class GameControll : MonoBehaviour {
    public static int scores = 0;
    public static int money = 200;
    public static int playerHP = 100;
    public static int bigTurretCost = 70;
    public static int smallTurretCost = 50;

    public Text scoreText;
    public Text moneyText;
    public Text healthText;
    
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
        scoreText.text = "Scores :" /*+ scores*/;
        moneyText.text = "Money : " /*+ money*/;
        healthText.text = "Health : " /*+ playerHP*/;
    }
}
