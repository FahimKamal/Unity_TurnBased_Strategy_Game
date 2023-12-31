using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour{

    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private Vector3 _targetFollowOffset;
    private CinemachineTransposer _cinemachineTransposer;

    private void Start(){
        _cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffset = _cinemachineTransposer.m_FollowOffset;
    }

    private void Update(){
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement(){
        var inputMoveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)){
            inputMoveDir.z += 1;
        }
        if (Input.GetKey(KeyCode.S)){
            inputMoveDir.z += -1;
        }
        if (Input.GetKey(KeyCode.A)){
            inputMoveDir.x += -1;
        }
        if (Input.GetKey(KeyCode.D)){
            inputMoveDir.x += 1;
        }
        
        var moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        var moveSpeed = 10.0f;
        transform.position += moveVector * (moveSpeed * Time.deltaTime);
    }

    private void HandleRotation(){
        var rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q)){
            rotationVector.y += 1;
        }

        if (Input.GetKey(KeyCode.E)){
            rotationVector.y += -1;
        }
        
        var rotationSpeed = 100.0f;
        transform.eulerAngles += rotationVector * (rotationSpeed * Time.deltaTime);
    }
    
    private void HandleZoom(){
        var zoomAmount = 1.0f;
        
        if (Input.mouseScrollDelta.y > 0){
            _targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0){
            _targetFollowOffset.y += zoomAmount;
        }
        _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        var zoomSpeed = 5f;
        _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _targetFollowOffset,
            Time.deltaTime * zoomSpeed);
    }
}
