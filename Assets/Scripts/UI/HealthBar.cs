using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image iHpBar;
    [SerializeField] TextMeshProUGUI hpText;
    
    private GameObject _player;
    private float _fOriginalScale; // reference to the original scale

    public static HealthBar Instance;

    void Awake()
    {
        Instance = this;
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Start()
    {
        _fOriginalScale = iHpBar.transform.localScale.x;
    }

    public void OnHpChange()
    {
        float currentHp = _player.GetComponent<Health>().currentHealth;
        float maxHp = _player.GetComponent<Health>().startingHealth;
        hpText.text = $"{currentHp} / {maxHp}";
        var transform1 = iHpBar.transform;
        Vector3 scale = transform1.localScale;
        scale.x = _fOriginalScale * currentHp / maxHp;
        transform1.localScale = scale;
    }
}