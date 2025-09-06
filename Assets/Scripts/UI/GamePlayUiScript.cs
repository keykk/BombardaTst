using TMPro;
using UnityEngine;

public class GamePlayUiScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highestScoreLabel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreLabel.text = GameManager.Instance.GetScore().ToString();
        highestScoreLabel.text = GameManager.Instance.GetHighestScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = GameManager.Instance.GetScore().ToString();
        highestScoreLabel.text = GameManager.Instance.GetHighestScore().ToString();
    }
}
