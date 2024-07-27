using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    //Variable that must be asigned when a fish is spawned
    [HideInInspector] public float speed;
    [HideInInspector] public float rotationSpeed;
    [HideInInspector] public FishBank fatherFishBank;
    [HideInInspector] public int objectiveNumber;

    // Variables to initialize in this script

    private Transform tr;

    private void Start()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = (fatherFishBank.objectives[objectiveNumber] - tr.position).normalized;

        tr.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), rotationSpeed);
        tr.position += tr.forward * speed;
    }
}
