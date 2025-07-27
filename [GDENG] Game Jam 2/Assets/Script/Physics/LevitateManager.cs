using UnityEngine;
using System.Collections.Generic;
using static EventNames;

public class LevitateManager : MonoBehaviour
{
    [SerializeField] private float levitateHeight = 2f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private Vector3 rotationAxis = new Vector3(0.5f, 1f, 0.2f);

    private List<LevitateData> levitatingObjects = new List<LevitateData>();
    private bool isLevitating = false;

    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("canLevitate");
        foreach (var obj in objects)
        {
            levitatingObjects.Add(new LevitateData(obj, levitateHeight, rotationSpeed, rotationAxis));
        }
    }

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, StartLevitation);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, ResetObjects);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, StartLevitation);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, ResetObjects);
    }

    private void Update()
    {
        foreach (var data in levitatingObjects)
        {
            if (isLevitating)
                data.LevitateObject();
            else
                data.ResetObject();
        }
    }

    private void StartLevitation()
    {
        isLevitating = true;
        foreach (var data in levitatingObjects)
            data.SetLevitatePosition();
    }

    private void ResetObjects()
    {
        isLevitating = false;
    }

    private class LevitateData
    {
        private GameObject obj;
        private Vector3 initialPosition;
        private Quaternion initialRotation;
        private Vector3 targetPosition;
        private float levitateHeight;
        private float rotationSpeed;
        private Vector3 rotationAxis;

        public LevitateData(GameObject obj, float levitateHeight, float rotationSpeed, Vector3 rotationAxis)
        {
            this.obj = obj;
            this.levitateHeight = levitateHeight;
            this.rotationSpeed = rotationSpeed;
            this.rotationAxis = rotationAxis;
            initialPosition = obj.transform.position;
            initialRotation = obj.transform.rotation;
            targetPosition = new Vector3(initialPosition.x, initialPosition.y + levitateHeight, initialPosition.z);
        }

        public void SetLevitatePosition()
        {
            obj.transform.position = targetPosition;
        }

        public void LevitateObject()
        {
            if (rotationAxis.magnitude > 0)
            {
                obj.transform.Rotate(rotationAxis.normalized, rotationSpeed * Time.deltaTime);
            }
        }

        public void ResetObject()
        {
            if (obj.transform.position != initialPosition)
            {
                obj.transform.position = initialPosition;
                obj.transform.rotation = initialRotation;
            }
        }
    }
}