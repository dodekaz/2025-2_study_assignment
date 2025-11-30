using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TestScript : MonoBehaviour
{
    private Button MyButton;
    private Image MyImage;
    public TextMeshProUGUI MyText;
    public void HelloWorld(){
        Debug.Log("hello");
    }

    void Awake() {
        MyImage = GameObject.Find("Image").GetComponent<Image>();
    
    }

    private void Start() {
        MyImage.color = Color.Lerp(Color.green, Color.yellow, 0.3f);

        MyText.text = "Welcome to Test Scene!";
    }
}
