using UnityEngine;
using UnityEngine.UI;

public class ExampleDataShower : MonoBehaviour{
    public Text dataDisplay;
    public LightingManager lm;

    float updateT;

    void Update(){
        updateT += Time.deltaTime;

        if(updateT > 0.05f){
            dataDisplay.text = $"Current Mode: {lm.lightMode}  |  Lighting Time: {lm.msThisUpdate}MS  |  FPS: {(1.0f / Time.deltaTime).ToString("F0")}  |  Secs Between Updates: {lm.updatesPerSecond.ToString("F3")}";
            updateT = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            switch (lm.lightMode){
                case LightMode.lit:
                    lm.lightMode = LightMode.unlit;
                    break;
                case LightMode.unlit:
                    lm.lightMode = LightMode.debugLight;
                    break;
                case LightMode.debugLight:
                    lm.lightMode = LightMode.debugVert;
                    break;
                case LightMode.debugVert:
                    lm.lightMode = LightMode.lit;
                    break;
            }
        }

        if(Input.GetKey(KeyCode.UpArrow)){
            lm.updatesPerSecond += 0.1f * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            lm.updatesPerSecond -= 0.1f * Time.deltaTime;
            if(lm.updatesPerSecond < 0){
                lm.updatesPerSecond = 0;
            }
        }
    }
}