using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Monetization;

public class AdController : MonoBehaviour
{
    [HideInInspector] public static AdController instance;

    private string store_id = "3217110";

    private string video_ad = "video";
    private string rewarded_video_ad = "rewardedVideo";
    private string banner_ad = "banner";

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
                Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }


    }

    private void Start()
    {
        Monetization.Initialize(store_id, true);
        //StartCoroutine(CallAd());
    }

    private void ShowBanner()
    {
        if (Monetization.IsReady(banner_ad))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(banner_ad) as ShowAdPlacementContent;


            if (ad != null)
                ad.Show();

        }
    }

    private void ShowVideo()
    {
        if (Monetization.IsReady(video_ad))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(video_ad) as ShowAdPlacementContent;


            if (ad != null)
                ad.Show();

        }
    }

    private void ShowRewardVideo()
    {
        if (Monetization.IsReady(rewarded_video_ad))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(rewarded_video_ad) as ShowAdPlacementContent;


            if (ad != null)
                ad.Show();

        }
    }

    IEnumerator CallAd()
    {
        yield return new WaitForSeconds(2f);

        ShowBanner();

        StartCoroutine(CallAd());

    }

}
