using UnityEngine;

namespace DiasGames.FootPlacementIK
{
    public class FootIK : MonoBehaviour
    {
        #region Exposed parameters

        [Tooltip("Maximum height that foot can be placed calculated from character origin")]
        [SerializeField] private float m_MaxFootHeight = 0.75f;

        [Tooltip("Minimum height that foot can be placed calculated from character origin. Value must be negative")]
        [SerializeField] private float m_MinFootHeight = -0.5f;

        [Tooltip("Layer that character can put feet")]
        [SerializeField] private LayerMask m_GroundLayerMask = (1 << 0);

        [Tooltip("Height offset from ground that feet must be placed")]
        [SerializeField] private float offSetFromGround = 0f;

        [Tooltip("Time to adjust foot position in seconds")]
        [Range(0, 0.5f)] [SerializeField] private float footAdjustSpeed = 0.05f;

        [Tooltip("Time to adjust body position in seconds")]
        [Range(0, 0.5f)] [SerializeField] private float bodyAdjustSpeed = 0.1f;

        public bool DebugRaycast = true;

        #endregion

        // Components
        private Animator m_Animator;                                                 // Get animator component

        // Feet internal parameters
        private Vector3 m_RightFootPosition, m_LeftFootPosition;                    // Current feet position
        private Vector3 m_LeftFootIKPosition, m_RightFootIKPosition;                // Desired feet IK position
        private Quaternion m_LeftFootIKRotation, m_RightFootIKRotation;             // Desired feet IK rot
        private float m_RightFootOffset, m_LeftFootOffset;                          // Vertical offset for both feet
        private Vector3 m_LeftGroundNormal = Vector3.up;
        private Vector3 m_RightGroundNormal = Vector3.up;

        // Body internal parameters
        private Vector3 m_BodySmoothDampVelocity;                                   // Velocity from SmoothDamp method for body
        private Vector3 m_BodyOffset;                                               // Body offset that should be applied

        // Control parameters
        private bool m_EnabledFootIK = true;                                        // Control wheter Foot IK should run or not
        private float m_BodyWeight = 0f;                                            // Body weight for adjustment
        private float m_FeetWeight = 0f;                                            // Feet weight for adjustment

        private void Start()
        {
            m_Animator = GetComponent<Animator>();

            if (m_Animator == null)
                Debug.LogError("There is no animator attached to " + transform.name + " game object. Please, add an animator to allow foot placement works.");

        }

        private void LateUpdate()
        {
            float bodyWeightVel = 0;
            float feetWeightVel = 0;
            if (m_EnabledFootIK)
            {
                m_BodyWeight = Mathf.SmoothDamp(m_BodyWeight, 1, ref bodyWeightVel, Time.deltaTime);
                m_FeetWeight = Mathf.SmoothDamp(m_BodyWeight, 1, ref feetWeightVel, Time.deltaTime);

                // Adjust start position of cast
                AdjustFootTarget(ref m_RightFootPosition, HumanBodyBones.RightFoot); // Get Right Foot position
                AdjustFootTarget(ref m_LeftFootPosition, HumanBodyBones.LeftFoot); // Get Left Foot position

                // Cast ground for each foot
                CastFoot(m_RightFootPosition, ref m_RightFootIKPosition, ref m_RightFootIKRotation, ref m_RightGroundNormal, HumanBodyBones.RightFoot); // Cast right foot
                CastFoot(m_LeftFootPosition, ref m_LeftFootIKPosition, ref m_LeftFootIKRotation, ref m_LeftGroundNormal, HumanBodyBones.LeftFoot); // Cast left foot

                // Get height offset for feet
                float rightVelocity = 0;
                float leftVelocity = 0;
                m_RightFootOffset = Mathf.SmoothDamp(m_RightFootOffset, (m_RightFootIKPosition.y - transform.position.y), ref rightVelocity, Time.deltaTime);
                m_LeftFootOffset = Mathf.SmoothDamp(m_LeftFootOffset, (m_LeftFootIKPosition.y - transform.position.y), ref leftVelocity, Time.deltaTime);

                UpdateBodyOffset();
            }
            else
            {
                m_BodyWeight = Mathf.SmoothDamp(m_BodyWeight, 0, ref bodyWeightVel, Time.deltaTime);
                m_FeetWeight = Mathf.SmoothDamp(m_BodyWeight, 0, ref feetWeightVel, Time.deltaTime);
            }

        }

        /// <summary>
        /// Make system run Foot IK Placement
        /// </summary>
        public void EnableFootIK()
        {
            m_EnabledFootIK = true;
        }

