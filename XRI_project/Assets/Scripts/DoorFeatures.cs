using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorFeatures : CoreFeatures // Inheritance - inherits the CoreFeatures script
{
    [Header("Door Configuration")]

    [SerializeField]
    private Transform doorPivot; // Controls door pivoting -- Encapsulation

    [SerializeField]
    private float maxAngle = 90.0f;

    [SerializeField]
    private bool reverseAngleDirection = false;

    [SerializeField]
    private float doorSpeed = 1.0f;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private bool MakeKinematicOnOpen = false;

    [Header("Interactions Configuration")]

    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        socketInteractor?.selectEntered.AddListener((s) => // Polymorphic
        {
            OpenDoor();
            PlayOnStart();
        });

        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
        });

        // For Dev Testing Only!!! -- DELETE ME
        //OpenDoor();
    }

    public void OpenDoor() // Abstraction
    {
        if (!open)
        {
            PlayOnStart();
            open = true;
            StartCoroutine(ProcessMotion());
        }
    }

    private IEnumerator ProcessMotion()
    {
        // Constantly look to confirm that door is open
        while (open)
        {
            var angle = doorPivot.localEulerAngles.y < 100 ? doorPivot.localEulerAngles.y : doorPivot.localEulerAngles.y - 360;
            angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;
            
            if (angle <= maxAngle)
            {
                doorPivot?.Rotate(Vector3.up, doorSpeed * Time.deltaTime * (reverseAngleDirection ? -1 : 1));
            }

            else
            {
                // When done interacting, turn off Rigidbody
                open = false;
                var featureRigidBody = GetComponent<Rigidbody>();

                if (featureRigidBody != null && MakeKinematicOnOpen) featureRigidBody.isKinematic = true;
            }
        }

        yield return null;
    }
}
