using UnityEngine;
using System.Collections;

public class TurretBuildMenuManager : MonoBehaviour
{
    public GameObject turretBuildMenuPanel;
    public GameObject turretPrefab1;
    public GameObject turretPrefab2;

    float lastTimeScale;
    Transform buildTransform;

    void Awake()
    {
        if (!turretBuildMenuPanel)
        {
            Debug.LogError("ERROR: Turret Build Menu Manager required Turret Build Menu object.");
            gameObject.SetActive(false);
        }
    }

    void OpenBuildPanel(Transform reqBuildTransform)
    {
        turretBuildMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        buildTransform = reqBuildTransform;
    }

    public void CloseBuildPanel()
    {
        turretBuildMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        buildTransform = null;
    }

    public void BuildTurret(int id)
    {
        int playerMoney = 0;
        switch (id)
        {
            case 1:
                playerMoney = GameControll.money;
                if (playerMoney >= 50)
                {
                    Instantiate(turretPrefab1, buildTransform.position, Quaternion.identity);
                    GameControll.Money(GameControll.smallTurretCost);
                    Destroy(buildTransform.gameObject);
                }
                break;
            case 2:
                playerMoney = GameControll.money;
                if (playerMoney >= 70)
                {
                    Instantiate(turretPrefab2, buildTransform.position, Quaternion.identity);
                    GameControll.Money(GameControll.bigTurretCost);
                    Destroy(buildTransform.gameObject);
                }
                break;
        }
        CloseBuildPanel();
    }
}