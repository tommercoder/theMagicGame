using UnityEngine;
using Cinemachine;

[AddComponentMenu("")] // Hide in menu
[SaveDuringPlay]
#if UNITY_2018_3_OR_NEWER
[ExecuteAlways]
#else
    [ExecuteInEditMode]
#endif
public class CinemachineTerrainHugger : CinemachineExtension
{
    public TerrainCollider m_Terrain;
    public float m_CameraHeight = 1;

    private void OnValidate()
    {
        m_CameraHeight = Mathf.Max(0, m_CameraHeight);
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body && m_Terrain != null)
        {
            var b = m_Terrain.bounds;
            var camPos = state.CorrectedPosition;
            camPos.y = b.max.y + 1;
            if (m_Terrain.Raycast(new Ray(camPos, Vector3.down), out RaycastHit hit, b.max.y - b.min.y + 2))
            {
                camPos = hit.point;
                camPos.y += m_CameraHeight;
                state.PositionCorrection += camPos - state.CorrectedPosition;
            }
        }
    }
}
