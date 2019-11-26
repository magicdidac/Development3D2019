using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [HideInInspector] private GameManager gm;

    /** Health **/
    [Header("Health")]
    [SerializeField] private Animator healthAnimator = null;
    [SerializeField] private Image healthIndicator = null;
    [SerializeField] private List<Color> healthColors = new List<Color>();
    [HideInInspector] private int currentLifes = 0;

    /** Coins **/
    [Header("Coins")]
    [SerializeField] private Text coinsText = null;

    /** Lives **/
    [Header("Lives")]
    [SerializeField] private Text livesText = null;

    /** Death **/
    [Header("DeathMenu")]
    [SerializeField] private GameObject reviveButton = null;

    /** Extra **/
    [Header("Extra")]
    [SerializeField] private Animator fadeAnimator = null;



    private void Start()
    {

        gm = GameManager.instance;

        gm.uiController = this;

        Invoke("Refresh", .2f);

    }

    public void Refresh()
    {
        RefreshHealth();
        RefreshCoins();
        RefreshLives();
    }

    private void RefreshHealth()
    {

        if(currentLifes != gm.player.lifeController.currentLifes)
        {
            healthAnimator.SetBool("Active", true);
            CancelInvoke("DeactiveHealth");
            Invoke("DeactiveHealth", 3);

            currentLifes = gm.player.lifeController.currentLifes;

            healthIndicator.color = healthColors[Mathf.Clamp(8 - currentLifes, 0, 7)];

            healthIndicator.fillAmount = .125f * Mathf.Clamp(currentLifes, 0, 8);

            if (currentLifes <= 0)
                ActiveDead();


            if (gm.player.lifeController.currentLives < 0)
                reviveButton.SetActive(false);
            else
                reviveButton.SetActive(true);

        }
    }

    private void DeactiveHealth()
    {
        healthAnimator.SetBool("Active", false);
    }

    private void ActiveDead()
    {
        fadeAnimator.SetBool("Dead", true);
        gm.audioManager.StopSound("Song-MainTheme");
    }

    private void RefreshCoins()
    {

        coinsText.text = "" + gm.player.coinController.coins;

    }

    private void RefreshLives()
    {

        livesText.text = "" + gm.player.lifeController.currentLives;

    }

    public void ExitGame()
    {
        gm.ExitGame();
    }

    public void Revive()
    {
        gm.Revive();
        fadeAnimator.SetBool("Dead", false);
    }

}
