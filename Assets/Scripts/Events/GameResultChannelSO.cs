using UnityEngine;

[CreateAssetMenu(fileName = "NewGameResultEventChannel", menuName = "Events/Game Result Event Channel")]
public class GameResultChannelSO : ScriptableObject
{
    public GameResultAction OnEventRaised;

    public void RaiseEvent(bool isWon, int playerScore)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(isWon, playerScore);
    }
}
public delegate void GameResultAction(bool isWon, int playerScore);
