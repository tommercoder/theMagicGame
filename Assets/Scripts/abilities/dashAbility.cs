using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Collections;
public class dashAbility : AbilityMain
{
    
    [Header("Connections")]
    [SerializeField] private Animator animator = default;
    [SerializeField] private CinemachineFreeLook originalCam = default;
   // [SerializeField] private MeleeWeapon weapon = default;
   // [SerializeField] private Damageable damageable = default;
    [Header("Visuals")]
    [SerializeField] private Renderer skinnedMesh = default;
    [SerializeField] private ParticleSystem dashParticle = default;
 
    public CharacterController controller;
    attacksController attacksController;
    public AbilityUI AbilityUI;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        attacksController = GetComponent<attacksController>();
        
    }
    //[SerializeField] private Volume dashVolume = defau lt;
    public override void Ability()
    {
        //DOTween.Init();


        dashParticle.Play();
        if (!attacksController.enemiesAround)
        {
            controller.enabled = false;
            animator.SetInteger("attackAnimation", 20);
            transform.DOMove(transform.position + (transform.forward * 5), .2f);
            //.AppendCallback(() => dashParticle.Stop());
            //transform.DOMove(transform.position + (transform.forward * 5), .2f);
           // DOVirtual.Float(0, 1, .1f, SetDashVolumeWeight)
          // .OnComplete(() => DOVirtual.Float(1, 0, .5f, SetDashVolumeWeight));

            DOVirtual.Float(40, 50, .1f, SetCameraFOV)
                .OnComplete(() => DOVirtual.Float(50, 40, .5f, SetCameraFOV));
        }
        else
        {
            controller.enabled = false;
            transform.DOMove(transform.position + (transform.forward * 5), .2f);
           //.AppendCallback(() => dashParticle.Stop());
            StartCoroutine(checker());
        }

        AbilityUI.ShowCoolDown(cooldownTime);
        dashParticle.Stop();





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
