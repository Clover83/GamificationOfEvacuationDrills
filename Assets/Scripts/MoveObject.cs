using UnityEngine;

// Responsible for moving the ar origin to the player's picked position on start.

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [Tooltip("Keep target's original height")]
    [SerializeField]
    private bool _keepOriginalHeight = true;

    public void PerformMove()
    {
        if (_keepOriginalHeight)
        {
            Vector3 p = StartPositionData.position;
            p.y = _target.position.y;
            _target.position = p;
        }
        else { _target.position = StartPositionData.position; }
        
        _target.rotation = StartPositionData.rotation;
    }
}
