using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private List<Camera> cameras;
    [SerializeField] private List<Animator> cameraAnimators;
    private int currentCameraIndex = 0;

    void Start()
    {
        ActivateCamera(currentCameraIndex);
    }

    void Update()
    {
        if (cameraAnimators.Count == 0 || cameras.Count == 0)
            return;

        Animator currentAnimator = cameraAnimators[currentCameraIndex];
        // Check if the animation is finished
        if (currentAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !currentAnimator.IsInTransition(0))
        {
            NextCamera();
        }
    }

    private void NextCamera()
    {
        // Deactivate current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Move to next camera, loop if at end
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Count;

        // Activate next camera
        ActivateCamera(currentCameraIndex);
    }

    private void ActivateCamera(int index)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
        // Optionally restart animation
        if (cameraAnimators.Count > index)
        {
            cameraAnimators[index].Play(0, 0, 0f);
        }
    }
}
