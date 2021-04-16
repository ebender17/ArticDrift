using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _gameResult = default;
    [SerializeField] TextMeshProUGUI _scoreCount = default;
    [SerializeField] Button _lostLevel;
    [SerializeField] Button _wonLevel;

    public void FillGameResultPanel(bool isWon, int value)
    {
        string gameResult = isWon ? "Level Complete" : "You have Lost";

        _gameResult.SetText($"{gameResult}");
        _scoreCount.SetText($"Final Score: {value}");

        if (isWon)
            _wonLevel.gameObject.SetActive(true);
        else
            _lostLevel.gameObject.SetActive(true);
    }
}
