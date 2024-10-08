using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Here lies enum :(

public class CoreFeatures : MonoBehaviour
{
    // Properties (easy way to access code outside of this script)
    // You can create a public variable to access them in another script, or you can create Properties
    // Inheritance, Encapsulation, Polymorphism, Abstraction
    // Properties are encapsulated and formated as fields
    // Properties have two ACCESSORS
    // GET Accessor returns other encapsulated variables (READ Component)
    // SET Accessor (WRITE Component)
    // Properties are in Pascal - PropertyName

    public bool AudioSFXSourceCreated { get; set; }

    // Audio plays when door opens
    [field: SerializeField]
    public AudioClip AudioClipOnStart { get; set; }
    [field: SerializeField]

    // Audio plays on close
    public AudioClip AudioClipOnEnd { get; set; }

    private AudioSource audioSource;

    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake()
    {
        MakeSFXAudioSource();
    }

    private void MakeSFXAudioSource()
    {
        //if this is equal to null, create it here

        audioSource = GetComponent<AudioSource>();

        //if component doesn't exist, make one!
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        AudioSFXSourceCreated = true;
    }

    protected void PlayOnStart()
    {
        if (AudioSFXSourceCreated && AudioClipOnStart != null)
        {
            audioSource.clip = AudioClipOnStart;
            audioSource.Play();
        }
    }

    protected void PlayOnEnd()
    {
        if (AudioSFXSourceCreated && AudioClipOnEnd != null)
        {
            audioSource.clip = AudioClipOnEnd;
            audioSource.Play();
        }
    }
}
