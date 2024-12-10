using UnityEngine;
using TMPro;

public class ModeSelector : MonoBehaviour
{
    public TextMeshPro aimModeText;
    public TextMeshPro forceModeText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))  // If the '1' key is pressed
        {
            DartGenerator.useAimMode = !DartGenerator.useAimMode;
            
            if (DartGenerator.useAimMode){
                aimModeText.text = "Manual";
            }
            else {
                aimModeText.text = "Automatic";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))  // If the '2' key is pressed
        {
            DartGenerator.useAdaptativeForce = !DartGenerator.useAdaptativeForce;
            
            if (DartGenerator.useAdaptativeForce){
                forceModeText.text = "Constant";
            }
            else {
                forceModeText.text = "Automatic";
            }
        }
    }
}
