using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionHandler : MonoBehaviour {

    public GameObject player;
    public PlayerController playerController;
    public ParticleSystem particles;
    private bool isEnabled = false;
    public GameObject keyPart;
    Material[] m_Material;
    Color green = Color.green;

    // Use this for initialization
    void Start () {
        playerController = player.GetComponent<PlayerController>();
        particles.Pause();
        m_Material = GetComponent<Renderer>().materials;
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerInventory.hasThirdKeyPart && !isEnabled) {
            isEnabled = true;
            m_Material[2].color = green;
            m_Material[4].color = green;
            m_Material[5].color = green;
        }
	}

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player") && isEnabled) {
            particles.Play();
            StartCoroutine("Transition");
            playerController.rb.velocity = new Vector3(0, 1.5f, 0);
        }
    }

    IEnumerator Transition() {
        yield return new WaitForSeconds(4);
        print("Change scene");
    }
}
