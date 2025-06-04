using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class DeliveryCounter : BaseCounter
{
    private int score = 0;

    [SerializeField] private KitchenObjectSO cheeseSliceSO;
    [SerializeField] private KitchenObjectSO cabbageSliceSO;
    [SerializeField] private KitchenObjectSO tomatoSliceSO;
    [SerializeField] private KitchenObjectSO meatPattyCookedSO;

    public event Action<int> OnScoreChanged;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            KitchenObject kitchenObject = player.GetKitchenObject();
            KitchenObjectSO kitchenObjectSO = kitchenObject.GetKitchenObjectSO();

            if (IsValidCutObject(kitchenObjectSO))
            {
                kitchenObject.DestroySelf();
                score++;
                OnScoreChanged?.Invoke(score);
                Debug.Log("Scorul curent: " + score);

                string currentScene = SceneManager.GetActiveScene().name;

                if (currentScene == "GameScene" && score >= 2)
                {
                    StartCoroutine(LoadNextSceneAfterDelay(1f));
                }
                else if (currentScene == "GameScene 1" && score >= 5)
                {
                    ShowGameOver();
                }
            }
            else
            {
                Debug.Log("Obiectul nu este valid pentru această scenă.");
            }
        }
    }

    private bool IsValidCutObject(KitchenObjectSO kitchenObjectSO)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "GameScene")
        {
            return kitchenObjectSO == cheeseSliceSO || kitchenObjectSO == cabbageSliceSO || kitchenObjectSO == tomatoSliceSO;
        }
        else if (currentScene == "GameScene 1")
        {
            return kitchenObjectSO == meatPattyCookedSO;
        }

        return false;
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene 1");
    }

    private void ShowGameOver()
    {
        Debug.Log("GAME OVER");

        GameObject gameOverTextGO = GameObject.Find("GameOverText");
        if (gameOverTextGO != null)
        {
            gameOverTextGO.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverText nu a fost găsit în scenă.");
        }
    }

    public int GetScore()
    {
        return score;
    }
}
