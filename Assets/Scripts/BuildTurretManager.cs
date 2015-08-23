using UnityEngine;
using System.Collections;

public class BuildTurretManager : MonoBehaviour
{
    GameObject tileSelectionMarker;
    GameObject selectorSprite;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // WARN: Cam must be in Orthographic mode
            //Pic point for the collider of a Build Plate
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            //Open turret build manager
            if (hitCollider)
            {
                if (hitCollider.gameObject.CompareTag("Turret Plate"))
                {
                    GameObject.Find("Build Menu Manager").SendMessage("OpenBuildPanel", hitCollider.transform);
                }
            }
        }
    }
}
