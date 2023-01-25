using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public string key;
    public PianoController pianoController;
    [SerializeField] GameObject blood;

    public void PlayKey()
    {
        Vector3 cursorPos = Input.mousePosition;
        
        GameObject bloodObj = Instantiate(blood, Camera.main.ScreenToWorldPoint(cursorPos), blood.transform.rotation);
        
        bloodObj.transform.position = new Vector3(bloodObj.transform.position.x,bloodObj.transform.position.y,0f);
        bloodObj.transform.SetParent(transform);
        bloodObj.transform.localScale = new Vector3(100f, 100f, 1f);
        SpriteRenderer sp =  bloodObj.GetComponent<SpriteRenderer>();
        sp.sortingLayerName = "UI";
        sp.sortingOrder = 11;
        pianoController.PlayPiano(key);
    }
}
