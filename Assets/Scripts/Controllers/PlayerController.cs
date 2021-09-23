using Game.Core.MoveSystem;
using Game.Singletones;
using System.Collections;
using UnityEngine;

namespace Game.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private new MeshRenderer renderer;

        private Vector3 _previousPosition;
        private Vector3 _originPosition;
        private Vector3 _targetPosition;
        private float _moveTime;

        private float _timeInterpolation = 1f;
        private Coroutine _executedCoroutine;

        private bool _isTwin;

        private void Start()
        {
            _timeInterpolation = GameManager.GameManagerInstance.SettingsConfig.timeInterpolation;
        }

        public void Init(bool isTwin)
        {
            _isTwin = isTwin;

            if (!_isTwin)
            {
                GameManager.GameManagerInstance.AbilitiesController.InitPlayer(this);
            }
            else
            {
                GameManager.GameManagerInstance.AbilitiesController.InitTwin(this);
            }
        }

        private void OnDestroy()
        {
            if (!_isTwin)
            {
                GameManager.GameManagerInstance.AbilitiesController.ClearPlayerEvents();
            }
        }

        public void SetColor(Color color)
        {
            renderer.material.color = color;
        }

        public void MoveTo(MoveData data)
        {
            if (_executedCoroutine != null)
            {
                StopCoroutine(_executedCoroutine);
            }

            _targetPosition = transform.position;

            var x = Quaternion.AngleAxis(data.angleForMove, new Vector3(1, 0, 0)).x * data.speed;
            var z = Quaternion.AngleAxis(data.angleForMove + 180, new Vector3(0, 0, 1)).z * data.speed;

            _targetPosition += Vector3.right * x + Vector3.forward * z;
            _originPosition = transform.position;
            _moveTime = 0;

            if (!_isTwin)
            {
                GameManager.GameManagerInstance.AbilitiesController.SetSpeed(data.speed);
            }

            _executedCoroutine = StartCoroutine(InterpolatedMove());
        }

        private IEnumerator InterpolatedMove()
        {
            while (Vector3.Distance(transform.position, _targetPosition) > 0)
            {
                _moveTime += Time.deltaTime;
                _previousPosition = transform.position;
                transform.position = Vector3.Lerp(_originPosition, _targetPosition, _moveTime / _timeInterpolation);
                
                if (!_isTwin)
                { 
                    GameManager.GameManagerInstance.AbilitiesController.
                        AddReachedDistance(Vector3.Distance(_previousPosition, transform.position));
                }

                yield return null;
            }
        }

        public void InterruptMove()
        {
            if (_executedCoroutine != null)
            {
                StopCoroutine(_executedCoroutine);
            }
        }
    }
}