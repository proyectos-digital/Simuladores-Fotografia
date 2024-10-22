using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script obsoleta ya no esta en uso
public class PhotoInventory : MonoBehaviour{

    public GameObject panelInventory;
    public RawImage image;
    public List<RawImage> raws;

    public Toggle tglPanel;

    void Start() {
        tglPanel.onValueChanged.AddListener(delegate {
            ToggleValueChanged(tglPanel);
        });
    }

    private void ToggleValueChanged(Toggle toggle) {
        panelInventory.SetActive(toggle.isOn);
    }

    public void GetTexture(RenderTexture texture) {
        foreach (RawImage raw in raws) {
            if (!raw.texture) {
                raw.texture = texture;
                break;
            }
        }
    }
}
