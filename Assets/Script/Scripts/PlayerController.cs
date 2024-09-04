using UnityEngine;



public class PlayerController : PlayerControllerBase
{
    
    private Vector3 _movement;
    private Quaternion _rotation;



    private void Start()
    {
        Cursor.visible = false;
        _currentHealth = _heroConfig.MaxHealh;
        _textMeshProUGUI = _pauseController.HeroHealthBarText;
        _textMeshProUGUI.text = _currentHealth.ToString();
    }


    private void Update()
    {
        _textMeshProUGUI.text = _currentHealth.ToString();
        _rotation = RotationPl;
        _movement = MoveDirection;
    }

    private void FixedUpdate()
    {
        PlayerMove(_movement);
        LookRotation(_rotation);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionWithEnemy(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerMethod(other);
    }

}
