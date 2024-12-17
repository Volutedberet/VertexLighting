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
    public LightMode lightMode;

    [Space(10)]
    public Color baseLightColor = new Color(0, 0, 0, 1);
    public float updatesPerSecond = 0.075f;

    [Space(10)]
    public bool rebakeLightsOnStart;

    [HideInInspector] public float msThisUpdate;
    LightMode modePrevFrame;
    float updateT;

    private void Start(){
        if(rebakeLightsOnStart){  
            LightableSurface[] lightableSurfaces = FindObjectsOfType<LightableSurface>();
            
            foreach (var surface in lightableSurfaces){
                if(surface.staticSurface){
                    surface.BakeLighting();
                }
            }
        }
    }

    private void Update(){
        updateT += Time.deltaTime;
        if(updateT > updatesPerSecond){
            updateT = 0;
            StartCoroutine(UpdateLighting());
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
                    lightableSurface.UpdateLighting(this, lights);
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
                    lightableSurface.UpdateLighting(this, lights);
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