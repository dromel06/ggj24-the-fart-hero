using UnityEngine;
using System.Collections;


/// <summary>
/// These class is able to create fade in and fade out using legacy OnGUI code.  It creates de black texture it uses for this.
/// </summary>
public class FadeScene : MonoBehaviour
{
    public float FadeFrom;
    public float FadeTo;

    public float FadeLength;

    public bool AutoDestroy;

    private Texture2D _fadeTexture;
    private float _accumulator;

    /// <summary>
    /// Creates fade in or out according to parameters. This static method will attach this script to a user GameObject that will update it.
    /// </summary>
    /// <param name="user">User. The GameObject instance that will update the fade script.</param>
    /// <param name="from">Initial alpha value.</param>
    /// <param name="to">Final alpha value.</param>
    /// <param name="duration">Duration of the fade effect.</param>
    /// <param name="autoDestroy">If set to <c>true</c> auto destroy script.</param>
    public static void CreateFadeScene(GameObject user, float from, float to, float duration, bool autoDestroy)
    {
        FadeScene fadeScene = user.AddComponent<FadeScene>() as FadeScene;
        fadeScene.FadeFrom = from;
        fadeScene.FadeTo = to;
        fadeScene.FadeLength = duration;
        fadeScene.AutoDestroy = autoDestroy;
    }

    void Awake()
    {
        AutoDestroy = true;
    }

    void Start()
    {
        _fadeTexture = new Texture2D(1, 1);
        _fadeTexture.SetPixel(0, 0, Color.black);
        _fadeTexture.Apply(true);
    }

    void Update()
    {
        _accumulator += Time.deltaTime;

        if (AutoDestroy && (_accumulator >= FadeLength))
        {
            Destroy(_fadeTexture);
            Destroy(this);
        }
    }

    void OnGUI()
    {
        GUI.depth = 0;
        GUI.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(FadeFrom, FadeTo, _accumulator));
        GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), _fadeTexture);
    }
}

