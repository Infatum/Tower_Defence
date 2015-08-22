using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float maxHP = 100f;
    public Color MaxDamageColor = Color.red;
    public Color MinDamageColor = Color.blue;

    float currentHP;

    void Awake()
    {
        ChangeColor();
        if (maxHP < 1)
        {
            maxHP = 1f;
        }
        currentHP = maxHP;
    }

    public void ReceiveDamage(float damage)
    {
        gameObject.GetComponent<Animation>().Play("Enemy_RecieveDamage", PlayMode.StopAll);
        if ((currentHP - damage) > maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP -= damage;
        }

        if (currentHP <= 0)
        {
            gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(MaxDamageColor, MinDamageColor, currentHP / maxHP);
    }

    void OnDestroy()
    {
        
    }
}