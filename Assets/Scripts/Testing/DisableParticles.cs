using UnityEngine;

public class DisableParticles : MonoBehaviour
{
    public ParticleSystem[] allParticles;
    void Start()
    {
        // Get all Particle System components in the scene
        allParticles = FindObjectsOfType<ParticleSystem>();

        // Loop through each particle system and stop them
        foreach (ParticleSystem particle in allParticles)
        {
            particle.Stop();
        }
    }
}
