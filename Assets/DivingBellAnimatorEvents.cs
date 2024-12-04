using UnityEngine;

public class DivingBellAnimatorEvents : MonoBehaviour
{
    Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        GameEventManager.EndEvent += CloseHatch;
        GameEventManager.BrokenEvent += MessyDivingBell;
    }

    void OnDisable()
    {
        GameEventManager.EndEvent -= CloseHatch;
        GameEventManager.BrokenEvent -= MessyDivingBell;
    }

    void CloseHatch()
    {
        animator.SetBool("CloseHatch", true);
    }

    void MessyDivingBell()
    {
        animator.SetBool("MessyDivingBell", true);        
    }
}
