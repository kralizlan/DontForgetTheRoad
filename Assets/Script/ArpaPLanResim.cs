using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArpaPLanResim : MonoBehaviour
{

    [SerializeField] GameObject ArkaPlan;
    [SerializeField] Sprite[] Resimler;
    void Start()
    {
        int seviye = PlayerPrefs.GetInt("SelectedLevel", 0);
        ArkaPlan.GetComponent<Image>().sprite = Resimler[seviye-1];
    }


}
