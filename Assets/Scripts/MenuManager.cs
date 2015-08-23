using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public bool escControlled = true;

    void Awake()
    {
        if (!menuPanel)
        {
            Debug.LogError("ERROR : Menu Manager required Menu Panel object");
            gameObject.SetActive(false);
        }
    }

    public void OpenMenu(bool hard)
    {
        escControlled = !hard;
        menuPanel.SetActive(!menuPanel.activeSelf);
        if (menuPanel.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && escControlled)
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            if (menuPanel.activeSelf)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
    public void LoadLevel(int level_id)
    {
        Application.LoadLevel(level_id);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }
}
