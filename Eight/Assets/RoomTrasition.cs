using UnityEngine;
using System.Collections;

public class RoomTransition : MonoBehaviour
{
    public Transform targetRoom; // The position where the player should appear in the next room
    public float transitionDelay = 1f; // Time interval before transitioning the player
    private bool isTransitioning = false;
    private PlayerMovement playerMovement; // Reference to the player's movement script

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered by: {other.gameObject.name}");

        if (other.CompareTag("Player") && !isTransitioning)
        {
            playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                Debug.Log("PlayerMovement found. Starting transition.");
                StartCoroutine(TransitionRoom(other.gameObject));
            }
            else
            {
                Debug.LogWarning("PlayerMovement not found on the Player object.");
                DebugComponents(other.gameObject);
            }
        }
    }

    IEnumerator TransitionRoom(GameObject player) 
    {
        isTransitioning = true;

        // Disable player movement input
        playerMovement.DisableInput();

        // Start fade out
        FadeController.Instance.FadeOut();

        // Wait for the specified transition delay
        yield return new WaitForSeconds(transitionDelay);

        // Move the player to the target room position
        player.transform.position = targetRoom.position;

        // Ensure proper wait times
        float remainingFadeTime = Mathf.Max(0, FadeController.Instance.fadeDuration - transitionDelay);

        // Wait for the fade out to complete
        yield return new WaitForSeconds(remainingFadeTime);

        // Start fade in
        FadeController.Instance.FadeIn();

        // Wait for the fade in to complete
        yield return new WaitForSeconds(FadeController.Instance.fadeDuration);

        // Re-enable player movement input
        playerMovement.EnableInput();

        isTransitioning = false;
    }

    void DebugComponents(GameObject obj)
    {
        Debug.Log($"Components on {obj.name}:");
        foreach (var component in obj.GetComponents<Component>())
        {
            Debug.Log(component.GetType());
        }
    }
}
