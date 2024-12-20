# Vertex Based Lighting, for Unity URP
![](https://github.com/Volutedberet/VertexLighting/blob/main/VertexGif.gif)

## This is a lighting system, that uses vertex colors to fake lighting, which was a technique used by older games like Silent Hill 1
![](https://github.com/Volutedberet/VertexLighting/blob/main/LightExample.png)
### It produces some nice and blocky lighting, perfectly fitting for making games that look like they belong on the ps1

### Key Features:
```
-Realtime Lights:
    -Updated each tick, lighting up static and nonstatic surfaces
    (Intended for places where the light is always on, and the performance cost doesn't matter)

-Baked Lights:
    -On Load, bakes it's light onto static surfaces
    -Baked lighting can be temporarily overwritten by realtime lights with more intensity
    (Intended for lighting background enviroments, since it's lighting doesn't translate to dynamic surfaces that move)

-Static Lights:
    -On Load, bakes just like a static, but can act as a realtime light, when the main camera is close
    -The activation distance is changable
    (Intended for main enviroment lighting, since they are a middle ground between static and realtime, offering performance even when there is a lot of them, but they can still cast lighting on dynamic surfaces when needed)

-4 lighting modes:
    -lit (Lights rendered as normal)
    -Unlit (No lighting)
    -Light Debug (Only the lighting is rendered)
    -Vertex Debug (Displays a gradient that helps you see how geometry dense the area is)

-Adjustable update rate, to get even chunkier results

-Custom main shader, that supports detail textures

-3 Demo Scenes, showcasing a setup for each lighting mode
```


# Quick Guide:
### Tip: The lightings resolution is 1-1 to the resolution of your meshes, so to get the intended blocky lighting, you must use low poly count meshes

## Setting up your enviroment:
```
This system uses a custom shader, called Uber. 
    All your materials must use either this shader, or use a shader that multiplies the final color with the vertex color

The lighting is handled by the LightingManager component, there is a drag and drop prefab that has this component on it, but you don't have to use that
    On this component you can change:
        -Base Light Color (What color is displayed when no light is found on an area. Set it to fully black if you want no lighting)
        -Updates per Second (How many seconds need to pass between each lighting update. Set it to 0 to update lighting each frame)
        -Dynamic Light Distance (How close the object marked as Main Camera has to be to a dynamic light for it to activate)
        -Lighting mode (mainly a debug tool, but can be useful to check for dark spots and vertex density)
If this component isn't on an object in the scene, the lighting wont update

The lighting system only uses objects that have the "LightableSurface" component on them
    This component allows you to mark the object as static
        -Marking an object static means that it wont ever move
        -Marking an object static will allow baking lights onto the surface
Make sure to add it to any object you want lighting on
```

## Setting up your lights
```
Adding a light is extremely simple, you just add the LightPoint component to an object
    On this component you can change:
        -Radious / Insensity (How far, and how strong the light is)
        -Light Mode (Changes how the light behaves, see below what each mode does)
            -Realtime: Updates every tick, lights up static and dynamic surfaces
            -Baked: Lights up static surfaces on start, and the lighting is stored
            -Dynamic: If camera is close, acts as a realtime light, but it does a bake on start that it falls back to once the camera is farther away
```

## You are free to use and modify this code as you wish, crediting is not needed but it's appreciated :3