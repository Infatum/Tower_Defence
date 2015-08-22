using UnityEngine;
using System.Collections;

public class TurretHP : MonoBehaviour {

    public float maxHP = 100f;
    public float curHP = 100f;
    private GlobalVars vars;

    private void Awake()
    {
        vars = GameObject.Find("GlobalVars").GetComponent<GlobalVars>();

        if (vars != null)
        {
            vars.TurretList.Add(gameObject);
            vars.TurretCount++;
        }
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
    private void OnDestroy()
    {
        if (vars != null)
        {
            vars.TurretList.Remove(gameObject);
            vars.TurretCount--;
        }
    }
}
