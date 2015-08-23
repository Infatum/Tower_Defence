using UnityEngine;
using System.Collections;

public class TurretBuildMenuManager : MonoBehaviour
{
    /// <summary>
    /// Main Turret Build Class;
    /// </summary>
    public GameObject turretBuildMenuPanel;
    public GameObject turretPrefab1;
    public GameObject turretPrefab2;

    float lastTimeScale;
    Transform buildTransform;

    /// <summary>
    /// Update method;
    /// Pics a point of a mouse for the Collider of a Build Plate;
    /// Opens turret build manager
    /// </summary>
    void Awake()
    {
        if (!turretBuildMenuPanel)
        {
            Debug.LogError("ERROR: Turret Build Menu Manager required Turret Build Menu object.");
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Opens Building menu, where player buys new buildings or upgrades exsisting;
    /// </summary>
    /// <param name="reqBuildTransform">Transform required bulding</param>
    void OpenBuildPanel(Transform reqBuildTransform)
    {
        turretBuildMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        buildTransform = reqBuildTransform;
    }
    /// <summary>
    /// Closes the build menu;
    /// </summary>
    public void CloseBuildPanel()
    {
        turretBuildMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        buildTransform = null;
    }
    /// <summary>
    /// Builds a new turret by the ID of it;
    /// </summary>
    /// <param name="id">int ID of a turret</param>
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