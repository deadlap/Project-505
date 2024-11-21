using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField, Min(1)] private int amount = 20;
    [HideInInspector] public GameObject[] fishies;
    [Space(5f), Header("Swim instructions")]
    [SerializeField, Tooltip("Whether the flock swims towards goalTransform")] private bool customGoal = true;
    [SerializeField, Tooltip("What the flock swims towards when customGoal is true")] private Transform goalTransform;
    [HideInInspector] public Vector3 goalPosition = Vector3.zero;
    [Space(5f), Header("Swim area")]
    [SerializeField] private Transform areaCorner1;
    [SerializeField] private Transform areaCorner2;

    [Space(5f), Header("Fishies settings")]
    [SerializeField, Range(0f, 5f)] public float minSpeed = 0.2f;
    [SerializeField, Range(0f, 5f)] public float maxSpeed = 0.7f;
    [SerializeField, Range(0f, 10f), Tooltip("Distance to fish before they start trying to avoid each other")] public float minNeighbourDistance = 1f;
    [SerializeField, Range(0f, 10f), Tooltip("Distance to furthest fish it considers part of its flock")] public float maxNeighbourDistance = 10f;
    [SerializeField, Range(0.1f, 20f), Tooltip("How fast the fish can turn")] public float rotationalSpeed = 2.5f;
    [Header("Behaviour")]
    [SerializeField, Range(0f, 1f), Tooltip("Chance to set a new goal every second. Only applicable if customGoal is true")] private float newGoalChance = 0.1f;
    private float lastReset = 0f;
    [SerializeField, Range(0f, 1f), Tooltip("Chance to reset a given fish's speed every second. Gives more dynamic speeds to the fishes")] public float speedNullChance = 0.4f;
    [SerializeField, Range(0f, 1f), Tooltip("Odds that fish actually run their flocking behaviour. Higher gives more flocking behaviour, but means worse performance")] public float flockChance = 0.1f;
    [SerializeField, Min(0f), Tooltip("How strongly the fish want to swim towards the centre of their pod")] public float gatherStrength = 1f;
    [SerializeField, Min(0f), Tooltip("How strongly the fish want to avoid hitting each other")] public float avoidStrength = 1f;
    [SerializeField, Min(0f), Tooltip("How strongly the fish want to swim towards their goal")] public float goalStrength = 1f;

    [HideInInspector] public Vector3 averagePosition = Vector3.zero;
    [HideInInspector] public Vector3 averageHeading = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        // Instantiate fish
        fishies = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(areaCorner1.position.x, areaCorner2.position.x),
                                      Random.Range(areaCorner1.position.y, areaCorner2.position.y),
                                      Random.Range(areaCorner1.position.z, areaCorner2.position.z));

            fishies[i] = Instantiate(fishPrefab, pos, Quaternion.identity);
            fishies[i].transform.parent = transform;
            if (!fishies[i].TryGetComponent(out Flock f))
            {
                Debug.LogError($"Trying to summon {fishPrefab} as a boid element, but it does not carry a Flock script");
                return;
            }
            f.myManager = this;
        }

        if (goalTransform != null)
            goalPosition = goalTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (customGoal && goalTransform != null)
            goalPosition = goalTransform.position;
        else
            RandomiseGoal();

        //averagePosition = Vector3.zero;
        //averageHeading = Vector3.zero;
        //foreach(GameObject fish in fishies)
        //{
        //    averagePosition += fish.transform.position;
        //    averageHeading += fish.transform.forward;
        //}
        //averagePosition /= fishies.Length;
        //averageHeading /= fishies.Length;
    }

    private void RandomiseGoal()
    {
        if (Time.time < lastReset) return;
        lastReset = Time.time + 1f;
        if (Random.Range(0f, 1f) > newGoalChance) return;

        goalPosition = new Vector3(Random.Range(areaCorner1.position.x, areaCorner2.position.x),
                                   Random.Range(areaCorner1.position.y, areaCorner2.position.y),
                                   Random.Range(areaCorner1.position.z, areaCorner2.position.z));
    }
}
