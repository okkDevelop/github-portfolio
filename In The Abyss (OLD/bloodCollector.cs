using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodCollector : MonoBehaviour
{
	ParticleSystem particle;
	List<ParticleSystem.Particle> particlesCollection = new List<ParticleSystem.Particle>();
	PlayerHealth playerHealth;
	
	private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        playerHealth = FindObjectOfType<PlayerHealth>(); // Find the PlayerHealth component in the scene
    }

    private void OnParticleTrigger()
    {    
        int trigger = particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesCollection);

        for (int i = 0; i < trigger; i++)
        {
            ParticleSystem.Particle p = particlesCollection[i];
            p.remainingLifetime = 0;
            Debug.Log("We collected 1 particle");
            playerHealth.IncreaseBlood(0.23f); // Call the IncreaseBlood function from the PlayerHealth component
            particlesCollection[i] = p;
        }

        particle.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesCollection);
    }
	
}
