using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject; // Reference to the CuttingCounter script
    [SerializeField] private Image barImage; // Reference to the progress bar UI element

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>(); // Get the IHasProgress component from the specified GameObject
        if (hasProgress == null)
        {
            Debug.LogError("Game Object"+ hasProgressGameObject+"does not have a component that implements IhasProgress!"); // Log an error if the component is not found
            return;
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged; // Subscribe to the OnProgressChanged event

        barImage.fillAmount = 0f; // Initialize the progress bar to 0

        Hide();

    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized; // Update the progress bar fill amount based on the normalized progress
        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide(); // Hide the progress bar if the progress is 0
        }
        else
        {
            Show(); // Show the progress bar if the progress is greater than 0
        }

    }

    private void Show()
    {
        gameObject.SetActive(true); // Show the progress bar UI
    }

    public void Hide()
    {
        gameObject.SetActive(false); // Hide the progress bar UI
    }
}
