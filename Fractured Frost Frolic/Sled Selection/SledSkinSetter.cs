using UnityEngine;

public class SledSkinSetter : MonoBehaviour
{
    [SerializeField] private GameObject[] _SledSkins;

    private int _playerIndex = 1;
    private int _skinIndex;

    private void Start()
    {
        _skinIndex = GameManager.Instance.GetSledIndex(_playerIndex);

        foreach (GameObject skin in _SledSkins)
        {
            skin.SetActive(false);
        }
        _SledSkins[_skinIndex].SetActive(true);
    }

    public void SetPlayerIndex(int value)
    {
        _playerIndex = value;
    }
}
