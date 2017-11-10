using UnityEngine.Audio;
using UnityEngine;


/**
 * Wrapper class for different Unity sound features. 
 * AudioMananger holds an array of this class used to keep track of all sounds i game,
 * as well as easily adding new ones and adjusting variables, e.g. volume.
 * Serializable so it can be seen in the Unity inspector 
 */
[System.Serializable] 
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
