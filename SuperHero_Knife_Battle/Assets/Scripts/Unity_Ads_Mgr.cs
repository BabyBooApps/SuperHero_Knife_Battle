using UnityEngine;
using UnityEngine.Advertisements;

public class Unity_Ads_Mgr : MonoBehaviour
{
    private string gameId = "your_game_id_here";
    private bool testMode = true; // Set to false for the production build

    private string rewardedVideoPlacementId = "rewardedVideo";

    private bool isRewardedVideoReady = false;

    private void Start()
    {
       // Advertisement.AddListener(this);
       // Advertisement.Initialize(gameId, testMode);
    }

    public void ShowRewardedAd()
    {
        if (isRewardedVideoReady)
        {
           // Advertisement.Show(rewardedVideoPlacementId);
        }
        else
        {
            Debug.Log("Rewarded video is not ready yet. Please try again later.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == rewardedVideoPlacementId)
        {
            isRewardedVideoReady = true;
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Unity Ads error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Unity Ads started: " + placementId);
    }

   /* public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedVideoPlacementId)
        {
            switch (showResult)
            {
                case ShowResult.Finished:
                    // Reward the player with coins here.
                    // For example, call a function in your game's currency manager to add coins.
                   // CurrencyManager.Instance.AddCoins(50);
                    Debug.Log("Rewarded video completed. Player rewarded with 50 coins.");
                    break;
                case ShowResult.Skipped:
                    Debug.Log("Rewarded video was skipped. No reward for the player.");
                    break;
                case ShowResult.Failed:
                    Debug.Log("Rewarded video failed to play. No reward for the player.");
                    break;
            }
        }
    }*/
}
