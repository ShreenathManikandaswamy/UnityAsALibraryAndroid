using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private void Start()
    {
        text.text = "Opening unity content";
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKeyDown(KeyCode.Escape)) 
                Application.Quit();
    }

    public void UnloadUnity()
    {
        Application.Unload();
    }
}
