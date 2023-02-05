using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CashController : Singleton<CashController>
{
    [SerializeField] private UnityEvent _onCashCredited;
    private const string _uiPrefix = "Balance: $";

    private int _balance = 10;

    [SerializeField] private TextMeshProUGUI textMesh;

    public int Balance => _balance;

    public void Start() => UpdateUI();

    public void Credit(int amount)
    {
        _balance += amount;
        _onCashCredited.Invoke();
        UpdateUI();
    }

    public void Deduct(int amount)
    {
        _balance -= amount;
        UpdateUI();
    }

    private void UpdateUI() => textMesh.text = _uiPrefix + _balance;
}
