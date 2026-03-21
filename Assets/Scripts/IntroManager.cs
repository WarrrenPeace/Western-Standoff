using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Toggle ishardcore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerPrefs.GetInt("HardcoreMode") == 0)
        {ishardcore.isOn = false;}
        else
        {ishardcore.isOn = true;}        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            NextScene();
        }
    }
    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ToggleHardcoreMode() //0 is false 1 is true
    {
        if(ishardcore.isOn)
        {PlayerPrefs.SetInt("HardcoreMode",1);}
        else
        {PlayerPrefs.SetInt("HardcoreMode",0);}
    }
}
