using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    private int _sceneLoadCount = 0;
    private const int ShowInterstitialAfterScenes = 3;

    void Awake()
    {
        if (Instance) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        MobileAds.Initialize(initStatus =>
        {
            LoadInterstitial();
            LoadRewardedInterstitial();   // YENİ
        });

    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _sceneLoadCount++;
        if (_sceneLoadCount >= ShowInterstitialAfterScenes)
        {
            ShowInterstitial();
            _sceneLoadCount = 0;
        }
    }

    #region GecisResklami

    InterstitialAd interstitial;
    string interstitialId = "ca-app-pub-7163425476823301/5146889626";
    void LoadInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();
            interstitial = null;
        }

        var adRequest = new AdRequest();
        InterstitialAd.Load(interstitialId, adRequest, (InterstitialAd ad, LoadAdError err) =>
        {
            if (err != null || ad == null)
            {
                Debug.LogWarning("Interstitial load fail: " + err);
                return;
            }

            interstitial = ad;

            interstitial.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial kapandı, yeniden yükleniyor.");
                LoadInterstitial();
            };
            interstitial.OnAdFullScreenContentFailed += (AdError e) =>
            {
                Debug.LogWarning("Interstitial gösterim hatası: " + e);
                LoadInterstitial();
            };
        });
    }

    public bool IsReady() => interstitial != null;

    public void ShowInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Show();
            interstitial = null; // tek kullanımlık; kapandıktan sonra yeniden yüklenecek
        }
        else
        {
            LoadInterstitial();
        }
    }

    #endregion


    #region Odullu Gecis Reklami

    RewardedInterstitialAd rInterstitial;
#if UNITY_ANDROID
    string rInterstitialId = "ca-app-pub-7163425476823301/3674025853\r\n"; // TEST Rewarded-Interstitial
#elif UNITY_IPHONE
string rInterstitialId = "ca-app-pub-7163425476823301/3674025853"; // TEST Rewarded-Interstitial iOS
#else
string rInterstitialId = "unused";
#endif
    void LoadRewardedInterstitial()
    {
        if (rInterstitial != null) { rInterstitial.Destroy(); rInterstitial = null; }

        var req = new AdRequest();
        RewardedInterstitialAd.Load(rInterstitialId, req, (RewardedInterstitialAd ad, LoadAdError err) =>
        {
            if (err != null || ad == null)
            {
                Debug.LogWarning("Rewarded-Interstitial load fail: " + err);
                return;
            }

            rInterstitial = ad;

            rInterstitial.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Rewarded-Interstitial kapandı, yeniden yükleniyor.");
                LoadRewardedInterstitial();
            };
            rInterstitial.OnAdFullScreenContentFailed += (AdError e) =>
            {
                Debug.LogWarning("Rewarded-Interstitial gösterim hatası: " + e);
                LoadRewardedInterstitial();
            };
        });
    }

    public void ShowRewardedInterstitial(Action onRewardEarned = null)
    {
        if (rInterstitial != null)
        {
            var ad = rInterstitial;  // tek kullanımlık
            rInterstitial = null;
            ad.Show((Reward r) =>
            {
                onRewardEarned?.Invoke();
            });
        }
        else
        {
            Debug.Log("Rewarded-Interstitial hazır değil, yükleniyor...");
            LoadRewardedInterstitial();
        }
    }


    #endregion

}
