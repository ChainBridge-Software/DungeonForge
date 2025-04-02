using UnityEngine;

public class SetPlayer : MonoBehaviour
{

    public string componentName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Set component's player attribute to the player

        // Find the player in the scene
        GameObject player = GameObject.Find("Player");

        // Find the component of the current object in the scene
        Component component = GetComponent(componentName);

        // Set the player attribute of the component to the player
        component.GetType().GetProperty("player").SetValue(component, player);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
