using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string enchantItSceneName;
    public GameManager gameManager;
    public static bool skipTeleportEffect;

    public void UpdateOption(string[] options)
    {
        if (options.Length != 0)
        {
            LoadScene(options);
            return;
        }

        if (options.Length == 0 || options[0] == "Option")
        {
            return;
        }
    }

    public void LoadScene(string[] options)
    {
        string option = options[0];
        if (option.Equals("main", StringComparison.OrdinalIgnoreCase))
        {
            gameManager.MainMenu();
        }
    }
}
