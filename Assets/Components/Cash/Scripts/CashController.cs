using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CashController : Singleton<CashController>
{
    private const string _uiPrefix = "Balance: $";

    private int _balance = 10;

    [SerializeField] private TextMeshProUGUI textMesh;

    public int Balance => _balance;

    public void Start() => UpdateUI();

    public void Credit(int amount)
    {
        _balance += amount;
        UpdateUI();
    }

    public void Deduct(int amount)
    {
        _balance -= amount;
        UpdateUI();
    }

    private void UpdateUI() => textMesh.text = _uiPrefix + _balance;
}
