//using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static readonly string KEY_HIGHEST_SCORE = "HighestScore";

    public bool isGameOver { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource gameOverSfx;

    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private int highestScore;

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        //Score
        score = 0;
        highestScore = PlayerPrefs.GetInt(KEY_HIGHEST_SCORE);
    }

    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime;

            if (GetScore() > GetHighestScore())
            {
                highestScore = GetScore();
            }
        }

    }

    public int GetScore()
    {
        return (int)Mathf.Floor(score);
    }

    public int GetHighestScore()
    {
        return highestScore;
    }

    public void EndGame()
    {
        if (isGameOver) return;

        isGameOver = true;

        //stop music
        musicPlayer.Stop();

        // Play SFX
        gameOverSfx.Play();

        //Save Score
        PlayerPrefs.SetInt(KEY_HIGHEST_SCORE, GetHighestScore());

        StartCoroutine(ReloadScene(5));

    }

    private IEnumerator ReloadScene(float delay)
    {
        yield return new WaitForSeconds(delay);

        //ResetGameState();

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName/*, LoadSceneMode.Single*/);
    }

    /*private void ResetGameState()
    {
        // Reset variáveis estáticas se houver
        // Exemplo: GameManager.Instance.Reset();
        
        // Para garantir que tudo seja recarregado corretamente
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }*/
}
