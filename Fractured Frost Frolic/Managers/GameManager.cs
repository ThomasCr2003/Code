using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int _sledIndexP1;
    [SerializeField] private int _sledIndexP2;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Getter for the Sled Index.
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <returns></returns>
    public int GetSledIndex(int playerIndex)
    {
        if (playerIndex == 1)
        {
            return _sledIndexP1;
        }
        else if (playerIndex == 2)
        {
            return _sledIndexP2;
        }
        else return _sledIndexP1;
    }

    /// <summary>
    /// Setter for the Sled Index.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="player"></param>
    public void SetSledIndex(int value, int player)
    {
        if (player == 1)
        {
            _sledIndexP1 = value;
        }
        else if (player == 2)
        {
            _sledIndexP2 = value;
        }
        else
        {
            Debug.LogWarning("Oeioeioei went wrong");
        }
    }
}