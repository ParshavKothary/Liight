using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
//ShootLaser: Spawns laser beam
//</summary>
[RequireComponent(typeof(LaserBeam))]
public class ShootLaser : MonoBehaviour {

    [SerializeField]
    private Transform beamStartPositionHolder;
    [SerializeField]
    private Color laserColor;
    [SerializeField]
    [Range(1, 3)]
    private float laserStrength;

    private LaserBeam mBeam;

    // Use this for initialization
    void Start()
    {
        mBeam = GetComponent<LaserBeam>();
        mBeam.SetColor(laserColor);
        mBeam.beamDir = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        mBeam.isBeamOn = true; // beam needs to be 'powered' every frame
        mBeam.beamStartPos = beamStartPositionHolder.position;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D)) gameObject.transform.Translate(gameObject.transform.right * 0.05f);
        else if (Input.GetKey(KeyCode.A)) gameObject.transform.Translate(gameObject.transform.right * -0.05f);
    }
}
