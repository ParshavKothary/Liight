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
        float move = 0f;
        if (Input.GetKey(KeyCode.D) || MyInput.GetKey(MyKeyCode.Right)) move = 1f;
        else if (Input.GetKey(KeyCode.A) || MyInput.GetKey(MyKeyCode.Left)) move = -1f;
        gameObject.transform.Translate(gameObject.transform.right * 0.05f * move);
    }
}
