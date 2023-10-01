using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeadZone
{
    public class NextLevelZone : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 10)
            {
                SceneManager.LoadScene("Level2");
            }
        }
    }
}