using UnityEngine;
using TMPro;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _gameResult = default;
    [SerializeField] TextMeshProUGUI _scoreCount = default;

    public void FillGameResultPanel(bool isWon, int value)
    {
        string gameResult = isWon ? "Won" : "Lost";

        _gameResult.SetText($"You have {gameResult}");
        _scoreCount.SetText($"Final Score: {value}");
    }
}
