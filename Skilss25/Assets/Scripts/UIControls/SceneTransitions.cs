using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;
    public Button changeSceneButton;
    private bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        if (changeSceneButton != null)
        {
            changeSceneButton.onClick.AddListener(OnButtonClicked);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (end)
        {
            StartCoroutine(LoadScene());
            end = false; // Reset the bool to prevent repeated transitions
        }
    }

    void OnButtonClicked()
    {
        end = true;
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetBool("end", true); // Assuming "end" is a boolean parameter in the Animator
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
