// Written by Nils Ebert. 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ChangeSceneAfterDuration : MonoBehaviour
{
    
    [SerializeField] float duration;

    [SerializeField] Animator transitionAnim;

    [SerializeField] float transitionTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(duration);
        
        transitionAnim.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene((0));
    }
}
