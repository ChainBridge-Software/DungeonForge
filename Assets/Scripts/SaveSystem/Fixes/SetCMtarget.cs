using UnityEngine;
using Unity.Cinemachine;

public class SetCMtarget : MonoBehaviour
{

    public string targetName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the target of the Cinemachine Virtual Camera to the target
        GameObject target = GameObject.Find(targetName);
        var vcam = GetComponent<CinemachineCamera>();
        vcam.LookAt = target.transform;
        vcam.Follow = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
