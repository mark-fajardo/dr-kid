using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject LangEnglishToggleBtn;
    public GameObject LangFilipinoToggleBtn;

    public GameObject FemaleToggleBtn;
    public GameObject MaleToggleBtn;

    void Start()
    {
        //SetupOptions();
    }

    public void SetupOptions()
    {
        SetupLanguage();
        SetupGender();
    }

    public void SetVolume (float volume)
    {
        if (volume <= -30)
        {
            volume = -80;
        }

        audioMixer.SetFloat("MainMixer", volume);
    }

    public void UpdateToEnglish()
    {
        if (LangEnglishToggleBtn.GetComponent<Toggle>().isOn == true)
        {
            LangFilipinoToggleBtn.GetComponent<Toggle>().isOn = false;
            FindObjectOfType<DB>().UpdateConfigDone("language = 0");
            return;
        }

        LangFilipinoToggleBtn.GetComponent<Toggle>().isOn = true;
        FindObjectOfType<DB>().UpdateConfigDone("language = 1");
    }

    public void UpdateToFilipino()
    {
        if (LangFilipinoToggleBtn.GetComponent<Toggle>().isOn == true)
        {
            LangEnglishToggleBtn.GetComponent<Toggle>().isOn = false;
            FindObjectOfType<DB>().UpdateConfigDone("language = 1");
            return;
        }

        LangEnglishToggleBtn.GetComponent<Toggle>().isOn = true;
        FindObjectOfType<DB>().UpdateConfigDone("language = 0");
    }

    public void UpdateToFemale()
    {
        if (FemaleToggleBtn.GetComponent<Toggle>().isOn == true)
        {
            MaleToggleBtn.GetComponent<Toggle>().isOn = false;
            UpdateGenderToFemale();
            return;
        }

        MaleToggleBtn.GetComponent<Toggle>().isOn = true;
        UpdateGenderToMale();
    }

    public void UpdateToMale()
    {
        if (MaleToggleBtn.GetComponent<Toggle>().isOn == true)
        {
            FemaleToggleBtn.GetComponent<Toggle>().isOn = false;
            UpdateGenderToMale();
            return;
        }

        FemaleToggleBtn.GetComponent<Toggle>().isOn = true;
        UpdateGenderToFemale();
    }

    public void UpdateGenderToMale()
    {
        FindObjectOfType<DB>().UpdateConfigDone("gender = 1");
    }

    public void UpdateGenderToFemale()
    {
        FindObjectOfType<DB>().UpdateConfigDone("gender = 0");
    }

    private void SetupLanguage()
    {
        List<string> ShowLanguage = FindObjectOfType<DB>().CheckLanguage();
        ShowLanguage.ForEach(x => ManageLanguage(x));
    }

    private void SetupGender()
    {
        List<string> ShowGender = FindObjectOfType<DB>().CheckGender();
        ShowGender.ForEach(x => ManageGender(x));
    }

    private void ManageLanguage(string Config)
    {
        switch (Config)
        {
            case "lang_0":
                LangEnglishToggleBtn.GetComponent<Toggle>().isOn = true;
                break;
            case "lang_1":
                LangFilipinoToggleBtn.GetComponent<Toggle>().isOn = true;
                break;
        }
    }

    private void ManageGender(string Config)
    {
        switch (Config)
        {
            case "gen_0":
                FemaleToggleBtn.GetComponent<Toggle>().isOn = true;
                break;
            case "gen_1":
                MaleToggleBtn.GetComponent<Toggle>().isOn = true;
                break;
        }
    }
}
