using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerLook : MonoBehaviourPunCallbacks
{
    public float MouseSensetivity;
    public Transform PlayerBody;
    public LayerMask LayerMask;
    [SerializeField] private float _InteractRange;
    [SerializeField] private GameObject _HandRef;
    [SerializeField] private Image[] _Crosshair;
    [SerializeField] private GameObject _PaintingPosition;
    [SerializeField] private PlayerHitBox _HitBox;
    private int _paintingID;
    private float _xRotation;
    private Painting _heldPainting = null;
    private Animation _doorAnimation;
    private AudioSource _DoorAudioSource;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _Crosshair[0].enabled = true;
        _Crosshair[1].enabled = false;
        _DoorAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensetivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        PlayerBody.Rotate(Vector3.up * mouseX);


        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward) * _InteractRange, _InteractRange, LayerMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _InteractRange, Color.yellow);
        UnInteractableCursor();
        Painting painting = null;
        PaintingCorrectPlace _CorrectPlace = null;
        Puzzle1Button puzzleButton = null;
        Pickup pickup = null;
        Interactable interact = null;
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            //Checks if it even hit a variable if not break the loop.
            if (hit.collider.gameObject.GetComponent<Painting>() == null && hit.collider.gameObject.GetComponent<PaintingCorrectPlace>() == null && hit.collider.gameObject.GetComponent<Puzzle1Button>() == null && hit.collider.gameObject.GetComponent<Pickup>() == null && hit.collider.gameObject.GetComponent<Interactable>() == null)
                break;

            //Gets fills the variable if it hit.
            if (hit.collider.gameObject.GetComponent<Painting>() != null && painting == null)
            {
                painting = hit.collider.gameObject.GetComponent<Painting>();
            }

            if (hit.collider.gameObject.GetComponent<PaintingCorrectPlace>() != null)
            {
                _CorrectPlace = hit.collider.gameObject.GetComponent<PaintingCorrectPlace>();
            }

            if (hit.collider.gameObject.GetComponent<Puzzle1Button>() != null)
            {
                puzzleButton = hit.collider.gameObject.GetComponent<Puzzle1Button>();
            }

            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                pickup = hit.collider.gameObject.GetComponent<Pickup>();
            }

            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                interact = hit.collider.gameObject.GetComponent<Interactable>();
            }
            //If you hit something change the cursor.
            if (hit.collider.gameObject.GetComponent<Painting>() != null || hit.collider.gameObject.GetComponent<PaintingCorrectPlace>() != null || hit.collider.gameObject.GetComponent<Puzzle1Button>() != null || hit.collider.gameObject.GetComponent<Interactable>() != null || hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                InteractableCursor();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_CorrectPlace != null && _heldPainting != null)
            {
                _PaintingPosition = _CorrectPlace.GetPosition();
                _CorrectPlace.CheckIfRightPainting(_paintingID);
                _heldPainting.Interactable(_PaintingPosition);
                _heldPainting = null;
                _PaintingPosition = null;
            }
            else if (painting != null && _heldPainting == null)
            {
                _heldPainting = painting;
                _heldPainting.Interactable(_HandRef);
                _paintingID = _heldPainting.GetPaintingID();
            }

            if (puzzleButton != null)
            {
                puzzleButton.PressButton();
            }

            if (pickup != null)
            {
                pickup.Interactable(_HandRef);
                pickup.HasKey = true;
                _HitBox.DoorAnimation = pickup.GetDoorAnimation();
            }
            if (interact != null)
            {
                if (!interact.DoorHasNotBeenOpend())
                {
                    _doorAnimation = interact.GetDoorAnimation();
                    _doorAnimation.Play();
                    _DoorAudioSource.Play();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _heldPainting.DropPainting();
            _heldPainting = null;
        }
    }

    public void UnInteractableCursor()
    {
        _Crosshair[1].enabled = false;
        _Crosshair[0].enabled = true;
    }

    public void InteractableCursor()
    {
        _Crosshair[0].enabled = false;
        _Crosshair[1].enabled = true;
    }

}
