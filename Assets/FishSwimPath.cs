using System;
using UnityEngine;

public class FishSwimPath : MonoBehaviour
{
    //Rigidbody rb;
    [SerializeField] bool canFishMove;
    [SerializeField] bool directMovement;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform[] movePoints;
    int target;
    void Start()
    {
        transform.position = movePoints[0].position;
        //rb = GetComponent<Rigidbody>();
        target = 0;
    }

    void Update()
    {
        if (!canFishMove) return;
        var relativePos = movePoints[target].position - transform.position;
        var targetRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if(directMovement)
            transform.position = Vector3.MoveTowards(transform.position, movePoints[target].position, moveSpeed * Time.deltaTime);
        else
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        print(target);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FishPoint"))
        {
            print("collider");
            if (target >= movePoints.Length - 1)
            {
                target = 0;
            }
            else
            {
                target++;
            }
        }
    }
}
