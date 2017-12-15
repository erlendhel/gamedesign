using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoButtonHandler : MonoBehaviour {

    private Animator anim;
    public UpstreamHandler upstreamHandler;
    public ParticleSystem upstreamParticles;

    private void Start() {
        anim = GetComponent<Animator>();
        upstreamParticles.Stop();
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("LavaRock")) {
            anim.Play("LavaButtonPlay");
            upstreamParticles.Play();
            upstreamHandler.yForce = 3500.0f;
        }
    }

}
