using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
	public void PressSelection(string _LevelName) {
		if(!gameObject.transform.GetChild(0).gameObject.activeSelf) {
			SceneManager.LoadScene(_LevelName);
		}
	}
}
