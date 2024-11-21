using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [HideInInspector] public FlockManager myManager;
    [HideInInspector] protected float speed;
    private Transform[] fishies = null;
    private float lastReset = 0f;
    private int groupSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);

        // Get all fish for their positions
        List<Transform> allFishes = new List<Transform>();
        foreach (GameObject fish in myManager.fishies)
        {
            if (fish == gameObject) continue;
            allFishes.Add(fish.transform);
        }
        fishies = allFishes.ToArray();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0f, 1f) < myManager.flockChance) ApplyRules();

        if (groupSize < 1) // If swimming solo, make sure not to leave bounds
        {
            Vector3 targetDirection = Vector3.zero;

            // If too high, begin looking down
            if (transform.position.y > myManager.areaCorner2.position.y)
                targetDirection += Vector3.down;
            else if (transform.position.y < myManager.areaCorner1.position.y) // Vice versa, begin looking up
                targetDirection += Vector3.up;

            // If too far right, begin looking left
            if (transform.position.x > myManager.areaCorner2.position.x)
                targetDirection += Vector3.left;
            else if (transform.position.x < myManager.areaCorner1.position.x) // Vice versa, begin looking right
                targetDirection += Vector3.right;

            // If too forward, begin looking back
            if (transform.position.z > myManager.areaCorner2.position.z)
                targetDirection += Vector3.back;
            else if (transform.position.z < myManager.areaCorner1.position.z) // Vice versa, begin looking forward
                targetDirection += Vector3.forward;

            // Turn that direction
            if (targetDirection != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), myManager.rotationalSpeed * Time.deltaTime);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void ApplyRules()
    {
        if (Time.time > lastReset)
        {
            lastReset = Time.time + 1f;
            if (Random.Range(0f, 1f) < myManager.speedNullChance) speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        }

        Vector3 centre = Vector3.zero;
        Vector3 avoid = Vector3.zero;
        Vector3 goal;

        float groupSpeed = 0.01f;
        float neighbourDistance;
        groupSize = 0;

        foreach (Transform fish in fishies)
        {
            neighbourDistance = Vector3.Distance(fish.position, transform.position);

            if (neighbourDistance > myManager.maxNeighbourDistance) continue; // If fish is too far, ignore it

            groupSize++; // Know the group size
            
            centre += fish.position; // Get centre

            if (neighbourDistance < myManager.minNeighbourDistance) avoid += transform.position - fish.position; // Know to avoid fish if too close

            groupSpeed += fish.GetComponent<Flock>().speed;
        }

        if (groupSize < 1) return;

        centre /= groupSize;
        goal = myManager.goalPosition - transform.position;
        groupSpeed /= groupSize;

        centre -= -transform.position;
        centre *= myManager.gatherStrength;
        avoid -= transform.position;
        avoid *= myManager.avoidStrength;
        goal -= transform.position;
        goal *= myManager.goalStrength;

        Vector3 targetDirection = (centre + goal + avoid);

        if (!targetDirection.Equals(Vector3.zero))
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), myManager.rotationalSpeed * Time.deltaTime);
        speed = Mathf.Clamp(groupSpeed, myManager.minSpeed, myManager.maxSpeed);
    }
}
