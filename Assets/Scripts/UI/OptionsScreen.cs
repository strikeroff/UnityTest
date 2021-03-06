﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsScreen : UIScreen {

    public UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Slider soundVolumeSlider;
    public UnityEngine.UI.Slider musicVolumeSlider;
    public Toggle soundToggle;
    public Toggle musicToggle;

    public override void Show() {
        base.Show();
        slider.value = Mathf.Log(GameManager.game.settings.mouseSpeed);
        soundVolumeSlider.value = GameManager.game.settings.soundVolume;
        musicVolumeSlider.value = GameManager.game.settings.musicVolume;
        soundToggle.isOn = GameManager.game.settings.sound;
        musicToggle.isOn = GameManager.game.settings.music;
    }

    public void Apply() {
        Debug.Log("Apply options");
        GameManager.game.settings.mouseSpeed = Mathf.Exp(slider.value);
        GameManager.game.settings.soundVolume = soundVolumeSlider.value;
        GameManager.game.settings.musicVolume = musicVolumeSlider.value;
        GameManager.game.settings.sound = soundToggle.isOn;
        GameManager.game.settings.music = musicToggle.isOn;
        SoundManager.instance.UpdateVolumes();
        GameManager.instance.Save();
        LevelUI.instance.HideModal();
    }
}
