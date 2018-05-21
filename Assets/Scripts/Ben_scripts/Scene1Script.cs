using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Script : MonoBehaviour {
    public string Scene1Name;


	public void OnClick () {
        SceneManager.LoadScene(Scene1Name);
    }
}
