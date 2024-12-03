using System;
using UnityEngine;

public class FishSwimPath : MonoBehaviour
{
    //Rigidbody rb;
    MeshRenderer meshRenderer;
    [SerializeField] bool canFishMove;
    [SerializeField] bool directMovement;
    [SerializeField] float animationSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform[] movePoints;
    int target;
    
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        if(movePoints == null) return;
        //transform.position = movePoints[0].position;
        target = 0;
    }

    void Update()
    {
        if (!canFishMove) return;
        meshRenderer.material.SetFloat("_WaveSpeed", animationSpeed);
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
