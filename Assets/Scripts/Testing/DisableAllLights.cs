using UnityEngine;

public class DisableAllLights : MonoBehaviour
{
    public Light[] allLights;
    void Start()
    {
        // Get all Light components in the scene
        allLights = FindObjectsOfType<Light>();

        // Loop through each light and disable them
        foreach (Light light in allLights)
        {
            light.enabled = false;

            if(light.name == "Light for Dartboard")
            {
                light.enabled = true;
            }
        }

        
    }
}

