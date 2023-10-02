using UnityEngine;

namespace DeadZone
{
    public class DeadZone : MonoBehaviour
    {
        public Transform resetTransform;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 10)
            {
                other.gameObject.GetComponent<Health>().reborn();
                other.gameObject.transform.position = resetTransform.position;
            }
        }
    }
}