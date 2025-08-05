using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading;
using Cysharp.Threading.Tasks;

public class InfoView : MonoBehaviour
{
    [SerializeField, Header("表示するImageをバインドしてください")] private Image _image;
    [SerializeField, Header("説明画面に表示するテキストと画像")] private InfoScriptableObj _infoScriptableObj;
    [SerializeField, Header("ウィンドウが表示されるまでにかかる時間")] private float _waitTime = 0.5f;
    [SerializeField, Header("ウィンドウが切り替わるのにかかる時間")] private float _windowChangeTime = 1.0f;
    [SerializeField, Header("ウィンドウの消えるときの移動先")] private float _deleteWindowPosX = -500f;
    [SerializeField, Header("テキスト")] private Text _text;
    Tween _deleteTween;
    Tween _newTween;
    Image _deleteImage;
    Image _newImage;
    private int _currentIndex = 0;
    CancellationTokenSource _cancellationTokenSource;
    void Start()
    {
        var localScale = transform.localScale;
        transform.localScale = Vector2.zero;
        _image.sprite = _infoScriptableObj.InfoDataArray[_currentIndex].Image;
        transform.DOScale(localScale, _waitTime).SetEase(Ease.OutBack);

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        Text(_infoScriptableObj.InfoDataArray[_currentIndex].Text, _cancellationTokenSource.Token).Forget();
    }
    public void NextWindow()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        Text(_infoScriptableObj.InfoDataArray[_currentIndex].Text, _cancellationTokenSource.Token).Forget();
        _deleteTween?.Kill();
        _newTween?.Kill();
        if (_deleteImage)
        {
            Destroy(_deleteImage?.gameObject);
            ((RectTransform)_newImage.transform).anchoredPosition = Vector2.zero;
            _image = _newImage;
        }
        _currentIndex++;
        if (_currentIndex >= _infoScriptableObj.InfoDataArray.Length)
        {
            _currentIndex = 0; // Loop back to the first item
        }
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        Text(_infoScriptableObj.InfoDataArray[_currentIndex].Text, _cancellationTokenSource.Token).Forget();
        _newImage = Instantiate(_image, _image.transform.parent);
        _deleteImage = _image;
        _deleteTween = ((RectTransform)_image.transform).DOAnchorPosX(_deleteWindowPosX, _windowChangeTime).OnComplete(() => Destroy(_deleteImage.gameObject));
        _newImage.sprite = _infoScriptableObj.InfoDataArray[_currentIndex].Image;
        ((RectTransform)_newImage.transform).anchoredPosition = new Vector2(_deleteWindowPosX * -1, ((RectTransform)_newImage.transform).anchoredPosition.y);
        _newTween = ((RectTransform)_newImage.transform).DOAnchorPosX(0, _windowChangeTime).OnComplete(() => _image = _newImage);
    }
    public void BackWindow()
    {
        _deleteTween?.Kill();
        _newTween?.Kill();
        if (_deleteImage)
        {
            Destroy(_deleteImage?.gameObject);
        ((RectTransform)_newImage.transform).anchoredPosition = Vector2.zero;
        _image = _newImage;
        }
        _currentIndex--;
        if (_currentIndex < 0)
        {
            _currentIndex = _infoScriptableObj.InfoDataArray.Length - 1; // Loop back to the last item
        }
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        Text(_infoScriptableObj.InfoDataArray[_currentIndex].Text, _cancellationTokenSource.Token).Forget();
        _newImage = Instantiate(_image, _image.transform.parent);
        _deleteImage = _image;
        _deleteTween = ((RectTransform)_image.transform).DOAnchorPosX(_deleteWindowPosX * -1, _windowChangeTime).OnComplete(() => Destroy(_deleteImage.gameObject));
        ((RectTransform)_newImage.transform).anchoredPosition = new Vector2(_deleteWindowPosX, ((RectTransform)_newImage.transform).anchoredPosition.y);
        _newImage.sprite = _infoScriptableObj.InfoDataArray[_currentIndex].Image;
        ((RectTransform)_newImage.transform).anchoredPosition = new Vector2(_deleteWindowPosX, ((RectTransform)_newImage.transform).anchoredPosition.y);
        _newTween = ((RectTransform)_newImage.transform).DOAnchorPosX(0, _windowChangeTime).OnComplete(() => _image = _newImage);
    }
    async UniTask Text(string text, CancellationToken cancellationToken)
    {
        _text.text = string.Empty;
        for (int i = 0; i < text.Length; i++)
        {
            _text.text += text[i];
            await UniTask.Delay(50, cancellationToken: cancellationToken);
        }
    }
}
