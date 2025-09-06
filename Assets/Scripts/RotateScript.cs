using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float degreesPerSecond = 90f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float stepY = degreesPerSecond * Time.deltaTime;
        transform.Rotate(0, stepY, 0);
    }
}
