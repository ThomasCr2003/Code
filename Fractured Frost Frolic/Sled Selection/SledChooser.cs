using UnityEngine;

public class SledChooser : MonoBehaviour
{
    public GameObject[] Sled;
    [SerializeField] private Transform _SledSpawnPoint;
    [SerializeField] private GameObject _Sled;
    private int _sledIndex;
    private int _playerIndex;

    private void Start()
    {
        GameManager.Instance.SetSledIndex(_sledIndex, _playerIndex);
        _Sled = Instantiate(Sled[_sledIndex], _SledSpawnPoint);
        FindObjectOfType<SledChooserPlayerManager>().AddPlayer(this);
    }

    /// <summary>
    /// Selects the sled you want.
    /// </summary>
    /// <param name="_change"></param>
    public void Selectcar(int _change)
    {
        Destroy(_Sled);                                                     //Destroys the sled so you dont see 2 sleds.
        _sledIndex += _change;                                              //Changes the index so you get the next sled.
        if (_sledIndex > Sled.Length - 1)                                   //Makes it so you dont go above the array length.
        {
            _sledIndex = 0;
        }
        else if (_sledIndex < 0)                                            //Makes it so you dont go lower then 0.
        {
            _sledIndex = Sled.Length - 1;
        }
        GameManager.Instance.SetSledIndex(_sledIndex, _playerIndex);        //Makes it so the GameManager knows what player chooses what sled.
        _Sled = Instantiate(Sled[_sledIndex], _SledSpawnPoint);             //Spawns in the sled.
    }

    /// <summary>
    /// To go to the next sled. Used For Buttons.
    /// </summary>
    public void OnNextSled()                                                
    {
        Selectcar(1);
    }

    /// <summary>
    /// To go to the previous sled. Used For Buttons.
    /// </summary>
    public void OnPrevSled()                                               
    {
        Selectcar(-1);
    }


    /// <summary>
    /// Setter for which player is choosing.
    /// </summary>
    /// <param name="value"></param>
    public void SetPlayerIndex(int value)                                   
    {
        _playerIndex = value;
    }
}
