using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class fogOff : MonoBehaviour
{
    bool doWeHaveFogInScene;
    void Start() => doWeHaveFogInScene = RenderSettings.fog;
    private void OnPreRender() => RenderSettings.fog = false;
    private void OnPostRender() => RenderSettings.fog = doWeHaveFogInScene;
}
