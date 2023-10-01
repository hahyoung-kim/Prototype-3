using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeadZone
{
    public class FinishZone : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 10)
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}