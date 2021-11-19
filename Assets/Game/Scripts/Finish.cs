using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class Finish : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent(out Player.Player _player)) {
                RestartLevel();
            }
        }

        private void RestartLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GC.Collect();
        }
    }
}
