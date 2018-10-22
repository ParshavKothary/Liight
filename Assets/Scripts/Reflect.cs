using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : LaserInteract {
    
	public override void SetInput(int sourceID, LaserHit hit) {
        if (!beamIOMap.ContainsKey(sourceID))
        {
            GameObject newBeam = new GameObject();
            newBeam.AddComponent<LaserBeam>();
            beamIOMap.Add(sourceID, newBeam.GetComponent<LaserBeam>());
        }
        beamIOMap[sourceID].isBeamOn = true;
        beamIOMap[sourceID].beamStartPos = hit.hitPosition;
        beamIOMap[sourceID].beamDir = Vector3.Reflect(hit.inDirection, hit.hitNormal);
        beamIOMap[sourceID].SetColor(hit.lightColor);
	}
}
