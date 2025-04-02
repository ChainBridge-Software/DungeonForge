using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
    public Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        // Find the main camera in the scene
        Camera mainCamera = Camera.main;

        // Set the render camera of the canvas to the main camera
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = mainCamera;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
