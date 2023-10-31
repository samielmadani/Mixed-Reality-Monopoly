using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteAudio : MonoBehaviour
{

    public Image oldImage;
    public Sprite newImage, oldImageSprite;

    // Audio toggle controls and sets correct sprite image
    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
            oldImage.sprite = newImage;
        } 
        if (!muted) {
            AudioListener.volume = 1;
            oldImage.sprite = oldImageSprite;
        }
    }
}
