using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public int Score { get; private set; }

    public void AddScore(int amount)
    {
        Score += amount;
        
        Debug.Log(Score);
        // Aquí puedes lanzar un evento para actualizar la UI
    }

    public void ResetScore() => Score = 0;
}
