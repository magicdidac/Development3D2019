using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject healthShieldInfoPanel = null;
    [SerializeField] private GameObject gunInfoPanel = null;
    [SerializeField] private GameObject deadPanel = null;
    [SerializeField] private TMP_Text loaderText = null;
    [SerializeField] private TMP_Text ammoText = null;

    [SerializeField] private Image healthSlider = null;
    [SerializeField] private TMP_Text healthText = null;

    [SerializeField] private Image shieldSlider = null;
    [SerializeField] private TMP_Text shieldText = null;

    [SerializeField] private GameObject enableActionInfo = null;
    [SerializeField] private TMP_Text enableActionText = null;
    [SerializeField] private GameObject unableActionInfo = null;
    [SerializeField] private TMP_Text unableActionText = null;

    [SerializeField] private Animation fade = null;

    [HideInInspector] private GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void Update()
    {
        RefreshPlayerStats();
    }

    private void RefreshPlayerStats()
    {
        SetHealth(gm.player.dmgShield.health);
        SetShield(gm.player.dmgShield.shield);
        SetAmoText(gm.player.gun.gunAmmo, gm.player.gun.ammo);

        if (gm.player.dmgShield.health <= 0 && healthShieldInfoPanel.activeSelf)
            Dead();

    }

    public void SetAmoText(int ammo, int loaderAmmo)
    {
        loaderText.text = "" + ammo;

        ammoText.text = " / "+loaderAmmo;
    }

    public void SetActiveGunInfo()
    {
        gunInfoPanel.SetActive(true);
    }

    public void SetHealth(int amount)
    {
        healthText.text = "" + amount;
        healthSlider.fillAmount = (amount + 0.0f) / 100;
    }

    public void SetShield(int amount)
    {
        shieldText.text = "" + amount;
        shieldSlider.fillAmount = (amount+0.0f) / 100;
    }

    public void Dead()
    {

        healthShieldInfoPanel.SetActive(false);
        gunInfoPanel.SetActive(false);
        deadPanel.SetActive(true);

        gm.audioManager.PlaySound("Dead");

        Invoke("Pause", .1f);

    }

    private void Pause()
    {
        Time.timeScale = 0;
    }

    public void Revive()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        Fade(); //Black

        healthShieldInfoPanel.SetActive(true);
        deadPanel.SetActive(false);
        if (gm.player.haveGun)
            gunInfoPanel.SetActive(true);

        gm.Revive();

    }

    public void Fade()
    {
        fade.CrossFade("Black", 0);
    }

    public void UnFade()
    {
        fade.CrossFade("Blank", 0);
    }
    

    public void ActiveActionInfo(InteractMessage i)
    {
        if (i.canInteract)
        {
            enableActionInfo.SetActive(false);
            enableActionInfo.SetActive(true);
            enableActionText.text = i.message;
        }
        else if (i.message != "")
        {
            enableActionInfo.SetActive(false);
            unableActionInfo.SetActive(true);
            unableActionText.text = i.message;
        }
        else
            DesactiveActionInfo();
    }

    public void DesactiveActionInfo()
    {
        enableActionInfo.SetActive(false);
        unableActionInfo.SetActive(false);
    }

}
