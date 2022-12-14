using UnityEngine;

// Used in the position picker scene to save the position the user chose.

public class PositionSaver : MonoBehaviour
{
    [SerializeField]
    private Transform _positionTransform;
    [SerializeField]
    private Transform _rotationTransform;

    public void SaveData()
    {
        if (_positionTransform != null)
        {
            StartPositionData.position = _positionTransform.position;
        }
        else
        {
            StartPositionData.position = transform.position;
        }

        if (_rotationTransform != null)
        {
            StartPositionData.rotation = _rotationTransform.rotation;
        }
        else
        {
            StartPositionData.rotation = transform.rotation;
        }
    }
}
