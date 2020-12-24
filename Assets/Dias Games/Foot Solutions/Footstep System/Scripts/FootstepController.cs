using UnityEngine;

namespace DiasGames.FootstepSystem {

    public class FootstepController : MonoBehaviour
    {
        public enum Foot { RightFoot, LeftFoot }

        // Exposed parameters
        [SerializeField] private Foot m_FootReference;              // Set which foot is this
        [SerializeField] private FootstepData m_FootstepData;       // Footstep sounds data

        [Tooltip("Reference to character rigidbody. Don't use ragdoll rigidbodies")]
        [SerializeField] private Rigidbody m_Rigidbody;             // Reference to character Rigidbody

        // Components references
        private AudioSource m_AudioSource;                          // Reference to audio source
        private Animator m_Animator;                                // Reference to animator

        // Internal controller vars
        private bool m_CanPlaySound = false;                        // Tell system wheter it can play a footstep audio
        private string m_GroundName = "";                           // Ground name found by cast
        
        private void Start()
        {
            m_Animator = GetComponentInParent<Animator>();
            m_AudioSource = GetComponent<AudioSource>();

            if (m_Rigidbody == null)
                m_Rigidbody = GetComponentInParent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Cast ground to find its type
            CheckGroundType();

            // Play footstep sound accordingly ground type
            PlayFootstep();
        }

        /// <summary>
        ///  Get footstep sounds and random it to find a clip to play
        /// </summary>
        private void PlayFootstep()
        {
            // Reset condition to play footstep sound
            if (m_Animator.GetFloat(m_FootReference.ToString()) <= 0.4f)
                m_CanPlaySound = true;


            if (m_Animator.GetFloat(m_FootReference.ToString()) > 0.8f && m_CanPlaySound)
            {
                m_AudioSource.clip = Footstep();

                if (m_AudioSource.clip == null)
                    return;

                //Randomize volume and pitch
                m_AudioSource.volume = 1f + Random.Range(-m_FootstepData.VolumeVariance, m_FootstepData.VolumeVariance);
                m_AudioSource.pitch = 1f + Random.Range(-m_FootstepData.PitchVariance, m_FootstepData.PitchVariance);

                m_AudioSource.Play();
                m_CanPlaySound = false;
            }
        }


        /// <summary>
        /// Get a footstep sound
        /// </summary>
        /// <returns>Footstep sound clip</returns>
        private AudioClip Footstep()
        {
            AudioClip[] clips = m_FootstepData.GetFootstepsClips(m_GroundName, m_Rigidbody.velocity);

            if (clips == null)
                return null;

            return clips[Random.Range(0, clips.Length)];
        }


        /// <summary>
        /// Cast ground to find its name or tag
        /// </summary>
        private void CheckGroundType()
        {
            Vector3 Start = transform.position + Vector3.up * 0.3f;

            RaycastHit ground;
            if (Physics.Raycast(Start, Vector3.down, out ground, 1f, Physics.AllLayers))
            {
                if (ground.collider.tag == "Terrain")
                    m_GroundName = FootstepData.TerrainFindTexture(ground.point);
                else
                    m_GroundName = ground.collider.tag;

                return;
            }

            m_GroundName = string.Empty;
        }

    }
}