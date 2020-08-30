using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText = null;

    [SerializeField]
    private Image _livesImage = null;

    [SerializeField]
    private Sprite[] _liveSprites = null;

    [SerializeField]
    private Text _gameOverText = null;

    private bool _isGameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
        _livesImage.sprite = _liveSprites[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && _isGameOver) {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        } 
    }

    public void UpdateScore(int newScore) {
        _scoreText.text = "Score: " + newScore;
    }

    public void UpdateLives(int index) {
        _livesImage.sprite = _liveSprites[index];
        if (index == 0) {
            _isGameOver = true;
            StartCoroutine(FlickerText());
        }
    }

    IEnumerator FlickerText() {
        while (_isGameOver) {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.4f);
        }
    }
}
