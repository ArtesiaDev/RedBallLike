using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Config _config;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private GameObject _playerPiece;

        private bool _isGrounded;
        private bool _isGameWin;

        private void Update()
        {
            if (SimpleInput.GetButtonDown(Config.JUMP_BUTTON) && _isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * _config.JumpForce, ForceMode2D.Impulse);
                _isGrounded = false;
            }

            if (Mathf.Abs(_rigidbody.velocity.x) < _config.MaxSpeed)
            {
                float input = default;
                if (SimpleInput.GetButton(Config.RIGHT_BUTTON))
                    input = 1;
                else if (SimpleInput.GetButton(Config.LEFT_BUTTON))
                    input = -1;

                _rigidbody.AddForce(Vector2.right * (input * _config.Acceleration));
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _isGrounded = true;

            if (!other.gameObject.TryGetComponent<Enemy>(out _))
                return;

            for (var i = 0; i < _config.PlayerPieceCount; i++)
            {
                var position = transform.position + Random.insideUnitSphere;
                var piece = Instantiate(_playerPiece, position, transform.rotation);
                if (piece.TryGetComponent<Rigidbody2D>(out var pieceRb))
                    pieceRb.AddForce(Random.insideUnitCircle * _config.PlayerPieceImpulse, ForceMode2D.Impulse);
            }

            GameOver();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Finish>(out _))
                WinGame();
        }

        private void OnCollisionExit2D() =>
            _isGrounded = false;

        private async void GameOver()
        {
            gameObject.SetActive(false);
            await Task.Delay(TimeSpan.FromSeconds(3f));
            Reload();
        }

        private async void WinGame()
        {
            if (_isGameWin)
                return;

            _isGameWin = true;
            await Task.Delay(TimeSpan.FromSeconds(1f));
            Reload();
        }

        private void Reload()
        {
            if (!Application.isPlaying)
                return;

            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    }
}