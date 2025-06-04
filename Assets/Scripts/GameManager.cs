using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // păstrează GameManager-ul între scene
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Scor actual: " + score);
    }

    public int GetScore()
    {
        return score;
    }
}
