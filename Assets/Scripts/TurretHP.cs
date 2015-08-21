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
        if (maxHP < maxHP) maxHP = 1;
    }
    private void ChangeHP(float adjust)
    {

    }
	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
