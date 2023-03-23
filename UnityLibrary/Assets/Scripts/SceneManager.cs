using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI intentText;
    [SerializeField]
    private Transform intentsParent;

    private List<string> intents;
    private int count = 0;

    private void Awake()
    {
        if(getIntentData())
            DisplayIntents();
    }

    private void DisplayIntents()
    {
        foreach(string intent in intents)
        {
            var value = Instantiate(intentText, intentsParent);
            value.text = intent;
        }
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
    }

    public void QuitUnity()
    {
        Application.Quit();
    }

    public void LoadAndroidActivity()
    {
        count++;
        AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("Launch", "Received data from unity. Count = " + count);
    }


    #region Intent Process
    private bool getIntentData()
    {
#if (!UNITY_EDITOR && UNITY_ANDROID)
        return CreatePushClass (new AndroidJavaClass ("com.unity3d.player.UnityPlayer"));
#endif
        return false;
    }

    public bool CreatePushClass(AndroidJavaClass UnityPlayer)
    {
#if UNITY_ANDROID
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
        AndroidJavaObject extras = GetExtras(intent);

        if (extras != null)
        {
            intents = new List<string>();
            intents.Add(GetProperty(extras, "my_text"));
            intents.Add(GetProperty(extras, "test"));
            return true;
        }
#endif
        return false;
    }

    private AndroidJavaObject GetExtras(AndroidJavaObject intent)
    {
        AndroidJavaObject extras = null;

        try
        {
            extras = intent.Call<AndroidJavaObject>("getExtras");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        return extras;
    }

    private string GetProperty(AndroidJavaObject extras, string name)
    {
        string s = string.Empty;

        try
        {
            s = extras.Call<string>("getString", name);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        return s;
    }
    #endregion
}