        /// <summary>
        /// Make system stops running Foot IK Placement
        /// </summary>
        public void DisableFootIK()
        {
            m_EnabledFootIK = false;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (m_Animator == null || layerIndex != 0) { return; }

            Vector3 targetPosition = m_Animator.bodyPosition + Vector3.Lerp(Vector3.zero, m_BodyOffset, m_BodyWeight);
            m_Animator.bodyPosition = targetPosition;

            //Left Foot
            m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, m_FeetWeight);
            m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, m_Animator.GetFloat("LeftFoot") * m_FeetWeight);

            PlaceFootOnIk(AvatarIKGoal.LeftFoot, m_LeftFootIKPosition, m_LeftFootIKRotation);

            //Right Foot
            m_Animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, m_FeetWeight);
            m_Animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, m_Animator.GetFloat("RightFoot") * m_FeetWeight);

            PlaceFootOnIk(AvatarIKGoal.RightFoot, m_RightFootIKPosition, m_RightFootIKRotation);

        }

               





        /// <summary>
        /// Get horizontal pos of each foot to cast ground
        /// </summary>
        private void AdjustFootTarget(ref Vector3 footPos, HumanBodyBones foot)
        {
            footPos = m_Animator.GetBoneTransform(foot).position;
            footPos.y = transform.position.y + m_MaxFootHeight;
        }


        /// <summary>
        /// Cast foot to find ground bellow
        /// </summary>
        /// <param name="startCastPos">Position to start cast</param>
        /// <param name="footIkPos">IK position parameter to get value</param>
        /// <param name="footIkRot">IK rotation parameter to get value</param>
        private void CastFoot(Vector3 startCastPos, ref Vector3 footIkPos, ref Quaternion footIkRot, ref Vector3 groundNormal, HumanBodyBones foot)
        {
            RaycastHit footHit; // Get hit from ground and from foot

            if (DebugRaycast)
                Debug.DrawLine(startCastPos, startCastPos + Vector3.down * (-m_MinFootHeight + m_MaxFootHeight), Color.magenta);

            Vector3 hitToePoint = Vector3.zero;
            if (Physics.Raycast(startCastPos + transform.forward * 0.2f, Vector3.down, out footHit, -m_MinFootHeight + m_MaxFootHeight, m_GroundLayerMask)) // Cast current foot
            {
                hitToePoint = footHit.point;
            }

            if (Physics.Raycast(startCastPos, Vector3.down, out footHit, -m_MinFootHeight + m_MaxFootHeight, m_GroundLayerMask)) // Cast current foot
            {
                footIkPos = startCastPos;
                footIkPos.y = footHit.point.y + offSetFromGround; // Add user offset

                groundNormal = Vector3.Lerp(groundNormal, footHit.normal, 0.1f);
                return;
            }



            footIkPos = Vector3.zero; // Means that cast failed
        }


        /// <summary>
        /// Check feet positions to find body offset value
        /// </summary>
        private void UpdateBodyOffset()
        {
            float offset = 0;                       // Desired offset

            // Check if system found feet positions
            if (m_RightFootIKPosition != Vector3.zero && m_LeftFootIKPosition != Vector3.zero)
            {
                offset = (m_LeftFootOffset < m_RightFootOffset) ? m_LeftFootOffset : m_RightFootOffset; // Choose the lowest distance
            }

            Vector3.SmoothDamp(m_BodyOffset, Vector3.up * offset, ref m_BodySmoothDampVelocity, bodyAdjustSpeed);
            m_BodyOffset += m_BodySmoothDampVelocity * Time.deltaTime;
        }



        /// <summary>
        /// Set foot position to ground
        /// </summary>
        /// <param name="foot">Target foot</param>
        /// <param name="ikPos">Ik position parameter calculated on Cast</param>
        /// <param name="ikRot">Ik rotation parameter calculated on Cast</param>
        void PlaceFootOnIk(AvatarIKGoal foot, Vector3 ikPos, Quaternion ikRot)
        {
            Vector3 desiredPos = m_Animator.GetIKPosition(foot); // Get initial foot pos

            if (ikPos != Vector3.zero) // If cast didn't fail
            {
                desiredPos = desiredPos + Vector3.up * ((foot == AvatarIKGoal.RightFoot) ? m_RightFootOffset : m_LeftFootOffset);
                if (desiredPos.y < ikPos.y)
                    desiredPos.y = ikPos.y;

                Quaternion rot = m_Animator.GetIKRotation(foot);
                Quaternion footIkRot = Quaternion.FromToRotation(Vector3.up, (foot == AvatarIKGoal.RightFoot) ? m_RightGroundNormal : m_LeftGroundNormal) * Quaternion.Euler(0, rot.eulerAngles.y, 0); // Get foot rotation
                m_Animator.SetIKRotation(foot, footIkRot); // Set rotation
            }

            m_Animator.SetIKPosition(foot, desiredPos); // Set position
        }
    }
}