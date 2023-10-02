using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float fStartingHealth;
    [SerializeField] public AudioSource audHurt;
    [SerializeField] private GameObject objHpBar;
    private float _fHpBarScale;
    private float _fCurrentHealth;
    private static readonly int Hurt = Animator.StringToHash("hurt");
    private static readonly int Die = Animator.StringToHash("die");


    private void Start()
    {
        _fCurrentHealth = fStartingHealth;
        _fHpBarScale = objHpBar.transform.localScale.x;
    }

    private void Update()
    {
        Vector3 hpPos = objHpBar.transform.position;
        hpPos.x = transform.position.x;
        objHpBar.transform.parent.position = hpPos;
    }

    public void TakeDamage(float dmg)
    {
        _fCurrentHealth = Mathf.Clamp(_fCurrentHealth - dmg, 0, fStartingHealth);
        audHurt.Play();
        if (_fCurrentHealth > 0)
        {
            animator.SetTrigger(Hurt);
            Vector3 scale = objHpBar.transform.localScale;
            scale.x = _fHpBarScale * _fCurrentHealth / fStartingHealth;
            objHpBar.transform.localScale = scale;
        }
        else
        {
            objHpBar.transform.parent.gameObject.SetActive(false);
            animator.SetTrigger(Die);
        }
    }

    public void Dead()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
            Destroy(objHpBar.transform.parent.gameObject);
        }
    }
}