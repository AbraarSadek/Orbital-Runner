using AudioSystem;
using UnityEngine;

public class SFXAgent : MonoBehaviour
{
    public AudioClip sfxClip;
    public bool randomizePitch = true;
    public float useSpecificPitch = 1.0f;
    public Vector2 randomizePitchCustomRange;
    public bool isLoopable = false;


    public void PlaySFX()
    {
        if (randomizePitch)
        {
            AudioManager.Instance.PlaySFX(sfxClip, randomizePitch);
        }
        else if (useSpecificPitch != 1.0f)
        {
            AudioManager.Instance.PlaySFX(sfxClip, exactPitch: useSpecificPitch);
        }
        else if (isLoopable)
        {
            AudioManager.Instance.PlayLoopableSFX(sfxClip);
        }
        else
        {
            AudioManager.Instance.PlaySFX(sfxClip, randomizePitchCustomRange);
        }
    }
    public void StopSFX()
    {
        AudioManager.Instance.StopLoopableSFX();
    }
    public void ChangePitch(float newPitch)
    {
        AudioManager.Instance.ChangeSFXPitchManually(newPitch);
    }
}
