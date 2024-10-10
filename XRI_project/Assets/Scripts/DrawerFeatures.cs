using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerFeatures : CoreFeatures
{
    [Header("Drawer Configuration")]

    [SerializeField]
    private Transform drawerSlide;

    [SerializeField]
    private float maxDistance = 1.0f;

    [SerializeField]
    private FeatureDirection featureDirection = FeatureDirection.Forward;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private Vector3 initialPosition; //Store initial position of our drawer

    //Restrict drawer position using clamps
    private float drawerMinLimit;
    private float drawerMaxLimit;

    void Start()
    {
        //Save the initial position of drawer at start
        initialPosition = drawerSlide.localPosition;

        //Find the drawer min and max limits based on initialPosition and maxDistance
        if (featureDirection == FeatureDirection.Forward)
        {
            drawerMinLimit = initialPosition.z;
            drawerMaxLimit = initialPosition.z + maxDistance;
        }

        else
        {
            drawerMinLimit = initialPosition.z - maxDistance;
            drawerMaxLimit = initialPosition.z;
        }

        //drawer with simple interactable
        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            //if drawer isn't open, open it ya dunce
            if (!open)
            {
                OpenDrawer();
            }
        });
    }

    private void OpenDrawer()
    {
        open = true;
        PlayOnStart();
        StopAllCoroutines();
        StartCoroutine(ProcessMotion());
    }

    private void CloseDrawer()
    {
        open = false;
        PlayOnEnd();
        StopAllCoroutines();
        StartCoroutine (ProcessMotion());
    }

    private IEnumerator ProcessMotion()
    {
        //Open drawer to maxDistance or close initialPosition based on "open" bool
        Vector3 targetPosition = open ? new Vector3(drawerSlide.localPosition.x, drawerSlide.localPosition.y, drawerMaxLimit) : initialPosition;

        while (drawerSlide.localPosition != targetPosition)
        {
            drawerSlide.localPosition = Vector3.MoveTowards(drawerSlide.localPosition, targetPosition, Time.deltaTime * speed);

            //Ensure drawer stays within defined limits
            float clampedZ = Mathf.Clamp(drawerSlide.localPosition.z, drawerMinLimit, drawerMaxLimit);

            drawerSlide.localPosition = new Vector3(drawerSlide.localPosition.x, drawerSlide.localPosition.y, clampedZ);

            yield return null;
        }
    }
}
