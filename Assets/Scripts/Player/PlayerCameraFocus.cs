using UnityEngine;

public sealed class PlayerCameraFocus : MonoBehaviour
{
    private Camera _camera;
    private RaycastHit[] _hits;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _hits = new RaycastHit[3];
    }
    private void Update()
    {
        var hitPoint = GetHitPoint(transform.position, transform.forward, 0.5f);
        var targetDistance = (hitPoint - transform.position).magnitude;
        if (targetDistance - _camera.focusDistance < 0.1f) return;
        _camera.focusDistance = Mathf.Lerp(_camera.focusDistance,
            targetDistance, Time.deltaTime * 10);
    }

    private Vector3 GetHitPoint(Vector3 origin, Vector3 direction, float radius)
    {
        var position = origin;
        if (Physics.SphereCastNonAlloc(origin, radius, direction, _hits) > 0)
        {
            foreach (var hit in _hits)
            {
                if (hit.collider == null || hit.collider.isTrigger) continue;
                position = hit.point + hit.normal / 50;
            }
        }
        return position;
    }
}
