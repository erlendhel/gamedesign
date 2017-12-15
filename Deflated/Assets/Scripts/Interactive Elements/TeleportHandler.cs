using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour {

    public GameObject player; 
    public PlayerController playerController;
    public ParticleSystem particles;
    private bool isEnabled = false;
    public ButtonCombinationHandler buttonCombination;    

    void Start() {
        playerController = player.GetComponent<PlayerController>();
        particles = GetComponentInChildren<ParticleSystem>();
        buttonCombination = GetComponent<ButtonCombinationHandler>();
        particles.Pause();
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Player") && !isEnabled && buttonCombination.isUnlocked) {
            particles.Play();
            StartCoroutine("TeleTimer");
        }
    }

    IEnumerator TeleTimer() {
        isEnabled = true;
        playerController.teleAnim.enabled = true;
        playerController.teleAnim.Play("PlayerTele");
        yield return new WaitForSeconds(5);
        playerController.teleAnim.enabled = false;
        playerController.rb.velocity = new Vector3(0, 0, 0);
        playerController.rb.transform.position = new Vector3(170.4794f, 577.6711f, -211.5278f);
        isEnabled = false;
        particles.Clear();
    }
}
