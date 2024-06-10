using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBoss : MonoBehaviour
{
    public GameObject endGamePanel; // Reference to the end game panel
    private GameObject player; // Reference to the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object by tag
    }

    void Update()
    {
        // Check if the Boss is destroyed
        if (GameObject.FindGameObjectWithTag("Boss") == null && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if (player != null)
        {
            Destroy(player); // Destroy the player
        }

        Time.timeScale = 0; // Pause the game
        endGamePanel.SetActive(true); // Display the end game panel
    }
}
