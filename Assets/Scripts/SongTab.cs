using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

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

    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }
    #endregion
}
