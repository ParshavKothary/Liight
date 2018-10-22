using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
//LaserEmit: Spawn and control one single laser beam
//</summary
public class LaserBeam : MonoBehaviour {
    
    //the cylinder that becomes the laser beam
    private GameObject laserBeamObject;

    //used to control the color and intensity of the beam
    private Material laserMaterial;

    private int mID;
    private Color mColor;

    public Vector3 beamStartPos;
    public Vector3 beamDir;
    public bool isBeamOn;

    private LaserHit mOutputHit;

    private void Awake()
    {
        // setup defaults
        mOutputHit = new LaserHit();
        mColor = Color.black;
        mID = gameObject.GetInstanceID();
        // setup laserbeam object
        laserBeamObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(laserBeamObject.GetComponent<CapsuleCollider>()); // no collider!
        laserBeamObject.transform.Rotate(90, 0, 0);
        laserMaterial = new Material(Shader.Find("Standard")); // setup material
        laserMaterial.EnableKeyword("_EMISSION");
        laserBeamObject.GetComponent<Renderer>().receiveShadows = false; // dont cast and receive shadows
        laserBeamObject.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        laserBeamObject.GetComponent<Renderer>().material = laserMaterial;
        // turn off by default
        laserBeamObject.SetActive(false);
        isBeamOn = false;
    }

    // Update is called once per frame
    // Beam needs power every frame (turns off at the end off update)
    // Powered on through public variable isBeamOn
    void Update()
    {
        laserBeamObject.SetActive(isBeamOn);
        if (!isBeamOn)
        {
            Debug.Log("beam off");
            return;
        }

        RaycastHit hit;
        Vector3 end = beamStartPos + (beamDir * 30);
        if (Physics.Raycast(beamStartPos, beamDir, out hit, 30f))
        {
            end = hit.point;
            LaserInteract temp = hit.collider.gameObject.GetComponent<LaserInteract>();
            Debug.Log(hit.collider.gameObject.name);
            if (temp != null) // hit something that interacts with laser beam
            {
                mOutputHit.inDirection = beamDir;
                mOutputHit.hitPosition = hit.point;
                mOutputHit.hitNormal = hit.normal;
                temp.SetInput(mID, mOutputHit); // send our output hit info
            }
        }
        Vector3 dist = end - beamStartPos;
        Quaternion rot = new Quaternion();
        rot.SetLookRotation(Vector3.up, beamDir);
        laserBeamObject.transform.localScale = new Vector3(0.025f, dist.magnitude * 0.5f, 0.025f);
        laserBeamObject.transform.position = beamStartPos + (dist * 0.5f);
        laserBeamObject.transform.rotation = rot;

        isBeamOn = false;
    }

    public void SetColor(Color c)
    {
        mColor = c;
        mOutputHit.lightColor = c;
        laserMaterial.SetColor("_Color", c);
        laserMaterial.SetColor("_EmissionColor", c * 2f);
    }

}
