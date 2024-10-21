using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private Image _cutsceneImage;
    [SerializeField] private Sprite[] _images;
    private float _imageTime = 3.5f;
    private float _slideDuration = 1.0f;
    private int _currentImageIndex = 0;
    private bool _isTransitioning = false;
    private float _timer = 0f;

    void Start()
    {
        _cutsceneImage.sprite = _images[_currentImageIndex];
        _cutsceneImage.rectTransform.anchoredPosition = Vector2.zero;
    }

    void Update()
    {
        if (!_isTransitioning)
        {
            _timer += Time.deltaTime;

            if (_timer >= _imageTime)
            {
                _timer = 0f;
                StartCoroutine(ChangeImage());
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if(_currentImageIndex == _images.Length - 1)
                {
                    EndCutscene();
                }
                _timer = 0f;
                StopAllCoroutines();
                StartCoroutine(ChangeImage());
            }
        }
    }

    IEnumerator ChangeImage()
    {
        _isTransitioning = true;
        int nextImageIndex = _currentImageIndex + 1;

        if (nextImageIndex < _images.Length)
        {
            Sprite nextImage = _images[nextImageIndex];
            Image newImage = Instantiate(_cutsceneImage,_cutsceneImage.transform.parent);
            newImage.sprite = nextImage;
            newImage.rectTransform.anchoredPosition = new Vector2(Screen.width,0);

            yield return StartCoroutine(SlideIn(newImage));

            Destroy(_cutsceneImage.gameObject);
            _cutsceneImage = newImage;
            _currentImageIndex = nextImageIndex;
        }
        else
        {
            EndCutscene();
        }

        _isTransitioning = false;
    }

    IEnumerator SlideIn(Image image)
    {
        float elapsedTime = 0;
        Vector2 startPos = new Vector2(Screen.width,0);
        Vector2 endPos = Vector2.zero;

        while (elapsedTime < _slideDuration)
        {
            image.rectTransform.anchoredPosition = Vector2.Lerp(startPos,endPos,elapsedTime / _slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.rectTransform.anchoredPosition = endPos;
    }

    void EndCutscene()
    {
        SceneManager.LoadScene(1);
    }
}
