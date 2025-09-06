using System.Collections;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float ExplosionDelay = 5f;
    public GameObject ExplosionPrefab;
    public GameObject WoodBreakingPrefab;
    public float BlastRadius = 4f;
    public int BlastDamage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start the coroutine to handle the explosion delay
        StartCoroutine(ExplosionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ExplosionCoroutine()
    {
        // Wait for the specified delay before executing the explosion logic
        yield return new WaitForSeconds(ExplosionDelay);

        // Explode a bomba
        Explode();
    }

    private void Explode()
    {
        //Cria VFX de explosão
        Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);

        // Destroir plataformas
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach (Collider collider in colliders)
        {
            GameObject hitObject = collider.gameObject;
            if (hitObject.CompareTag("Platform"))
            {
                LifeScript lifeScript = hitObject.GetComponent<LifeScript>();
                if (lifeScript != null)
                {
                    float distance = (hitObject.transform.position - transform.position).magnitude;
                    float distanceRate = Mathf.Clamp(distance / BlastRadius, 0, 1);
                    float damageRate = 1f - Mathf.Pow(distanceRate, 4); // Curva de dano (quanto mais longe, menos dano)
                    int damage = (int) Mathf.Ceil(damageRate * BlastDamage);
                    lifeScript.health -= damage;
                    if (lifeScript.health <= 0)
                    {
                        // Criar SFX de destruição
                        Instantiate(WoodBreakingPrefab, hitObject.transform.position, WoodBreakingPrefab.transform.rotation);
                        Destroy(hitObject);
                    }
                }
                //Destroy(collider.gameObject);
            }
        }

        // Criar SFX de destruição

        // Destroir a bomba
        Destroy(gameObject);
    }

}
