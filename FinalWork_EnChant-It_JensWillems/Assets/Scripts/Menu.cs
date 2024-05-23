using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject Spellbook;
    public Book Book;
    public AutoFlip AutoFlip;
    private bool isMenuOpenedForFirstTime = false;

    public event Action OnMenuOpenedFirstTime;
    public WorldSpaceCanvasController WorldSpaceCanvasController;

    private void Start()
    {
        Spellbook.SetActive(false);
    }
    public void UpdateMenu(string[] menuCommands)
    {
        if (menuCommands.Length != 0)
        {
            MenuCommands(menuCommands);
            return;
        }

        if (menuCommands.Length == 0 || menuCommands[0] == "spell")
        {
            return;
        }
    }

    public void MenuCommands(string[] menuCommands)
    {
        if (menuCommands.Length > 0)
        {
            string menuCommandName = menuCommands[0];

            if (menuCommandName.Equals("book", StringComparison.OrdinalIgnoreCase))
            {
                Spellbook.SetActive(true);
                WorldSpaceCanvasController.distanceFromTarget = 0.4f;
                WorldSpaceCanvasController.moveWithCamera = false;

                if (isMenuOpenedForFirstTime)
                {
                    OnMenuOpenedFirstTime?.Invoke();
                }
                Debug.Log("opening book");

            } else if (menuCommandName.Equals("Next", StringComparison.OrdinalIgnoreCase))
            {
                AutoFlip.FlipRightPage();

            }
            else if (menuCommandName.Equals("Previous", StringComparison.OrdinalIgnoreCase))
            {
                AutoFlip.FlipLeftPage();

            }
            else if (menuCommandName.Equals("close", StringComparison.OrdinalIgnoreCase))
            {
                Spellbook.SetActive(false);
                WorldSpaceCanvasController.moveWithCamera = true;
            }

        }
    }
    public void EnableFirstTimeMenuOpening()
    {
        isMenuOpenedForFirstTime = true;
    }
}
