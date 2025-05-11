using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public bool peony;
    public bool columbine;
    public bool bluebell;

    public Image peonyImage;
    public Image columbineImage;
    public Image bluebellImage;

    public static PlayerInventory Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        EmptyInventory();
        UpdateUI();
    }

    public void FillInventory()
    {
        peony = true;
        columbine = true;
        bluebell = true;
        UpdateUI();
    }

    public void EmptyInventory()
    {
        peony = false;
        columbine = false;
        bluebell = false;
        UpdateUI();
    }

    public void ChangePeony(bool value)
    {
        peony = value;
        UpdateUI();
    }

    public void ChangeColumbine(bool value)
    {
        columbine = value;
        UpdateUI();
    }

    public void ChangeBluebell(bool value)
    {
        bluebell = value;
        UpdateUI();
    }

    public void UpdateUI()
    {
        peonyImage.gameObject.SetActive(peony);
        columbineImage.gameObject.SetActive(columbine);
        bluebellImage.gameObject.SetActive(bluebell);
    }

}
