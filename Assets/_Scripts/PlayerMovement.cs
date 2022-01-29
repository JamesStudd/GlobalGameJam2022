using UnityEngine;

namespace _Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationFactorPerFrame;
        
        // declare reference variables
        private CharacterController _characterController;
        private PlayerInput _playerInput;
        private MovementPlayback _movementPlayback;

        // variables to store player input values
        private Vector2 _currentMovementInput;
        private Vector3 _currentMovement;
        private Vector3 _appliedMovement;
        private bool _isMovementPressed;

        // gravity variables
        private float _gravity = -9.8f;
        private float _groundedGravity = -.05f;
        private float _maxJumpHeight = 2.0f;
        private float _maxJumpTime = 0.75f;
        
        private float _initialJumpVelocity;

        // jumping variables
        private bool _isJumpPressed = false;
        private bool _isJumping = false;
        private int _jumpCount = 0;
        
        private Coroutine _currentJumpResetRoutine = null;

        private bool _hasStartedRecording = false;

        private bool CanMove => FeatureLocker.PlayerInputEnabled;
        
        // Awake is called earlier than Start in Unity's event life cycle
        private void Awake()
        {
            // initially set reference variables
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
            _movementPlayback = GetComponent<MovementPlayback>();

            _playerInput.OnReplay += () => enabled = false;

            SetupJumpVariables();
        }

        public void Move(Vector2 input)
        {
            _currentMovementInput = input;

            _currentMovement.x = _currentMovementInput.x * _movementSpeed;
            _isMovementPressed = _currentMovementInput.x != 0;

            if (_isMovementPressed)
            {
                TryStartRecording();
            }
        }

        public void Jump(Vector2 input)
        {
            _isJumpPressed = input.y != 0;
            TryStartRecording();
        }

        private void SetupJumpVariables()
        {
            float timeToApex = _maxJumpTime / 2;
            _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        }

        void HandleRotation()
        {
            Vector3 positionToLookAt;
            // the change in position our character should point to
            positionToLookAt.x = 0;
            positionToLookAt.y = 0;
            positionToLookAt.z = -_currentMovement.x;
            // the current rotation of our character
            Quaternion currentRotation = transform.rotation;

            if (_isMovementPressed) {
                // creates a new rotation based on where the player is currently pressing
                Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
                // rotate the character to face the positionToLookAt            
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
            }
        }
        
        private void HandleJump()
        {
            if (!_isJumping && _characterController.isGrounded && _isJumpPressed)
            {
                if (_jumpCount < 3 && _currentJumpResetRoutine != null)
                {
                    StopCoroutine(_currentJumpResetRoutine);
                }

                _isJumping = true;
                _jumpCount += 1;
                _currentMovement.y = _initialJumpVelocity;
                _appliedMovement.y = _initialJumpVelocity;
            }
            else if (!_isJumpPressed && _isJumping && _characterController.isGrounded)
            {
                _isJumping = false;
            }
        }

        private void HandleGravity()
        {
            bool isFalling = _currentMovement.y <= 0.0f || !_isJumpPressed;
            float fallMultiplier = 2.0f;
            // apply proper gravity if the player is grounded or not
            if (_characterController.isGrounded)
            {
                _currentMovement.y = _groundedGravity;
                _appliedMovement.y = _groundedGravity;

                // additional gravity applied after reaching apex of jump
            }
            else if (isFalling)
            {
                float previousYVelocity = _currentMovement.y;
                _currentMovement.y = _currentMovement.y + (_gravity * fallMultiplier * Time.deltaTime);
                _appliedMovement.y = Mathf.Max((previousYVelocity + _currentMovement.y) * .5f, -20.0f);

                // applied when character is not grounded
            }
            else
            {
                float previousYVelocity = _currentMovement.y;
                _currentMovement.y = _currentMovement.y + (_gravity * Time.deltaTime);
                _appliedMovement.y = (previousYVelocity + _currentMovement.y) * .5f;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            HandleRotation();
            
            _isJumpPressed = _playerInput.IsHoldingJump;
            
            _appliedMovement.x = _currentMovement.x;
            _appliedMovement.z = _currentMovement.z;

            _characterController.Move(_appliedMovement * Time.deltaTime);

            HandleGravity();
            HandleJump();
        }

        private void TryStartRecording()
        {
            if (_hasStartedRecording)
            {
                return;
            }

            _hasStartedRecording = true;
            _movementPlayback.Record();
        }
    }
}