using UnityEngine;
using System.Collections;

public class RoomTransition : MonoBehaviour
{
    public Transform targetRoom; // The position where the player should appear in the next room
    public float transitionDelay = 1f; // Time interval before transitioning the player
    private bool isTransitioning = false;
    private PlayerController playerController; // Reference to the player's control script

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            playerController = other.GetComponent<PlayerController>();
            StartCoroutine(TransitionRoom(other.gameObject));
        }
    }

    IEnumerator TransitionRoom(GameObject player)
    {
        isTransitioning = true;

        // Disable player control
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Start fade out
        FadeController.Instance.FadeOut();

        // Wait for the specified transition delay
        yield return new WaitForSeconds(transitionDelay);

        // Move the player to the target room position
        player.transform.position = targetRoom.position;

        // Wait for the fade out to complete
        yield return new WaitForSeconds(FadeController.Instance.fadeDuration - transitionDelay);

        // Start fade in
        FadeController.Instance.FadeIn();

        // Wait for the fade in to complete
        yield return new WaitForSeconds(FadeController.Instance.fadeDuration);

        // Re-enable player control
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        isTransitioning = false;
    }
}