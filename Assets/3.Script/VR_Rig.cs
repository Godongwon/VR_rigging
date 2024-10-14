using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform VR_target;
    public Transform IK_target;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        IK_target.position = VR_target.TransformPoint(trackingPositionOffset);
        IK_target.rotation = VR_target.rotation * Quaternion.Euler(trackingRotationOffset);

    }
}


public class VR_Rig : MonoBehaviour
{
    [Range(0f, 1f)]
    public float turnSmoothness = 0.1f;
    [Space(10f)]
    public VRMap Head;
    [Space(10f)]
    public VRMap LeftHand;
    [Space(10f)]
    public VRMap RightHand;

    [Space(20f)]
    public Vector3 headBodypositionOffset;
    public float headBodyYawOffset;



    private void LateUpdate()
    {
        transform.position = Head.IK_target.position + headBodypositionOffset;
        float yaw = Head.VR_target.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);

        Head.Map();
        LeftHand.Map();
        RightHand.Map();

    }
}
