using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] Transform playertransform;
    [SerializeField] Transform playercameratransform;
    [SerializeField] Transform modelheadtransform;


    [SerializeField] float measureperiod;
    [SerializeField] float maxidlespeed;
    [SerializeField] float maxwalkspeed;

    float maxneckangle = 90;
    [SerializeField] float bodyrotationspeed;

    [SerializeField] float maxheight;
    [SerializeField] float crouchheight;
    [SerializeField] float crouchtransitionspeed;


    public class VeclocityMeasurement
    {
        public Vector3 oldposition { get; private set; }
        public Vector3 oldeulerAngles { get; private set; }
        public Vector3 newposition { get; private set; }
        public Vector3 neweulerAngles { get; private set; }

        public Vector3 veclocity { get; private set; }

        public Vector3 horizontalveclocity
        {
            get
            {
                return new Vector3(veclocity.x, 0, veclocity.z);
            }
        }
        public float horizontalangularveclocity { get; private set; }

        public float deltaTime { get; private set; }

        public VeclocityMeasurement()
        {
        }

        public void Measure(Vector3 _oldposition, Vector3 _oldeulerAngles, Vector3 _newposition, Vector3 _neweulerAngles, float _deltaTime)
        {
            oldposition = _oldposition;
            oldeulerAngles = _oldeulerAngles;
            newposition = _newposition;
            neweulerAngles = _neweulerAngles;

            deltaTime = _deltaTime;

            veclocity = (newposition - oldposition) / deltaTime;

            horizontalangularveclocity = neweulerAngles.y - oldeulerAngles.y;
            if (horizontalangularveclocity > 180)
                horizontalangularveclocity -= 360;
            if (horizontalangularveclocity < -180)
                horizontalangularveclocity += 360;
            horizontalangularveclocity /= deltaTime;
        }
    }
    VeclocityMeasurement veclocitymeasurement;
    VeclocityMeasurement cameralocalveclocitymeasurement;

    Animator animator;

    /*
     * Crouch = 3/4
     * Walk = 3/4
     * Run = 5/8
     * Turn = 1.625/90
     */
    static Dictionary<string, float> transitiondurations = new Dictionary<string, float>()
    {
        {"Crouch", 0.1f},
        {"Walk", 0.1f},
        {"Run", 0.1f},
        {"Idle", 0.2f},
        {"Crouch Idle", 0.2f},
        {"Turn Left", 0.2f},
        {"Turn Right", 0.2f},
        {"Crouch Turn Left", 0.2f},
        {"Crouch Turn Right", 0.2f}
    };
    bool IsCrouching()
    {
        return Mathf.Abs(playercameratransform.localPosition.y - crouchheight) <= 0.01f || cameralocalveclocitymeasurement.veclocity.y < 0;
    }

    private void Awake()
    {
        veclocitymeasurement = new VeclocityMeasurement();
        cameralocalveclocitymeasurement = new VeclocityMeasurement();

        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        currentanimation = null;
    }

    Vector3 bodyEulerAngles;
    float timer = 0;
    float maxhorizontalangularveclocity;
    float minhorizontalangularveclocity;
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = bodyEulerAngles;

        timer += Time.deltaTime;
        if (timer >= measureperiod)
        {
            veclocitymeasurement.Measure(veclocitymeasurement.newposition, veclocitymeasurement.neweulerAngles, playertransform.position, playercameratransform.eulerAngles, timer);
            cameralocalveclocitymeasurement.Measure(cameralocalveclocitymeasurement.newposition, cameralocalveclocitymeasurement.neweulerAngles, playercameratransform.localPosition, playercameratransform.localEulerAngles, timer);

            maxhorizontalangularveclocity = PositiveAngle(bodyEulerAngleY, GetEulerAngleY(veclocitymeasurement.horizontalveclocity)) / veclocitymeasurement.deltaTime;
            minhorizontalangularveclocity = NegativeAngle(bodyEulerAngleY, GetEulerAngleY(veclocitymeasurement.horizontalveclocity)) / veclocitymeasurement.deltaTime;

            SetAnimation();

            timer = 0;
        }

        //Set body rotation
        //Is Idle?
        if (veclocitymeasurement.horizontalveclocity.magnitude < maxidlespeed)
        {
            //Prevent Neck Angle from going beyond Max Neck Angle
            if (GetNeckSignedAngle() > maxneckangle)
                bodyEulerAngleY = headEulerAngleY + maxneckangle;
            if (GetNeckSignedAngle() < -maxneckangle)
                bodyEulerAngleY = headEulerAngleY - maxneckangle;
        }
        else
        {
            float veclocityEulerAngleY = GetEulerAngleY(veclocitymeasurement.horizontalveclocity);
            //Is Moving Forward?
            if (Mathf.Abs(SignedAngle(headEulerAngleY, veclocityEulerAngleY)) <= 112.5f)
            {
                if (PositiveAngle(bodyEulerAngleY, veclocityEulerAngleY) > PositiveAngle(bodyEulerAngleY, headEulerAngleY - 180))
                    bodyEulerAngleY = RotateTo(bodyEulerAngleY, veclocityEulerAngleY, Mathf.Clamp(veclocitymeasurement.horizontalangularveclocity - veclocitymeasurement.horizontalveclocity.magnitude * bodyrotationspeed, minhorizontalangularveclocity, 0) * Time.deltaTime);
                else
                    bodyEulerAngleY = RotateTo(bodyEulerAngleY, veclocityEulerAngleY, Mathf.Clamp(veclocitymeasurement.horizontalangularveclocity + veclocitymeasurement.horizontalveclocity.magnitude * bodyrotationspeed, 0, maxhorizontalangularveclocity) * Time.deltaTime);
            }
            //Moving Backward.
            else
            {
                veclocityEulerAngleY = veclocityEulerAngleY - 180;
                if (PositiveAngle(bodyEulerAngleY, veclocityEulerAngleY) > PositiveAngle(bodyEulerAngleY, headEulerAngleY - 180))
                    bodyEulerAngleY = RotateTo(bodyEulerAngleY, veclocityEulerAngleY, Mathf.Clamp(veclocitymeasurement.horizontalangularveclocity - veclocitymeasurement.horizontalveclocity.magnitude * bodyrotationspeed, minhorizontalangularveclocity, 0) * Time.deltaTime);
                else
                    bodyEulerAngleY = RotateTo(bodyEulerAngleY, veclocityEulerAngleY, Mathf.Clamp(veclocitymeasurement.horizontalangularveclocity + veclocitymeasurement.horizontalveclocity.magnitude * bodyrotationspeed, 0, maxhorizontalangularveclocity) * Time.deltaTime);
            }
        }

        bodyEulerAngles = transform.eulerAngles;
    }

    private void LateUpdate()
    {
        //Match model head rotation with camera
        float xnormalized;

        if (playercameratransform.eulerAngles.x > 180)
            xnormalized = (playercameratransform.eulerAngles.x - 360) * (0.5f);
        else
            xnormalized = (playercameratransform.eulerAngles.x) * (2f / 3f);
        modelheadtransform.eulerAngles = new Vector3(xnormalized, playercameratransform.eulerAngles.y, modelheadtransform.eulerAngles.z);
    }

    float bodyEulerAngleY
    {
        get { return transform.eulerAngles.y; }
        set { transform.eulerAngles = new Vector3(transform.eulerAngles.x, value, transform.eulerAngles.z); }
    }
    float headEulerAngleY
    {
        get { return playercameratransform.eulerAngles.y; }
    }
    float GetEulerAngleY(Vector3 vector)
    {
        return Vector3.SignedAngle(Vector3.forward, vector, Vector3.up);
    }
    float SignedAngle(float fromEulerAngle, float toEulerAngle)
    {
        float signedangle = toEulerAngle - fromEulerAngle;
        if (signedangle > 180)
            signedangle -= 360;
        if (signedangle < -180)
            signedangle += 360;
        return signedangle;
    }
    float Mod360(float x)
    {
        float r = x % 360;
        return r < 0 ? r + 360 : r;
    }
    float PositiveAngle(float fromEulerAngle, float toEulerAngle)
    {
        return Mod360(toEulerAngle - fromEulerAngle);
    }
    float NegativeAngle(float fromEulerAngle, float toEulerAngle)
    {
        float r = Mod360(toEulerAngle - fromEulerAngle);
        return r != 0 ? r - 360 : 0;
    }
    float RotateTo(float fromEulerAngle, float toEulerAngle, float AngularVeclocity)
    {
        if (Mod360(fromEulerAngle) == Mod360(toEulerAngle) || AngularVeclocity >= 360)
        {
            return toEulerAngle;
        }
        if (AngularVeclocity > 0)
        {
            if (Mod360(toEulerAngle - fromEulerAngle) < AngularVeclocity)
            {
                return toEulerAngle;
            }
            return fromEulerAngle + AngularVeclocity;
        }
        else
        {
            if (Mod360(toEulerAngle - fromEulerAngle) - 360 > AngularVeclocity)
            {
                return toEulerAngle;
            }
            return fromEulerAngle + AngularVeclocity;
        }
    }
    float GetNeckSignedAngle()
    {
        return SignedAngle(headEulerAngleY, bodyEulerAngleY);
    }

    void SetAnimation()
    {
        //Is not moving?
        if (veclocitymeasurement.horizontalveclocity.magnitude < maxidlespeed)
        {
            if (IsCrouching())
            {
                if (GetNeckSignedAngle() >= maxneckangle && veclocitymeasurement.horizontalangularveclocity < 0)
                {
                    PlayAnimation("Crouch Turn Left", Mathf.Abs(veclocitymeasurement.horizontalangularveclocity));
                    return;
                }
                if (GetNeckSignedAngle() <= -maxneckangle && veclocitymeasurement.horizontalangularveclocity > 0)
                {
                    PlayAnimation("Crouch Turn Right", Mathf.Abs(veclocitymeasurement.horizontalangularveclocity));
                    return;
                }

                PlayAnimation("Crouch Idle", 1);
                return;
            }
            else
            {
                if (GetNeckSignedAngle() >= maxneckangle && veclocitymeasurement.horizontalangularveclocity < 0)
                {
                    PlayAnimation("Turn Left", Mathf.Abs(veclocitymeasurement.horizontalangularveclocity));
                    return;
                }
                if (GetNeckSignedAngle() <= -maxneckangle && veclocitymeasurement.horizontalangularveclocity > 0)
                {
                    PlayAnimation("Turn Right", Mathf.Abs(veclocitymeasurement.horizontalangularveclocity));
                    return;
                }

                PlayAnimation("Idle", 1);
                return;
            }
        }

        // Is moving forward?
        float veclocityEulerAngleY = GetEulerAngleY(veclocitymeasurement.horizontalveclocity);
        if (Mathf.Abs(SignedAngle(headEulerAngleY, veclocityEulerAngleY)) <= 112.5f)
        {
            if (IsCrouching() && veclocitymeasurement.horizontalveclocity.magnitude > maxidlespeed)
            {
                PlayAnimation("Crouch", veclocitymeasurement.horizontalveclocity.magnitude);
                return;
            }
            if (veclocitymeasurement.horizontalveclocity.magnitude > maxidlespeed && veclocitymeasurement.horizontalveclocity.magnitude < maxwalkspeed)
            {
                PlayAnimation("Walk", veclocitymeasurement.horizontalveclocity.magnitude);
                return;
            }
            if (veclocitymeasurement.horizontalveclocity.magnitude > maxwalkspeed)
            {
                PlayAnimation("Run", veclocitymeasurement.horizontalveclocity.magnitude);
                return;
            }
        }
        //Moving backward
        else
        {
            if (IsCrouching() && veclocitymeasurement.horizontalveclocity.magnitude > maxidlespeed)
            {
                PlayAnimation("Crouch", -veclocitymeasurement.horizontalveclocity.magnitude);
                return;
            }
            if (veclocitymeasurement.horizontalveclocity.magnitude > maxidlespeed && veclocitymeasurement.horizontalveclocity.magnitude < maxwalkspeed)
            {
                PlayAnimation("Walk", -veclocitymeasurement.horizontalveclocity.magnitude);
                return;
            }
            if (veclocitymeasurement.horizontalveclocity.magnitude > maxwalkspeed)
            {
                PlayAnimation("Run", -veclocitymeasurement.horizontalveclocity.magnitude);
                return;
            }
        }
    }

    string currentanimation = null;
    void PlayAnimation(string animationname, float movementvalue)
    {
        if (!transitiondurations.ContainsKey(animationname))
        {
            Debug.LogWarning("Animation " + animationname + " does not exist.");
            return;
        }

        animator.SetFloat("movementvalue", movementvalue);

        if (currentanimation == null)
        {
            currentanimation = animationname;
            animator.Play(animationname);
            return;
        }

        if (IsCrouchAnimation(animationname) && !IsCrouchAnimation(currentanimation))
        {
            currentanimation = animationname;
            animator.CrossFadeInFixedTime(currentanimation, Mathf.Abs(playercameratransform.localPosition.y - crouchheight) / crouchtransitionspeed);
            return;
        }
        if (IsCrouchAnimation(currentanimation) && !IsCrouchAnimation(animationname))
        {
            currentanimation = animationname;
            animator.CrossFadeInFixedTime(currentanimation, Mathf.Abs(maxheight - playercameratransform.localPosition.y) / crouchtransitionspeed);
            return;
        }

        if (currentanimation != animationname)
        {
            currentanimation = animationname;
            animator.CrossFadeInFixedTime(currentanimation, transitiondurations[animationname]);
            return;
        }
    }

    bool IsCrouchAnimation(string animationname)
    {
        if (animationname == "Crouch") return true;
        if (animationname == "Crouch Idle") return true;
        if (animationname == "Crouch Turn Left") return true;
        if (animationname == "Crouch Turn Right") return true;
        return false;
    }
}
