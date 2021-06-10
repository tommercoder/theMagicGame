using UnityEngine;
using DG.Tweening;
using Cinemachine;
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
    public AbilityUI AbilityUI;
    public  bool hitHitted = false;

    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        
    }
    //ta funkcja służy do sprawdzenia czy może być ta zdolność uruchomiona,jeśli przed bohaterem jest jakiś objekt, to ta zdolność nie uruchomi się
    bool checkIfDashCanBeCasted()
    {
        RaycastHit hit;
        //pród od bohatera 
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 8;
        //Raycast tworzy linię która na swoim końcu sprawdza czy ma jakiś collider,trigger itd
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
        //włącząc Gizmosc na scenie czy w grze można zobaczyć jak te Raycasty wyglądają
        Debug.DrawRay(transform.position + Vector3.up * 2, forward, Color.red);
        Debug.DrawRay(transform.position + Vector3.up , forward, Color.red);
        Debug.DrawRay(transform.position, forward, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * 2.5f, forward, Color.blue);
        Debug.DrawRay(transform.position + Vector3.up*1.5f, forward, Color.blue);
    }

    public override void Ability()
    {
        
        //Debug.Log("checkIfDashCanBeCasted(); " + checkIfDashCanBeCasted());
        if (checkIfDashCanBeCasted())
        {
            dashStarted = true;
            //wyłączam CharacterContoller dla możliwości poruszania się gracza za pomocą DoTween    
            controller.enabled = false;
            animator.SetInteger("attackAnimation", 20);
            transform.DOMove(transform.position + (transform.forward * 5), .2f);

            //powoli porusza kamerę po tyłu i przodu za pomocą funkcji SetCameraFov
            DOVirtual.Float(40, 55, .1f, SetCameraFOV)
                .OnComplete(() => DOVirtual.Float(55, 40, .5f, SetCameraFOV));
            

            AbilityUI.ShowCoolDown(cooldownTime);
            dashParticle.Stop();
            abilityDone = true;
        }
        else
        {
            dashStarted = false;
            abilityDone = false;
          
        }

    }
   
    public void dashEndedEvent()//animation event na końcu animacji
    {
        controller.enabled = true;
        
        
    }

    void SetCameraFOV(float fov)
    {
        originalCam.m_Lens.FieldOfView = fov;
    }
   

}
