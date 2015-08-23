using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{

    public GameObject MenuPanel;

    void Awake()
    {
        if (!MenuPanel)
        {
            Debug.LogError("ERROR : Menu Manager required Menu Panel object");
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPanel.SetActive(!MenuPanel.activeSelf);
            if (MenuPanel.activeSelf)
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
