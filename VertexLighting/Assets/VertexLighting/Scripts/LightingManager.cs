using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum LightMode{
    lit,
    unlit,
    debugLight,
    debugVert
}

public class LightingManager : MonoBehaviour{
    [Tooltip("Changes how lighting is rendered; Lit = lighting is Rendered, Unlit = Lighting not Rendered, DebugLight = Only Lighting is Rendered, DebugVert = Vert density visualiser")]
    public LightMode lightMode;

    [Space(10)]
    public Color baseLightColor = new Color(0, 0, 0, 1);
    public float secondsBetweenUpdates = 0.04f;

    [Space(10)]
    public float dynamicLightActiveDistance = 50;

    [HideInInspector] public float msThisUpdate;
    LightMode modePrevFrame;
    float updateT;
    [HideInInspector] public Camera camMain;

    private void Start(){
        camMain = Camera.main;
        LightPoint[] lights = FindObjectsOfType<LightPoint>();
        foreach (var light in lights){
            light.SetMeUp(this);
        }

        BakeLights();
    }

    private void Update(){
        updateT += Time.deltaTime;
        if(updateT > secondsBetweenUpdates){
            updateT = 0;
            StartCoroutine(UpdateLighting());
        }
    }

    public void BakeLights(){
        LightableSurface[] lightableSurfaces = FindObjectsOfType<LightableSurface>();

        foreach (var surface in lightableSurfaces){
            if(surface.staticSurface){
                surface.BakeLighting();
            }
        }
    }

    public void ClearBakedLights(){
        LightableSurface[] lightableSurfaces = FindObjectsOfType<LightableSurface>();

        foreach (var surface in lightableSurfaces){
            if(surface.staticSurface){
                surface.ClearBakedData();
            }
        }
    }

    private IEnumerator UpdateLighting(){
        DateTime start = DateTime.Now; 
        LightableSurface[] lightableSurfaces = FindObjectsOfType<LightableSurface>();
        LightPoint[] lights = FindObjectsOfType<LightPoint>();

        if(modePrevFrame != lightMode){
            UpdateMaterials(lightableSurfaces);
        }


        switch(lightMode){
            case LightMode.lit:
                foreach (var lightableSurface in lightableSurfaces){
                    lightableSurface.UpdateSurfaceLighting(this, lights);
                }    
                break;
            case LightMode.unlit:
                foreach (var lightableSurface in lightableSurfaces){
                    lightableSurface.ResetLighting();
                }   
                break;
            case LightMode.debugVert:
                foreach (var lightableSurface in lightableSurfaces){
                    lightableSurface.VisualiseVerts();
                }   
                break;
            case LightMode.debugLight:
                foreach (var lightableSurface in lightableSurfaces){
                    lightableSurface.UpdateSurfaceLighting(this, lights);
                }    
                break;
        }
        modePrevFrame = lightMode;

        DateTime end = DateTime.Now; 
        TimeSpan dur = end.Subtract(start);
        msThisUpdate = dur.Milliseconds;
        yield return null;
    }

    private void UpdateMaterials(LightableSurface[] surfaces){
        foreach (var surf in surfaces){
            foreach (var mat in surf.gameObject.GetComponent<MeshRenderer>().materials){
                switch(lightMode){
                    case LightMode.lit:
                        mat.SetFloat("_VertexOnly", 0);
                        break;
                    case LightMode.unlit:
                        mat.SetFloat("_VertexOnly", 0);
                        break;
                    case LightMode.debugVert:
                        mat.SetFloat("_VertexOnly", 1);
                        break;
                    case LightMode.debugLight:
                        mat.SetFloat("_VertexOnly", 1);
                        break;
                }
            }
        }
    }
}