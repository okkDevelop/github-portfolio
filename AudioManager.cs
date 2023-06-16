using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public SFX[] sounds;
	
    // Start is called before the first frame update
    void Start()
    {
        foreach(SFX s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}
				
    }
	
	public void PlaySound(string name)
	{
		foreach(SFX s in sounds)
		{
			if(s.name == name)
				s.source.Play();
		}
	}
	
	public void StopSound(string name)
	{
		foreach (SFX s in sounds)
		{
			if (s.name == name)
			{
				s.source.Stop();
				return;
			}
		}
	}

}
