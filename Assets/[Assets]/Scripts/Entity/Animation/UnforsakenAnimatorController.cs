using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnforsakenAnimatorController : MonoBehaviour
{
    [SerializeField] float measureperiod;
    [SerializeField] float animationspeed;

    AnimatorController.VeclocityMeasurement veclocitymeasurement = new AnimatorController.VeclocityMeasurement();

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("run");
    }

    float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= measureperiod)
        {
            veclocitymeasurement.Measure(veclocitymeasurement.newposition, veclocitymeasurement.neweulerAngles, transform.position, transform.eulerAngles, timer);
            timer = 0;
        }

        animator.speed = veclocitymeasurement.horizontalveclocity.magnitude * animationspeed;
    }
}
