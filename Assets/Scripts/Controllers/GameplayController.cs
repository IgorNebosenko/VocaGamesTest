using Game.Core.Ablilities;
using Game.Core.Interfaces;
using Game.Core.MoveSystem;
using Game.ScriptableObjects.Abilities;
using Game.Singletones;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controllers
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private Transform rootPlayers;
        [SerializeField] private PlayerController playerPrefab;

        private Queue<MoveData> _queueMoves;
        private Queue<MoveData> _queueCurrentMoves;

        private PlayerController _playerController;
        private PlayerController _twinController;

        private float _timeInterpolation;
        private float _timePassed;

        public void Init()
        {
            GameManager.GameManagerInstance.InputController.buttonRespawnClicked += OnRespawnButtonClicked;

            _playerController ??= SpawnPlayer(false);
            _twinController ??= SpawnPlayer(true);

            _twinController.gameObject.SetActive(false);

            _queueMoves = new Queue<MoveData>();
            _queueCurrentMoves = new Queue<MoveData>();

            _timeInterpolation = GameManager.GameManagerInstance.SettingsConfig.timeInterpolation;
        }

        private void OnDestroy()
        {
            GameManager.GameManagerInstance.InputController.buttonRespawnClicked -= OnRespawnButtonClicked;
        }

        private void Update()
        {
            //TODO: new system of ability

            _timePassed += Time.deltaTime;

            if (_timePassed >= _timeInterpolation)
            {
                _timePassed = 0f;

                var moveData = MoveData.GenerateRandom();
                //var ability = GetAbility();

                _queueCurrentMoves.Enqueue(moveData);
                //_queueCurrentAbilites.Enqueue(ability);

                _playerController.MoveTo(moveData);
                //_playerController.SetAbility(ability);

                if (_queueMoves.Count != 0)
                {
                    _twinController.MoveTo(_queueMoves.Dequeue());
                    //_twinController.SetAbility(_queueAbilities.Dequeue());
                }
            }
        }

        private void OnRespawnButtonClicked()
        {
            _twinController.gameObject.SetActive(true);

            _queueMoves = _queueCurrentMoves;
            _queueCurrentMoves = new Queue<MoveData>();

            GameManager.GameManagerInstance.AbilitiesController.Respawn();

            _playerController.InterruptMove();
            _twinController.InterruptMove();

            _playerController.transform.position = Vector3.zero;
            _twinController.transform.position = Vector3.zero;
        }

        private PlayerController SpawnPlayer(bool isTwin)
        {
            var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, rootPlayers)
                .GetComponent<PlayerController>();

            player.Init(isTwin);

            return player;
        }
    }
}