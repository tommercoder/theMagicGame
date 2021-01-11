using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCharacter : MonoBehaviour
{
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Camera m_Camera;
    private Vector3 m_GroundNormal;
    private bool IsGrounded = false;
    private float m_GroundCheckDistance = 0.3f;

    [SerializeField] private float m_TurnSpeed = 220f;
    [SerializeField] private float m_DistanceCheck = 0.3f;
    [SerializeField] private UnityEvent OnGrounded, OnJump;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Camera = Camera.main;
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (IsGrounded)
            HandleGrounded();
        else
            ExtraGravityOnAir();

        Move();

        m_Animator.SetFloat("vSpeed", m_Rigidbody.velocity.y, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Grounded", IsGrounded);
    }

    private void Update()
    {
        // Perform Jump
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Vector3 velocity = m_Rigidbody.velocity;
            velocity.y = 8f;

            IsGrounded = false;
            m_GroundCheckDistance = 0;
            m_Rigidbody.velocity = velocity;
            OnJump.Invoke();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(m_Camera.transform.forward, new Vector3(1, 0, 1));
        Vector3 move = cameraForward * vertical + m_Camera.transform.right * horizontal;

        if (move.magnitude > 1f)
            move.Normalize();

        move = transform.InverseTransformDirection(move);
        float forward = move.z;
        float turn = Mathf.Atan2(move.x, move.z);

        if (Input.GetKey(KeyCode.LeftShift))
            forward = Mathf.Clamp(forward, 0, 0.5f);

        m_Animator.SetFloat("Forward", forward, 0.1f, Time.fixedDeltaTime);
        m_Animator.SetFloat("Turn", turn, 0.1f, Time.fixedDeltaTime);

        RotateToDirection(turn);
    }

    /// <summary>
    /// Rotates the character to direction of movement
    /// </summary>
    void RotateToDirection(float turnAmount)
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        transform.Rotate(0, m_TurnSpeed * turnAmount * Time.deltaTime, 0);
    }

    private void OnAnimatorMove()
    {
        if (Mathf.Approximately(Time.deltaTime, 0) || !IsGrounded)
            return;

        Vector3 velocity = m_Animator.deltaPosition / Time.deltaTime;
        velocity.y = m_Rigidbody.velocity.y;

        m_Rigidbody.velocity = velocity;
    }

    /// <summary>
    /// Checks ground bellow character
    /// </summary>
    private void CheckGround()
    {
        RaycastHit m_GroundHit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out m_GroundHit, m_GroundCheckDistance, Physics.AllLayers))
        {

            if (!IsGrounded)
                OnGrounded.Invoke();

            IsGrounded = true;
            m_GroundNormal = m_GroundHit.normal;
            return;
        }

        IsGrounded = false;
        m_GroundNormal = Vector3.up;
    }


    /// <summary>
    /// Method to make gravity more realistic on Jump
    /// </summary>
    void ExtraGravityOnAir()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * 2) - Physics.gravity;
        m_Rigidbody.AddForce(extraGravityForce * m_Rigidbody.mass);

        m_GroundCheckDistance = m_Rigidbody.velocity.y < 2 ? m_DistanceCheck : 0.01f; // change ground distance to allow Jump
    }




    /// <summary>
    /// Method that restrict rigidbody velocity to avoid character goes up during a move
    /// </summary>
    void HandleGrounded()
    {
        Vector3 vel = m_Rigidbody.velocity;
        vel.y = Mathf.Clamp(vel.y, -50, 0); // Avoid character goes up

        m_Rigidbody.velocity = vel;
    }
}
