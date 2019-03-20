using UnityEngine;
using UnityEngine.UI;
public class TextChange : MonoBehaviour {
    private bool isActive = false;
    private Text tex;
    public string ActivateText;
    public string UnActivateText;
    private void Start()
    {
        tex = GetComponent<Text>();
    }

    public void SetText()
    {
        if (isActive)
        {
            tex.text = ActivateText;
        }
        else
        {
            tex.text = UnActivateText;
        }
        isActive = !isActive;
    }
    
}
