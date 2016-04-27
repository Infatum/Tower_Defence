using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Menu Manager class
    /// </summary>
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private bool escControlled = true;

    /// <summary>
    /// Awake method;
    /// Checks if menu panel is missing;
    /// If menu panel is missing - shows error message;
    /// </summary>
    void Awake()
    {
        if (!menuPanel)
        {
            Debug.LogError("ERROR : Menu Manager required Menu Panel object");
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Opens game menu;
    /// 
    /// </summary>
    /// <param name="hard">Freezed menu(can't be closed by esc key)</param>
    public void OpenMenu(bool hard)
    {
        escControlled = !hard;
        menuPanel.SetActive(!menuPanel.activeSelf);
        if (menuPanel.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
    }
    /// <summary>
    /// Induces the menu panel by pressing a 'esc' key;
    /// Stops the game, while game menu is active;
    /// </summary>
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
    /// <summary>
    /// Loads Level by it's ID;
    /// </summary>
    /// <param name="level_id"></param>
    public void LoadLevel(int level_id)
    {
        Application.LoadLevel(level_id);
    }
    /// <summary>
    /// Quits the game or stops in Unity;
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }
}
