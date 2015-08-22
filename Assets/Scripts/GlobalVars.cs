using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVars : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    public List<GameObject> TurretList = new List<GameObject>();
    public int EnemyCount = 0;
    public int TurretCount = 0;
    public float PlayerMoney = 200f;
}
