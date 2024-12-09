using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerAnimation : MonoBehaviour
{

    public Animator animator;
    public AIDartThrower aiDartThrower;

    private void Start()
    {
        // Get the animator component
        animator = GetComponent<Animator>();
        AIDartThrower.OnAIDartThrown += triggerAnimation;
    }

    private void Update() {

        // Check for a press of the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Trigger the throwing animation
            animator.SetTrigger("isThrowing");
        }
        
    }

    public void triggerAnimation()
    {
        animator.SetTrigger("isThrowing");
    }

}
