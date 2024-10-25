using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volumerSlider;


    public void OnStart()
    {
        SceneManager.LoadScene("The_Game");
        Debug.Log("start");
    }

    public void OnExit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void OnBack()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void OnOptions()
    {
        
        
        
        //SceneManager.LoadScene("Scene");
        Debug.Log("blah");
    }

    public void SetMusicVolume()
    {
        float volume = volumerSlider.value;
        mixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

}
