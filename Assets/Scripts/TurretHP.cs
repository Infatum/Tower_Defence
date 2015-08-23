using UnityEngine;
using System.Collections;

public class TurretHP : MonoBehaviour {

    public float maxHP = 100f;
    public float curHP = 100f;

    private void Awake()
    {
        if (maxHP < 1) maxHP = 1;
    }
    public void ChangeHP(float adjust)
    {
        if((curHP + adjust) > maxHP)
        {
            curHP = maxHP;
        }
        else
        {
            curHP += adjust;
        }
        if (curHP > maxHP)
        { curHP = maxHP; }
    }

	void Start()
	{
	
	}
	
	void Update()
	{
	    if (curHP <= 0)
        {
            Destroy(gameObject);
        }
	}
}
