using UnityEngine;

public class OceanScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;

        if (otherObject.CompareTag("Player"))
        {
            // Notify the GameManager that the game is over
            GameManager.Instance.EndGame();
        }
    }
}
