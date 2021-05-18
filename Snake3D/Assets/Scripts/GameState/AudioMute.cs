using UnityEngine;

public class AudioMute : MonoBehaviour
{
    private bool isMute = false;

    public void MuteSound()
    {
        isMute = !isMute;
        AudioListener.pause = !isMute;
    }
       
}
