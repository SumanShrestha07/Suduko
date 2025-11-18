using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SudukoNumber : MonoBehaviour
{
    [SerializeField] public int row,column;
    private TextMeshProUGUI _numberText;
    private Button _numberButton;

    private void Start()
    {
        _numberButton = GetComponent<Button>();
        _numberText = transform.GetComponentInChildren<TextMeshProUGUI>();
        
        _numberButton.onClick.AddListener(NumberCellClicked);
    }

    private void NumberCellClicked()
    {
        GameManager.Instance.HighLightCells(row,column);
    }
}
