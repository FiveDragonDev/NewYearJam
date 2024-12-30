using UnityEngine;

public sealed class PlayerInteracts : MonoBehaviour
{
    public bool HeldObject => _joint != null;

    [SerializeField] private Transform _origin;
    [SerializeField] private Rigidbody _hand;
    [SerializeField, Min(0)]
    private float _throwForce = 1, _offset = 1.5f, _distance = 1;

    private Vector3 _scale;
    private HingeJoint _joint;
    private Pickupable _pickupable;

    private void Start()
    {
        PlayerInput.Input.Base.Enable();
        PlayerInput.Input.Inventory.Enable();
    }
    private void Update()
    {
        if (PlayerInput.Input.Inventory.Place.triggered) Place();
        else if (PlayerInput.Input.Inventory.Throw.triggered) Throw();

        bool crosshair = false;
        if (!HeldObject)
        {
            if (Physics.Raycast(_origin.position, _origin.forward,
                    out RaycastHit hit, _distance) && !hit.collider.isTrigger)
            {
                var interact = hit.collider.TryGetComponent(out IInteractable interactable);
                crosshair = interact;

                if (interact && PlayerInput.Input.Base.Interact.triggered)
                    Interact(hit, interactable);
            }
        }
        PlayerCanvas.SetCrosshair(crosshair);
    }

    private void Interact(RaycastHit hit, IInteractable interactable)
    {
        if (_pickupable = interactable as Pickupable) Pickup(hit);
        interactable.Interact();
        if (interactable is MonoBehaviour component)
            PlayerCanvas.SetShortInfo(component.gameObject.name);
    }
    private void Pickup(RaycastHit hit)
    {
        _joint = hit.collider.gameObject.AddComponent<HingeJoint>();
        _scale = hit.collider.bounds.size;
        _hand.transform.localPosition = _origin.transform.forward * _offset +
            _origin.transform.forward * _scale.z;
        _joint.connectedBody = _hand;
        _joint.axis = Vector3.one;
        _joint.breakForce = 20;
        _joint.useSpring = true;
        _joint.spring = new()
        {
            spring = 1000,
            damper = 20,
        };
        _joint.massScale = GetComponent<Rigidbody>().mass;
        _joint.connectedMassScale = _joint.GetComponent<Rigidbody>().mass;
    }

    private void Place()
    {
        if (!HeldObject) return;
        _pickupable.Place();
        Destroy(_joint);
        _joint = null;
        _pickupable = null;
    }
    private void Throw()
    {
        if (!HeldObject) return;
        _joint.gameObject.GetComponent<Rigidbody>()
            .AddForce(_origin.forward * _throwForce);
        _pickupable.Throw();
        Destroy(_joint);
        _joint = null;
        _pickupable = null;
    }
}
