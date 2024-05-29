using UnityEngine;
using TMPro;

public class DisplayMoney : MonoBehaviour
{
    private TextMeshProUGUI _moneyText;
    [SerializeField] private bool _isMainMenu;
    private void Start()
    {
        _moneyText = GetComponent<TextMeshProUGUI>();
        //Checks If The Text is in the main menu for the Money that need to be displayed.
        if (_isMainMenu)
        {
            MoneyManager.Instance.GetSavedMoney();
            _moneyText.text = "$" + MoneyManager.Instance.TotalMoney.ToString();
        }
        else
        {
            _moneyText.text = "$" + MoneyManager.Instance.CurrentMoneyCollected.ToString();
        }
    }

    private void Update()
    {
        if (!_isMainMenu)
        {
            _moneyText.text = "$" + MoneyManager.Instance.CurrentMoneyCollected.ToString();
        }
    }
}
