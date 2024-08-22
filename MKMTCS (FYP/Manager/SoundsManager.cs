using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static SoundsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        foreach (Sounds s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.loop = s.loop;
            s.audioSource.volume = s.volume = 1;
        }
    }

    public void PlaySound(string name)
    {
        foreach (Sounds s in sounds)
        {
            if (s.audioName == name)
                s.audioSource.Play();
        }
    }

    public void StopSound(string name) 
    {
        foreach (Sounds s in sounds)
        {
            if (s.audioName == name)
                s.audioSource.Stop();
        }
    }
}
