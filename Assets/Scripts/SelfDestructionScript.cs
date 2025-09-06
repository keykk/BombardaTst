using System.Collections;
using UnityEngine;

public class SelfDestructionScript : MonoBehaviour
{
    public float Delay = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BeginSelfDestruction());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private IEnumerator BeginSelfDestruction()
    {
        // Wait for the specified delay before destroying the object
        yield return new WaitForSeconds(Delay);
        
        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
}
