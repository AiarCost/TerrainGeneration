using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TerrainGenerator terrainGen;

    public void HeightInputChange(TMP_InputField Input)
    {
        int InputInt;
        bool success = int.TryParse(Input.text, out InputInt);
        if (success)
        {
            terrainGen.depth = InputInt;
        }
    }

    public void ScaleInputChange(TMP_InputField Input)
    {
        int InputInt;
        bool success = int.TryParse(Input.text, out InputInt);
        if (success)
        {
            terrainGen.scale = InputInt;
        }
    }

    public void TiersInputChange(TMP_InputField Input)
    {
        int InputInt;
        bool success = int.TryParse(Input.text, out InputInt);
        if (success)
        {
            terrainGen.Tiers = InputInt;
        }
    }

    public void TierFilterToggle(Toggle toggleStatus)
    {
        terrainGen.TieredHeight = toggleStatus.isOn;
    }

    public void IslandFilterToggle(Toggle toggleStatus)
    {
        terrainGen.IslandHeight = toggleStatus.isOn;
    }
}
