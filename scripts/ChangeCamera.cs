using Unity.Cinemachine;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camera_1;
    [SerializeField] private CinemachineCamera camera_2;

    private bool cameraActive1 = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            if (cameraActive1) {
                camera_2.Prioritize();
            }
            else {
                camera_1.Prioritize();
            }

            cameraActive1 = !cameraActive1;
        }

    }
}
