using UnityEngine;

public class Painting : MonoBehaviour
{
    [SerializeField] private GameObject _Painting;
    [SerializeField] private GameObject _PaintingHolder;
    [SerializeField] private bool _IsPickedUp;

    [SerializeField] private int _ItemID;

    [SerializeField] private float _RotationHolder;
    [SerializeField] private int _RotationInHand;

    public void Interactable(GameObject pos)
    {
        if (!_IsPickedUp)
        {
            _Painting.transform.parent = pos.transform;
            transform.position = pos.transform.position;
            _Painting.transform.localRotation = Quaternion.Euler(0, _RotationInHand, 0);
            _IsPickedUp = true;
        }
        else
        {
            transform.SetParent(pos.transform);
            transform.position = pos.transform.position;
            _Painting.transform.localRotation = Quaternion.Euler(0, 90, 0);
            _IsPickedUp = false;
        }
    }

    public void DropPainting()
    {
        _Painting.transform.parent = _PaintingHolder.transform;
        transform.position = _PaintingHolder.transform.position;
        _Painting.transform.localRotation = Quaternion.Euler(0, _RotationHolder, 0);
        _IsPickedUp = false;
    }

    public int GetPaintingID()
    {
        return _ItemID;
    }
}
