using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBank : MonoBehaviour
{
    [Header("Individual fish values")]

    [Tooltip("Min speed for individual fishesh")]
    [SerializeField] private float fishSpeedMin = 0.01f;
    [Tooltip("Max speed for individual fishesh")]
    [SerializeField] private float fishSpeedMax = 0.03f;
    
    [Space(4)]

    [Tooltip("Min rotation speed for individual fishesh")]
    [SerializeField] private float fishRotationSpeedMin = 0.7f;
    [Tooltip("Max rotation speed for individual fishesh")]
    [SerializeField] private float fishRotationSpeedMax = 0.9f;

    [Header("Fish bank values")]

    [Tooltip("Number of objectives that the fishes choose to follow")]
    [SerializeField] private int objectivesNumber = 1;

    [Space(4)]

    [Tooltip("Min time that has to pass before an objective is randomized inside the sphere")]
    [SerializeField] private float minRepositionObjectiveTime = 0.5f;
    [Tooltip("Max time that has to pass before an objective is randomized inside the sphere")]
    [SerializeField] private float maxRepositionObjectiveTime = 1f;

    [Space(4)]

    [Tooltip("Allocates the fish gameobject prefab")]
    [SerializeField] private GameObject fishPrefab;

    [Tooltip("Amount of fish to be spawned")]
    [SerializeField] private int fishAmount = 20;



    //Array of timers that controls when an objective should be reallocated
    private float[] timers;

    //array of objectives that fish will follow
    [HideInInspector] public Vector3[] objectives;


    // Variables to initialize in this script

    private Transform tr;


    // Start is called before the first frame update
    void Start()
    {
        tr = transform;

        objectives = new Vector3[objectivesNumber];
        timers = new float[objectivesNumber];

        for (int i = 0; i < objectives.Length; i++)
        {
            RepositionObjective(i);
        }

        for (int i = 0 ; i < timers.Length; i++)
        {
            timers[i] = Random.Range(minRepositionObjectiveTime, maxRepositionObjectiveTime);
        }

        GenerateFishAmount(fishAmount);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < timers.Length; i++)
        {
            timers[i] -= Time.deltaTime;

            if (timers[i] <= 0)
            {
                timers[i] = Random.Range(minRepositionObjectiveTime, maxRepositionObjectiveTime);
                RepositionObjective(i);
            }
        }
    }

    private void RepositionObjective(int index)
    {
        objectives[index] = tr.position + Random.insideUnitSphere * tr.localScale.x / 2;
    }

    private void GenerateFishAmount(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GenerateFish(Random.Range(fishSpeedMin, fishSpeedMax), Random.Range(fishRotationSpeedMin, fishRotationSpeedMax));
        }
    }

    private void GenerateFish(float speed, float rotateSpeed)
    {
        GameObject clon = Instantiate(fishPrefab, tr);
        Transform clonTransform = clon.transform;
        Fish fishScript = clon.GetComponent<Fish>();

        fishScript.speed = speed;
        fishScript.rotationSpeed = rotateSpeed;
        fishScript.fatherFishBank = this;
        fishScript.objectiveNumber = Random.Range(0,objectives.Length - 1);

        clonTransform.rotation = Random.rotation;
        clonTransform.position = tr.position + Random.insideUnitSphere * tr.localScale.x / 2;
    }
}
