using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{
    public Transform target;

    public float targetHeight = 1.7f;
    public float distance = 5.0f;
    public float offsetFromWall = 0.1f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public int zoomRate = 40;
    public float zoomDampening = 5.0f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rigidbody;
    public LayerMask collisionLayers = -1;

    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }

        if (target == null)
            FindPlayer();
    }

    void FindPlayer()
    {
        CharacterController controller = FindObjectOfType<CharacterController>();
        if (controller)
        {
            target = controller.gameObject.transform;
        }
    }

    void LateUpdate()
    {
        Vector3 vTargetOffset;
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // calculate distance 
            desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
            desiredDistance = Mathf.Clamp(desiredDistance, distanceMin, distanceMax);
            correctedDistance = desiredDistance;

            // calculate camera position
            vTargetOffset = new Vector3(0, -targetHeight, 0);
            Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);


            RaycastHit collisionHit;
            Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);

            bool isCorrected = false;
            if (Physics.Linecast(trueTargetPosition, position, out collisionHit, collisionLayers.value))
            {
                // calculate and remove clipping
                correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - offsetFromWall;
                isCorrected = true;
            }

            currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening) : correctedDistance;


            currentDistance = Mathf.Clamp(currentDistance, distanceMin, distanceMax);

            position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}