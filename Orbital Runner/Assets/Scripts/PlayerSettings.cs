using System.Diagnostics;

public static class PlayerSettings
{
    //TODO: Use PlayerPrefs or whatever persistent system to save these values and let the user choose.
    public static float MasterVolume = 0.7f;
    public static float MusicVolume = 0.7f;
    public static float SfxVolume = 0.7f;

    public static void SetSpecificValue(string param, float newValue) //TODO: param should be an enum.
    {
        string lcParam = param.Split(',')[0].ToLower();
        switch (lcParam)
        {
            case "mastervolume":
                MasterVolume = newValue;
                break;
            case "musicvolume":
                MusicVolume = newValue;
                break;
            case "sfxvolume":
            case "rollervolume":
                SfxVolume = newValue;
                break;
        }
    }
    public static float GetSpecificValue(string param)
    {
        string lcParam = param.Split(',')[0].ToLower();
        return lcParam switch
        {
            "mastervolume" => MasterVolume,
            "musicvolume" => MusicVolume,
            "sfxvolume" or "rollervolume" => SfxVolume,
            _ => 0f,
        };
    }
}

