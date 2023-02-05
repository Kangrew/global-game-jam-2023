using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class CashController : Singleton<CashController>
{
    [SerializeField] private UnityEvent _onCashCredited;
    private const string _uiPrefix = "Balance: $";

    private int _balance = 10;

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] Color DeductColor,creditColor;

    public int Balance => _balance;

    public void Start() => UpdateUI();

    public void Credit(int amount)
    {
        _balance += amount;
        _onCashCredited.Invoke();
        UpdateUI();
        textMesh.color = Color.green;
        textMesh.transform.DOPunchScale(new Vector3(1,0.4f,0),0.5f,5).OnComplete(()=>textMesh.color = Color.white);
    }

    public void Deduct(int amount)
    {
        _balance -= amount;
        UpdateUI();
        textMesh.color = Color.red;
        textMesh.transform.DOPunchScale(new Vector3(1,0.4f,0),0.5f,5).OnComplete(()=>textMesh.color = Color.white);
    }

    private void UpdateUI() => textMesh.text = _uiPrefix + _balance;
}
