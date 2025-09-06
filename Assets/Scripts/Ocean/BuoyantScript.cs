using Unity.VisualScripting;
using UnityEngine;

public class BuoyantScript : MonoBehaviour
{
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    public float buoyancyForce = 10;
    private Rigidbody thisRigidbody;

    private bool hasTouchedWater;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float diffY = transform.position.y;
        bool isUnderwater = diffY < 0;
        if (isUnderwater)
        {
            hasTouchedWater = true;
        }

        if (!hasTouchedWater)
        {
            return;
        }

        if (isUnderwater)
        {
            Vector3 vector = Vector3.up * buoyancyForce * -diffY;
            thisRigidbody.AddForce(vector, ForceMode.Acceleration);
        }
        thisRigidbody.linearDamping = isUnderwater ? underwaterDrag : airDrag;
        thisRigidbody.angularDamping = isUnderwater ? underwaterAngularDrag : airAngularDrag;
    }
}
