using TMPro;
using UnityEngine;

public class DeliveryCounterUIConnector : MonoBehaviour
{
    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        deliveryCounter.OnScoreChanged += UpdateScoreText;
        UpdateScoreText(deliveryCounter.GetScore());
    }

    private void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }
}
