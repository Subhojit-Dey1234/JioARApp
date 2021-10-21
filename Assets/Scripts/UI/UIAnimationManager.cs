using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationManager : MonoBehaviour
{
    public delegate void TextAppearHandler();
    public static event TextAppearHandler OnTextAppeared;
    public delegate void BeyBladeAppearHandler();
    public static event BeyBladeAppearHandler OnBeyBladeAppeared;
    public void TextAppeared()
    {
        OnTextAppeared?.Invoke();
    }
    public void BeyBlade()
    {
        OnBeyBladeAppeared?.Invoke();
    }

    public void PlayClashSound()
    {
        AudioManager.Instance.PlaySoundOneShot("BeyBladeClash");
    }

}
