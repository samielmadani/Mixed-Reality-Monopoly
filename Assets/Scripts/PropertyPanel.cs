using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PropertyPanel : MonoBehaviour
{
    public TMP_Text PanelName;

    public TMP_Text PanelOwner;

    public int BuyerSeller = -1;

    // Sets the text for property panel.
    void Update() {
        PlayerSetup.PropertyToBuySell = PanelName.text;
    }


}
