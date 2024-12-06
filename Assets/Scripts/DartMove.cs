using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class DartMove : MonoBehaviour
{
    public Rigidbody rb;
    public Controller leapController;

    public float defaultThrowForce;
    public float throwForce;
    public float gravityUnit;
    public float gravity;

    public bool isFlying;

    public float pickupDistance;
    public float pinchThresholdPickup;
    public float pinchThresholdRelease;
    public bool isPickedUp;

    public DartGenerator dGenerator;
    public bool useAimMode;
    public bool useAdaptativeForce; // moving the hand too fast makes the leapmotion lost the tracking

    public GameObject handTracker;


    void Start()
    {
        pickupDistance = 0.12f;
        pinchThresholdPickup = 0.5f;
        pinchThresholdRelease = 0.3f;

        isFlying = false;
        isPickedUp = false;

        useAimMode = false;
        useAdaptativeForce = true;
        
        gravityUnit = 1.3f; //1.035f;
        gravity = 7.0f ;//2.5f;
        defaultThrowForce = 110; //55

        leapController = new Controller();
    }

    void Update(){
        Frame frame = leapController.Frame();

        if (frame.Hands.Count > 0){
            Hand hand = frame.Hands[0];


            Vector3 dartPosition = transform.position;
            Finger[] fingers = hand.fingers;
            Finger thumb = hand.fingers[0];
            Vector3 thumbCoordinatesLocal = thumb.TipPosition;
            Vector3 thumbCoordinateGlobal = handTracker.transform.TransformPoint(thumbCoordinatesLocal);
            
            float distance = Vector3.Distance(dartPosition, thumbCoordinateGlobal);

            if (distance < pickupDistance && !isPickedUp && hand.PinchStrength > pinchThresholdPickup && !isFlying) // Pick up the dart
            {
                PickUpDart(hand);
                Debug.Log("Picked  up");
            }
            if (isPickedUp && hand.PinchStrength > pinchThresholdRelease) // Hold the dart
            {
                HoldDart(hand, thumbCoordinateGlobal);
                Debug.Log("Holding the dart.");
            }
            else if (isPickedUp){ // Throw the dart
                
                if (useAdaptativeForce){
                        Vector3 handVelocity = hand.PalmVelocity;
                        throwForce = (Mathf.Abs(handVelocity[0])+Mathf.Abs(handVelocity[1])+Mathf.Abs(handVelocity[2]))   * defaultThrowForce;
                        Debug.Log("throwForce: " + throwForce + "multiplier: " + Mathf.Abs(handVelocity[0])+Mathf.Abs(handVelocity[1])+Mathf.Abs(handVelocity[2]));
                    }
                else{
                    throwForce = defaultThrowForce;
                }
                ReleaseDart();
                Debug.Log("Released the dart.");
            }
        }
        if(isFlying){

            ThrowDart();
        }
    }

    private void PickUpDart(Hand hand)
    {
        isPickedUp = true;
        rb.isKinematic = true; // Disable physics simulation while the dart is being held
    }
    private void HoldDart(Hand hand, Vector3 thumbCoordinates){
        transform.position = thumbCoordinates;
        Quaternion currentRotation = Quaternion.LookRotation(hand.Direction, hand.PalmNormal);

        Quaternion addedRotation = Quaternion.Euler(160, -90, 0); // This vector rotates the dart wrt the hand

        // objects rotation = global rotation of the hand (tracker) * local rotation of the hand * dart rotation
        transform.rotation = handTracker.transform.rotation * currentRotation * addedRotation;
        transform.position = transform.position + (transform.rotation * new Vector3(0, 0, -0.13f)); //new Vector3(0.005f, -0.08f, 0));
    }

    private void ReleaseDart(){
        isPickedUp = false;
        rb.isKinematic = false;
        isFlying = true;
        dGenerator.weCanGenerate = true;
    }

    private void ThrowDart()
    {
        rb.isKinematic = false; 
        gravity = gravity * gravityUnit;

        Vector3 throwDirection;

        if (useAimMode){
            Debug.Log(- transform.up);
            //throwDirection = (- transform.up).normalized * throwForce + gravity * new Vector3(0, -1, 0); //- transform.up is actually the "forward" of the dart
            throwDirection = (- transform.forward).normalized * throwForce + gravity * new Vector3(0, -1, 0); //- transform.up is actually the "forward" of the dart
        }
        else{
            throwDirection = new Vector3(0, 0.20f, -1).normalized * throwForce + gravity * new Vector3(0, -1, 0);
        }
        rb.AddForce(throwDirection);
    }

    public bool IsPickedUp
    {
        get { return isPickedUp; }
    }
}
