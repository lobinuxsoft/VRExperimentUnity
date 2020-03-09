using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VRRig : MonoBehaviour
{
    //[SerializeField] private float turnSmoothness = 5;
    [SerializeField] private Transform headConstraint;
    //[SerializeField] private Transform baseTransform;
    [SerializeField] private Vector3 headBodyOffset;
    [SerializeField] private VRMap baseMap;
    [SerializeField] private VRMap head;
    [SerializeField] private VRMap leftHand;
    [SerializeField] private VRMap rightHand;

    //[SerializeField] private Quaternion desiredBaseRot;
    
    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        //transform.position = headConstraint.position + headBodyOffset;
        //transform.forward = Vector3.ProjectOnPlane(headConstraint.up, baseTransform.up).normalized;
        //transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, baseTransform.up).normalized, Time.deltaTime * turnSmoothness);

        //desiredBaseRot = Quaternion.FromToRotation(transform.forward, baseTransform.forward);
        //transform.rotation = Quaternion.Lerp(transform.rotation, desiredBaseRot, Time.deltaTime * 1.5f);

        baseMap.Map();
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;

    [Range(0f, 100f)] public float smoothLerpPosition = 10;
    public Vector3 trackingPositionOffset;
    [Range(0f, 100f)] public float smoothLerpRotation = 10;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        var desirePos = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.position = Vector3.Lerp(rigTarget.position, desirePos, Time.deltaTime * smoothLerpPosition);
        var desireRot = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        rigTarget.rotation = Quaternion.Lerp(rigTarget.rotation, desireRot, Time.deltaTime * smoothLerpRotation);
    }
}
