using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using AK.Wwise.Event;


public class Bird : MonoBehaviour
{
  private Vector3 _initialPosition;
  private bool _birdWasLaunched = false;
  private float _timeSitingAround;  

  public AK.Wwise.Event SynthEvent;


  // Deste modo podemos editar a variavel no editor
  [SerializeField] private float _launchPower = 20;

  private void Awake() {
    // Grava a posição inicial
    _initialPosition = transform.position;
    
  }
  private void Update() {

	  if (_birdWasLaunched && 
	  GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1) {
		  // time.deltaTime da o tempo do frame 
		  _timeSitingAround += Time.deltaTime;
	  }
    if (
		transform.position.y > 6 ||
		transform.position.y < - 4 ||
		transform.position.x < -11.1 ||
		transform.position.x > 7.2 ||
		_timeSitingAround > 3
		 ) {
		string currentSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentSceneName);

    }
  }
  private void OnMouseDown() 
  {
      GetComponent<SpriteRenderer>().color = Color.red;
      // SynthEvent.Post("")
  
    
  }

private void OnMouseUp() 
  {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        // chamar GetComponent é lento
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower * 10 );
        GetComponent<Rigidbody2D>().gravityScale = 1;
		_birdWasLaunched = true;

  }

  private void OnMouseDrag() 
  {
    // mapear a posição do rato a posição do jogo
    Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    transform.position = new Vector3(newPosition.x,newPosition.y,0);
  }
}
