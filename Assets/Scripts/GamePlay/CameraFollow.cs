using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 initialPosition;

    [SerializeField] private Transform target;
    private float lerpDuration = 0.125f;
    private Vector3 offSet = new Vector3(0,-8f,0);

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (!target ) return;
        float nextPosition = target.position.y + offSet.y;
        if (nextPosition <= Mathf.Abs(offSet.y)) {
            nextPosition = offSet.y;
            return;
        }
        var newY = Mathf.Lerp(transform.position.y, nextPosition, lerpDuration);
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
