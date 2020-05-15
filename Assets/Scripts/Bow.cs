using System.Collections;
using UnityEngine;
using OVR;

public class Bow : MonoBehaviour
{
    [Header("Assets")]
    public GameObject arrowPref;

    [Header("Bow")]
    public float grabThreshold = 0.15f;
    public Transform start = null;
    public Transform end = null;
    public Transform socket = null;

    private Transform pullingHand = null;
    private Arrow currentArrow = null;
    private Animator animator = null;

    [Header("Grabbing Function")]
    public OVRInput.Controller m_Controller = OVRInput.Controller.None;

    private Quaternion contRot = Quaternion.Euler(0, 0, 0);

    private float pullValue = 0.0f;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CreateArrow(0));

    }

    private void Update()
    {
        if (!pullingHand || !currentArrow)
            return;

        pullValue = CalculatePull(pullingHand);
        pullValue = Mathf.Clamp(pullValue, 0.0f, 1.0f);

        animator.SetFloat("Blend", pullValue);
    }

    private float CalculatePull(Transform pullHand)
    {
        Vector3 direction = end.position - start.position;
        float magnitude = direction.magnitude;

        direction.Normalize();
        Vector3 diff = pullHand.position - start.position;

        return Vector3.Dot(diff, direction) / magnitude;
    }



    private IEnumerator CreateArrow(float waitTime)
    {
        // Wait
        yield return new WaitForSeconds(waitTime);

        //Create our object and child it
        GameObject arrowObject = Instantiate(arrowPref, socket);

        //Orient it
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f);
        arrowObject.transform.localEulerAngles = Vector3.zero;

        // Set it
        currentArrow = arrowObject.GetComponent<Arrow>();
    }

    public void Pull(Transform hand)
    {
        float distance = Vector3.Distance(hand.position, start.position);

        if (distance > grabThreshold)
            return;

        pullingHand = hand;
    }

    public void Release()
    {
        if (pullValue > 0.25f)
            fireArrow();

        pullingHand = null;

        pullValue = 0.0f;
        animator.SetFloat("Blend", 0.0f);

        if (!currentArrow)
        {
            StartCoroutine(CreateArrow(0.25f));
        }
    }

    private void fireArrow()
    {
        currentArrow.Fire(pullValue);
        // Call fire from Arrow
        currentArrow = null;
    }

}
