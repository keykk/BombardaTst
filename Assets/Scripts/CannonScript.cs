using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public List<GameObject> bombPrefabs;
    public Vector2 timeInterval = new Vector2(1, 1);
    public GameObject spawnPoint;
    public GameObject target;
    public float rangeInDegrees;
    public Vector2 force;
    public float arcDegress = 45f;
    private float cooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cooldown = Random.Range(timeInterval.x, timeInterval.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;
        cooldown -= Time.deltaTime;
        if (cooldown < 0)
        {
            cooldown = Random.Range(timeInterval.x, timeInterval.y);

            Fire();
        }
    }

    private void Fire()
    {
        GameObject bombPrefab = bombPrefabs[Random.Range(0, bombPrefabs.Count)];

        GameObject bomb = Instantiate(bombPrefab, spawnPoint.transform.position, bombPrefab.transform.rotation);

        //aplica a força
        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        Vector3 impulseVector = target.transform.position - spawnPoint.transform.position;
        
        impulseVector.Scale(new Vector3(1, 0, 1));
        impulseVector.Normalize();

        impulseVector += new Vector3(0, arcDegress / 45f, 0);
        impulseVector.Normalize();
        //impulseVector = Quaternion.AngleAxis(arcDegress, Vector3.right) * impulseVector;
        impulseVector = Quaternion.AngleAxis(rangeInDegrees * Random.Range(-1f, 1f), Vector3.up) * impulseVector;

        impulseVector *= Random.Range(force.x, force.y);
        bombRigidbody.AddForce(impulseVector, ForceMode.Impulse);
    }
}
