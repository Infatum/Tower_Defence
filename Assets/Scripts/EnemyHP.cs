using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    /// <summary>
    /// Enemy Hit Points Class;
    /// </summary>
    public float maxHP = 100f;
    public Color MaxDamageColor = Color.red;
    public Color MinDamageColor = Color.blue;

    float currentHP;
    float oldHP = -1;

    /// <summary>
    /// Indicate enemy color to red, when it is near to death;
    /// </summary>
    void Awake()
    {
        ChangeColor();
        if (maxHP < 1)
        {
            maxHP = 1f;
        }
        currentHP = maxHP;
    }
    /// <summary>
    /// Recieving damage method. Activates animation of a recieved damage by enemy;
    /// Activates sounds of a receiving damage by enemy;
    /// Substracts enemies health point;
    /// Causes death of enemy, if it's health points are 0;
    /// </summary>
    /// <param name="damage">damage value</param>
    public void ReceiveDamage(float damage)
    {
        gameObject.GetComponent<Animation>().Play("Enemy_RecieveDamage", PlayMode.StopAll);
        gameObject.GetComponent<AudioSource>().Play();
        if ((currentHP - damage) > maxHP)
        {
            currentHP = maxHP;
            ChangeColor();
        }
        else
        {
            currentHP -= damage;
        }

        if (currentHP <= 0)
        {
            Die();

        }
    }
    /// <summary>
    /// Causes death to enemy, destroys it's gameobject;
    /// Increases player's Score and Money value;
    /// </summary>
   public void Die()
    {
        GameControll.ScoreEngage(5);
        GameControll.Money(15);
        Destroy(gameObject, 0.05f);
    }
    /// <summary>
    /// Changes color of the enemy, if it's near to death;
    /// </summary>
    void Update()
    {
        ChangeColor();
    }
    /// <summary>
    /// Change color method. Changes color of the enemy, if it's near to death;
    /// </summary>
    void ChangeColor()
    {
        if (oldHP != currentHP)
        {
            oldHP = currentHP;
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(MaxDamageColor, MinDamageColor, currentHP / maxHP);
        }
    }
}