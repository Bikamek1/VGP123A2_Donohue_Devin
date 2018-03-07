using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    // This script keeps track of the health the game should display.
    // Note: This should exist on the Canvas object in the scene.
public class HealthHUD : MonoBehaviour
{

    // The heart object that will be displayed for each point of health.
    public GameObject heartPrefab;

    // The size of the heart prefab's width for proper separation of multiple instances of it.
    private float heartImageWidth;

    // The current health of the player that should be displayed.
    private int currentHealth;

    // A list of created hearts that are displayed in the HUD.
    private List<GameObject> displayedHearts;


    // When the script first starts
    void Start()
    {
        currentHealth = 0;
        heartImageWidth = heartPrefab.GetComponent<RectTransform>().rect.width;
        displayedHearts = new List<GameObject>();

        // Start the HUD off with the default currentHealth value.
        ChangeDisplayedHealth(5);
    }

    // Call this from any object that wants to change the displayed health.
    // Example: Character object calls this to tell the HUD about it taking damage or being healed.
    public void ChangeDisplayedHealth(int updatedHealth)
    {
        // Display a heart for each point of health the player now has.

        if (currentHealth < updatedHealth)
        {
            // Add hearts.
            for (int i = currentHealth; i < updatedHealth; ++i)
            {
                // Create a new heart.
                displayedHearts.Add(Instantiate(heartPrefab));
                // Make it a child of the canvas that this script should be attached to.
                displayedHearts[displayedHearts.Count - 1].transform.SetParent(this.gameObject.transform, false);
                // Moves the newly created heart to a position on the HUD to the right of all previous hearts.
                displayedHearts[displayedHearts.Count - 1].GetComponent<RectTransform>().anchoredPosition = new Vector3((i * heartImageWidth) + (heartImageWidth / 2), -15.0f, 0.0f);
            }
        }
        else
        {
            // Subtract hearts.
            for (int i = currentHealth; i > updatedHealth; --i)
            {
                // Destroy the heart's gameobject so it no longer exists in the HUD.
                Destroy(displayedHearts[displayedHearts.Count - 1]);
                // Remove the heart from the list of hearts.
                displayedHearts.RemoveAt(displayedHearts.Count - 1);
            }
        }

        // The current health is set to the newly given value.
        currentHealth = updatedHealth;
    }

    // Add one to the currently displayed HP.
    public void AddDisplayedHealth()
    {
        ChangeDisplayedHealth(currentHealth + 1);
    }

    // Subtract one to the currently displayed HP.
    public void SubtractDisplayedHealth()
    {
        ChangeDisplayedHealth(currentHealth - 1);
    }
}



