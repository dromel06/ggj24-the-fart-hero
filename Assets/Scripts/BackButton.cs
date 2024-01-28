using GodFramework;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private string SceneToGoBackTo = "MainMenu";

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnBackButtonDownHandler); ;
    }

    private void OnBackButtonDownHandler()
    {
        FwkPubSub.OnLoadScene.Dispatch(this, new LoadSceneArg(SceneToGoBackTo, false));
    }
}
