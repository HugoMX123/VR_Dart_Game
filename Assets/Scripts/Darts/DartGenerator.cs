using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class DartGenerator : MonoBehaviour
{
    public Rigidbody rb;
    public Controller leapController;
    public GameObject dartPrefab;

    public float pickupDistance;
    public float pinchThresholdPickup;
    private bool generatedDartPicked;
    public bool weCanGenerate;
    private GameObject lastGeneratedDart;
    public GameObject handTracker;
    public GameObject debug;

    public static bool useAimMode;
    public static bool useAdaptativeForce; // moving the hand too fast makes the leapmotion lost the tracking

    void Start()
    {
        pickupDistance = 0.12f;
        pinchThresholdPickup = 0.5f;

        generatedDartPicked = false;
        weCanGenerate = true;

        leapController = new Controller();
    }

    void Update(){
        Frame frame = leapController.Frame();
        if (frame.Hands.Count > 0){
            Hand hand = frame.Hands[0];

            Vector3 generatorPosition = transform.position;
            Finger[] fingers = hand.fingers;
            Finger thumb = hand.fingers[0];
            Vector3 thumbCoordinatesLocal = thumb.TipPosition;
            Vector3 thumbCoordinateGlobal = handTracker.transform.TransformPoint(thumbCoordinatesLocal);
            
            float distance = Vector3.Distance(generatorPosition, thumbCoordinateGlobal);

            debug.transform.position = thumbCoordinateGlobal;

            //Debug.Log("Distance: " + distance);

            if (distance < pickupDistance && hand.PinchStrength > pinchThresholdPickup && weCanGenerate){
                weCanGenerate = false;
                GenerateNewDart();
            }
        }
    }

    void GenerateNewDart(){
        GameObject newDart = Instantiate(dartPrefab, transform.position, Quaternion.identity);
        DartMove dm = newDart.GetComponent<DartMove>();
        dm.dGenerator = this;
        dm.handTracker = handTracker;
        dm.useAimMode = useAimMode;
        dm.useAdaptativeForce = useAdaptativeForce;
        lastGeneratedDart = newDart;
        Debug.Log("Generated new dart");
    }
}
