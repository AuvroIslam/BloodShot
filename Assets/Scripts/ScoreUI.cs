using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private bool _findScoreManagerAutomatically = true;

    private void Start()
    {
        // Find text component if not assigned
        if (_scoreText == null)
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
        }

        // Find ScoreManager automatically
        if (_findScoreManagerAutomatically && _scoreManager == null)
        {
            _scoreManager = FindFirstObjectByType<ScoreManager>();
        }

        if (_scoreManager == null)
        {
            Debug.LogError("ScoreUI: ScoreManager not found!");
        }
    }

    private void Update()
    {
        if (_scoreManager != null && _scoreText != null)
        {
            _scoreText.text = _scoreManager.GetScore().ToString();
        }
    }
}
