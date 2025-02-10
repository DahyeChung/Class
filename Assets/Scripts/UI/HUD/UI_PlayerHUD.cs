using UnityEngine;

public class UI_PlayerHUD : UI_Base
{
    enum GameObjects
    {
        PlayerHealthGroup,
    }
    enum Images
    {
        PlayerHealth1,
        PlayerHealth2,
        PlayerHealth3,
        PlayerHealth4,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.UI.SetCanvas(gameObject, true);

        return true;
    }

    public void SetHP(int hp)
    {
        Transform hpBar = transform.Find("HPBar");
        hpBar.localScale = new Vector3(hp * 0.01f, 1, 1);
    }

    public void SetScore(int score)
    {
        Transform scoreText = transform.Find("Score");
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
    }

    public void SetBullet(int mag, int remain)
    {
        Transform bulletText = transform.Find("Bullet");
        bulletText.GetComponent<TMPro.TextMeshProUGUI>().text = $"{mag}/{remain}";
    }
}
