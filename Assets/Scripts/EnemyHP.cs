using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour {

    public float maxHP = 100f;
    public float currentHP = 100f;
    public Color MaxDamageColor = Color.red;
    public Color MinDamageColor = Color.blue;

    private GlobalVars vars;

    private void Awake()
    {
        vars = GameObject.Find("GlobalVars").GetComponent<GlobalVars>();
        if (vars != null)
        {
            vars.EnemyList.Add(gameObject);
            vars.EnemyCount++;
        }
        if (maxHP < 1)
        {
            maxHP = 1;
        }
    }
    public void ChangeHP(float adjust)
    {
        gameObject.GetComponent<Animation>().Play("Enemy_recieveDamage");
        if ((currentHP + adjust) > maxHP) { currentHP = maxHP; }
        else
        {
            currentHP += adjust;
        }
    }
	void Start()
	{
	
	}
	
	void Update()
	{
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(MaxDamageColor, MinDamageColor, currentHP / maxHP);
        if (currentHP <= 0)
        {
            EnemyAI enAI = gameObject.GetComponent<EnemyAI>();
            if(enAI != null && vars != null)
            {
                vars.PlayerMoney += enAI.enemyPrice;
                Destroy(gameObject);
            }
        }
	}
    private void OnDestroy()
    {
        if (vars != null)
        {
            vars.EnemyList.Remove(gameObject);
            vars.EnemyCount--;
        }
    }
}
