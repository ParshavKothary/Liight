using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInteract : MonoBehaviour {

    protected Dictionary<int, LaserBeam> beamIOMap;

	// Use this for initialization
	void Awake () {
        beamIOMap = new Dictionary<int, LaserBeam>();
	}

    public virtual void SetInput(int sourceID, LaserHit hit) { }
}
