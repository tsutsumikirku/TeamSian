using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class SceneChanger : MonoBehaviour
{
    [SerializeField, Tooltip("次に遷移するシーンを書き込んでください")] private string[] _sceneName;
    [SerializeField, Tooltip("暗転するまでの時間を記載してください")] private float _blackOutTime;
    Image _blackImage;
    void Start()
    {
        _blackImage = GetComponent<Image>();
        _blackImage.color = Color.black;
        _blackImage.DOFade(0, _blackOutTime);
    }
    public void SceneChange(int sceneIndex = 0)
    {
        _blackImage.DOFade(1, _blackOutTime).OnComplete(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName[sceneIndex]);
        });
    }
}
