using Cinemachine;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void UpdateFollowTarget(CameraFollowTargetSignal cameraFollowTargetInfo)
    {
        _virtualCamera.Follow = cameraFollowTargetInfo.Target.transform;
    }
}

public class CameraFollowTargetSignal
{
    public GameObject Target;
}
