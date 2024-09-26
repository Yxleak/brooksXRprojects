using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorFeatures : CoreFeatures // Inherits the CoreFeatures script
{
    [Header("Door Configuration")]

    [SerializeField]
    private Transform doorPivot; // Controls door pivoting

    [SerializeField]
    private float maxAngle = 90.0f;

    [SerializeField]
    private bool reverseAngleDirection = false;

    [SerializeField]
    private float doorSpeed = 1.5f;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private bool MakeKinematicOnOpen = false;

    [Header("Interactions Configuration")]

    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        socketInteractor?.selectEntered.AddListener((s) =>
        {
            //OpenDoor();
        });
    }
}
