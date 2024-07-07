using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _characterTransform;
    private Vector3 _character;
    private Vector3 _cameraCurrentPosition;
    public float _cameraSpeed = 5f;

    public void Initialize(Transform characterTransform)
    {
        _characterTransform = characterTransform;
    }

    private void Update()
    {
        if (_characterTransform != null)
        {
            _character = new Vector3(_characterTransform.position.x, _characterTransform.position.y, -10);
            _cameraCurrentPosition = Vector3.Lerp(transform.position, _character, _cameraSpeed * Time.deltaTime);
            transform.position = _cameraCurrentPosition;
        }
    }
}
