using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Collections;
public class dashAbility : AbilityMain
{
    #region Singleton
    public static dashAbility instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    [Header("Connections")]
    [SerializeField] private Animator animator = default;
    [SerializeField] public Transform player;
    [SerializeField] private CinemachineFreeLook originalCam = default;
    public LayerMask groundMask;
   
    [Header("Visuals")]
    
    [SerializeField] private ParticleSystem dashParticle = default;
 
    public CharacterController controller;
    attacksController attacksController;
    public AbilityUI AbilityUI;
    public  bool hitHitted = false;

    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        attacksController = GetComponent<attacksController>();
        
    }
    bool checkIfDashCanBeCasted()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 8;
        if (Physics.Raycast(transform.position+Vector3.up,forward,out hit,8,groundMask) 
            || Physics.Raycast(transform.position + Vector3.up*2, forward, out hit, 8, groundMask)
            || Physics.Raycast(transform.position, forward, out hit, 8, groundMask)
            || Physics.Raycast(transform.position + Vector3.up * 1.5f, forward, out hit, 8, groundMask)
            || Physics.Raycast(transform.position + Vector3.up * 2.5f, forward, out hit, 8, groundMask))
        {
            if (hit.collider != null)
            {
                    
                    return false;
                
            }
        }
       
       
        return true;
    }
    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 8;
        Debug.DrawRay(transform.position + Vector3.up * 2, forward, Color.red);
        Debug.DrawRay(transform.position + Vector3.up , forward, Color.red);
        Debug.DrawRay(transform.position, forward, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * 2.5f, forward, Color.blue);
        Debug.DrawRay(transform.position + Vector3.up*1.5f, forward, Color.blue);
    }

    public override void Ability()
    {
        
        Debug.Log("checkIfDashCanBeCasted(); " + checkIfDashCanBeCasted());
        if (checkIfDashCanBeCasted())
        {
            dashStarted = true;
            
            controller.enabled = false;
            animator.SetInteger("attackAnimation", 20);
            transform.DOMove(transform.position + (transform.forward * 5), .2f);


            DOVirtual.Float(40, 50, .1f, SetCameraFOV)
                .OnComplete(() => DOVirtual.Float(50, 40, .5f, SetCameraFOV));
            

            AbilityUI.ShowCoolDown(cooldownTime);
            dashParticle.Stop();
            abilityDone = true;
        }
        else
        {
            dashStarted = false;
            abilityDone = false;
           
            
            
            Debug.Log("RETURN" + attacksController.instanceA.dashParticle.isStopped);
           
           
        }





    }
   
    public void dashEndedEvent()//animation event
    {
        controller.enabled = true;
        
        
    }
    

    void SetCameraFOV(float fov)
    {
        originalCam.m_Lens.FieldOfView = fov;
    }
   

}
