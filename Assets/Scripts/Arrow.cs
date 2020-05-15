using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 2000.0f;
    public Transform tip = null;

    private Rigidbody rigid = null;
    private bool isStopped = true;
    private Vector3 lastPos = Vector3.zero;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (isStopped)
            return;

        // Rotate
        rigid.MoveRotation(Quaternion.LookRotation(rigid.velocity, transform.up));

        // Collision
        if (Physics.Linecast(lastPos, tip.position))
        {
            Stop();
        }

        // Store Position
        lastPos = tip.position;
    }

    private void Stop()
    {
        isStopped = true;

        rigid.isKinematic = true;
        rigid.useGravity = false;
    }

    public void Fire(float pullValue)
    {
        isStopped = false;
        transform.parent = null;

        rigid.isKinematic = false;
        rigid.useGravity = true;
        rigid.AddForce(transform.forward * (pullValue * speed));

        Destroy(gameObject, 5.0f);
    }
}
