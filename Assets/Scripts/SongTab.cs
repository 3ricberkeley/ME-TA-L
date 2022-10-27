using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SongTab : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    #region Tab_variables
    RectTransform rt;
    #endregion

    #region Unity_functions
    // Enlarge the tab when selected
    public void OnSelect(BaseEventData eventData)
    {
        rt.sizeDelta = new Vector2(1000, 1000);
    }

    // Enlarge the tab when selected
    public void OnDeselect(BaseEventData eventData)
    {
        rt.sizeDelta = new Vector2(600, 600);
    }

    // Switch to the song's scene
    public void OnEnter()
    {
        Debug.Log("enter pressed!");
        SceneManager.LoadScene(transform.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }
    #endregion
}
