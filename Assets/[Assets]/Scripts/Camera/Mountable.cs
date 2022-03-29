using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


[RequireComponent( typeof(PositionConstraint), typeof(RotationConstraint) )]
public class Mountable : MonoBehaviour
{
    [SerializeField] PositionConstraint positionconstraint;
    [SerializeField] RotationConstraint rotationconstraint;

    public void Mount(Transform target)
    {
        ConstraintSource constraint = new ConstraintSource();
        constraint.sourceTransform = target;
        constraint.weight = 1;

        positionconstraint.SetSource(0, constraint);
        rotationconstraint.SetSource(0, constraint);
    }
}
