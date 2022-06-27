using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class CamAsset : PlayableAsset
{
    [Header("Custom ±¸Çö")]
    public ExposedReference<Camera> cam;
    public float fovValue;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        ScriptPlayable<CamBehaviour> behaviour = ScriptPlayable<CamBehaviour>.Create(graph);

        CamBehaviour scb = behaviour.GetBehaviour();
        
        Camera _cam = cam.Resolve(graph.GetResolver());
        scb.cam = _cam;
        return behaviour;
    }
}
