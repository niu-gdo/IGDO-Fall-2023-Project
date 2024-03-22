using UnityEngine;

/// <summary>
/// This is used to decrease entity clutter in the UI.
/// </summary>
public class DebugTextContainer
{
    public static GameObject cont = new GameObject("Debug Text Boxes");
}

/// <summary>
/// Produces a very basic debug text that can stay attached to objects.
/// </summary>
public class FloatingDebugText : MonoBehaviour
{

    public bool _debugTextActive = true;
    private string _text;
    private TextMesh _mesh;
    
    // Start is called before the first frame update
    public void Start()
    {
        // Generate the floating text box
        var go = new GameObject("Debug Text box");
        _mesh = go.AddComponent<TextMesh>();
        _mesh.text = "";
        _mesh.color = Color.black;
        // This is a wierd hack to make the text sharper
        _mesh.fontSize = 100;
        _mesh.characterSize = (float)0.05;
        
        // insert self as a child of container
        // DebugTextContainer.Start();
        go.transform.parent = DebugTextContainer.cont.transform;

    }

    // Update is called once per frame
    public void UpdateText(string text = "")
    {
        // Update the display text if needed
        if (_debugTextActive)
        {
            _mesh.text = text;
            _mesh.gameObject.transform.position = gameObject.transform.position + Vector3.up + Vector3.back;
        }
        else
        {
            if (_mesh.text != "")
            {
                _mesh.text = "";
            }
        }
    }
}
