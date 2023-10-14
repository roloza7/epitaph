using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationEvents : MonoBehaviour
{
    [SerializeField] private LevelGeneration levelGeneration;

    public void OnUIFadeoutEnd() {
        levelGeneration.PlaceEnemies();
    }
}
