using UnityEngine;

public class DivingBellAnimatorEvents : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject tentacles;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        tentacles.SetActive(false);
    }

    void OnEnable()
    {
        GameEventManager.EndEvent += CloseHatch;
        GameEventManager.CompleteGaugeEvent += MessyDivingBell;
    }

    void OnDisable()
    {
        GameEventManager.EndEvent -= CloseHatch;
        GameEventManager.CompleteGaugeEvent -= MessyDivingBell;
    }

    void CloseHatch()
    {
        animator.SetBool("CloseHatch", true);
    }

    void MessyDivingBell()
    {
        animator.SetBool("MessyDivingBell", true);        
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
