using DefaultCompany.Singleton; //Make sure namespace matches company name in Project Settings - Player
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("UI Options")]
    [SerializeField]
    private float offsetPositionFromPlayer = 1.0f; // How far to place from the camera

    [SerializeField]
    private GameObject menuContainer; // Canvas with a couple of buttons

    private const string GAME_SCENE_NAME = "Game"; // Allows us to restart the game

    [Header("Events")]
    public Action onGameResumedAction; // Notifies other classes like GameManager to update

    private MainMenu menu;

    public void Awake()
    {
        // Bind the Menu Buttons
        menu = menuContainer.GetComponentInChildren<MainMenu>(true); // Added 'true' just in case any part of menu is hidden

        menu.ResumeButton.onClick.AddListener(() =>
        {
            HandleMenuOptions(GameState.Playing);
            onGameResumedAction?.Invoke();
        });

        menu.RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
            onGameResumedAction?.Invoke();
        });
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void HandleMenuOptions(GameState gameState) // Absolutely has to match enum values
    {
        if (gameState == GameState.Paused)
        {
            menuContainer.SetActive(true);
            //PlaceMenuInFront();
        }

        else if (gameState == GameState.ChallengeSolved)
        {
            menuContainer.SetActive(true);
            menu.ResumeButton.gameObject.SetActive(false);
            menu.SolvedText.gameObject.SetActive(true);
            //PlaceMenuInFront();
        }

        else
        {
            menuContainer.SetActive(false); // Don't want menu to appear while playing ;)
        }
    }

    private void PlaceMenyInFront()
    {
        // Places UI in front of player
        var playerHead = Camera.main.transform; // playerHead is location of the XR camera
        menuContainer.transform.position = playerHead.position + (playerHead.forward * offsetPositionFromPlayer);
        menuContainer.transform.rotation = playerHead.rotation;
    }
}
