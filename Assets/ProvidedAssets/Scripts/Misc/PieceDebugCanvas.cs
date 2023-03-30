using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PieceDebugCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text orignalIDTxt;
    [SerializeField] TMP_Text currentIDTxt;
    public void Set(int orignalID, int currentID)
    {
        orignalIDTxt.text = orignalID.ToString();
        currentIDTxt.text = currentID.ToString();
    }
}
