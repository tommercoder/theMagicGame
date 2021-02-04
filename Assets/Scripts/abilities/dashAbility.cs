﻿using UnityEngine;
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
   // [SerializeField] private MeleeWeapon weapon = default;
   // [SerializeField] private Damageable damageable = default;
    [Header("Visuals")]
    //[SerializeField] private Renderer skinnedMesh = default;
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
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 5;
        if (Physics.Raycast(transform.position+Vector3.up*2,forward,out hit,5,groundMask))
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
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 3;
        Debug.DrawRay(transform.position + Vector3.up * 2, forward, Color.red);
    }
    //[SerializeField] private Volume dashVolume = default;
    public override void Ability()
    {
        //DOTween.Init();
        Debug.Log("checkIfDashCanBeCasted(); " + checkIfDashCanBeCasted());
        if (checkIfDashCanBeCasted())
        {
            //dashParticle.Play();
            //if (!StateControllerTest.instance.enemiesAround)
            //{
            controller.enabled = false;
            animator.SetInteger("attackAnimation", 20);
            transform.DOMove(transform.position + (transform.forward * 5), .2f);


            DOVirtual.Float(40, 50, .1f, SetCameraFOV)
                .OnComplete(() => DOVirtual.Float(50, 40, .5f, SetCameraFOV));
            //}
            //else
            //{
            //    controller.enabled = false;
            //    transform.DOMove(transform.position + (transform.forward * 5), .2f);
            //    .AppendCallback(() => dashParticle.Stop());
            //    StartCoroutine(checker());
            //}

            AbilityUI.ShowCoolDown(cooldownTime);
            dashParticle.Stop();
            abilityDone = true;
        }
        else
        {
            abilityDone = false;
            //attacksController.instanceA.dashParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            //dashParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            Debug.Log("RETURN");
           
           // return;//should i keep it????
        }





    }
    IEnumerator checker()
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true; 
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
