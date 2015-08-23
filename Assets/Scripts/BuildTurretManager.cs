﻿using UnityEngine;
using System.Collections;


public class BuildTurretManager : MonoBehaviour
{
    /// <summary>
    /// Building a turret class.
    /// </summary>
    GameObject tileSelectionMarker;
    GameObject selectorSprite;

    /// <summary>
    /// Update method;
    /// Pics a point of a mouse for the Collider of a Build Plate;
    /// Opens turret build manager
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // WARN: Cam must be in Orthographic mode
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

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
