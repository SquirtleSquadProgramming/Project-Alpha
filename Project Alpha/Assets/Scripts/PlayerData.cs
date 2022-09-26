using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static SerialisablePlayerData SerialisedPlayer = new SerialisablePlayerData();

    public static float[] SliderValues {
        get => new float[] { MouseSensitivity.x, MouseSensitivity.y, (float)FieldOfView };
        set {
            MouseSensitivity = new Vector2(value[0], value[1]);
            FieldOfView = (int)value[2];
        }
    }

    public static Vector2 MouseSensitivity
    {
        get => new Vector2(
                    SerialisedPlayer.MouseSensitivity[0],
                    SerialisedPlayer.MouseSensitivity[1]
                );
        set => SerialisedPlayer.MouseSensitivity = new float[2] {
                value.x,
                value.y
            };
    }
    public static int Level
    {
        get => SerialisedPlayer.Level;
        set => SerialisedPlayer.Level = value;
    }
    public static int FieldOfView
    {
        get => SerialisedPlayer.FieldOfView;
        set => SerialisedPlayer.FieldOfView = value;
    }
}
