using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedLevelChecker : MonoBehaviour
{
    public Button[] levels;
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("Levels"))
        {
            PlayerPrefs.SetInt("Levels", 1);
        }
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }
        CheckCompleted();
    }
    private void CheckCompleted()
    {
        int z = PlayerPrefs.GetInt("Levels");
        Debug.Log(z);
        for (int i = 0; i < z; i++)
        {
            if (i<levels.Length)
            {
                levels[i].interactable = true;
            }
        }
    }
}
