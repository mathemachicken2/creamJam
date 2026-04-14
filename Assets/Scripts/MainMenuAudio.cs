using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.Play("OpenPhone");
    }
}