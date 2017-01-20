using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (TP_Controller))]
[RequireComponent (typeof (TP_Animator))]
public class TP_Animator : MonoBehaviour {

	public enum Direction {
		Stationary,
		Forward,
		Backward,
		Left,
		Right,
		LeftForward,
		RightForward,
		LeftBackward,
		RightBackward
	}

	public enum CharacterState {
		Idle,
		Walking,
		Running,
		WalkingBackwards,
		StrafingLeft,
		StrafingRight,
		Jumping,
		Falling,
		Landing,
		Climbing,
		Sliding,
		Using,
		Dead,
		Attacking,
		Victorious,
		ActionLocked
	}

	#region PUBLIC_VARIABLES

	public static TP_Animator instance;

	#endregion

	#region PRIVATE_VARIABLES

	private Animation anim;

	#endregion

	#region PUBLIC_PROPERTIES

	public Direction MoveDirection {get; set;} 			// Property holds the movement direction.
	public CharacterState State {get; set;}				// Property holds the animation state.

	#endregion

	#region UNITY_FUNCTIONS

	// Use this for initialization
	void Awake () {
		if(instance != this) {
			instance = this;
		}

		anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		DetermineCurrentCharacterState ();
		ProcessCurrentCharacterState ();
	}

	#endregion

	#region PUBLIC_FUNCTIONS

	public void DetermineCurrentMoveDirection () {
		bool forward = false;
		bool backward = false;
		bool left = false;
		bool right = false;

		if(TP_Motor.instance.MoveVector.z > 0){
			forward = true;
		}
		else if(TP_Motor.instance.MoveVector.z < 0){
			backward = true;
		}

		if(TP_Motor.instance.MoveVector.x > 0){
			right = true;
		}
		else if(TP_Motor.instance.MoveVector.x < 0){
			left = true;
		}

		if (forward) {
			if(left) {
				MoveDirection = Direction.LeftForward;
			} else if (right) {
				MoveDirection = Direction.RightForward;
			} else {
				MoveDirection = Direction.Forward;
			}
		} else if (backward) {
			if (left) {
				MoveDirection = Direction.LeftBackward;
			} else if (right) {
				MoveDirection = Direction.RightBackward;
			} else {
				MoveDirection = Direction.Backward;
			}
		} else if (left) {
			MoveDirection = Direction.Left;
		} else if (right) {
			MoveDirection = Direction.Right;
		} else {
			MoveDirection = Direction.Stationary;
		}
	}

	#endregion

	#region PRIVATE_FUNCTIONS

	void DetermineCurrentCharacterState () {
		if (State == CharacterState.Dead) {
			return;
		}

		if (!TP_Controller.characterController.isGrounded){
			if(State != CharacterState.Falling && State != CharacterState.Jumping && State != CharacterState.Landing) {
				// TODO: Should be falling!
			}
		}

		if (State != CharacterState.Falling && State != CharacterState.Jumping && State != CharacterState.Landing 
			&& State != CharacterState.Using && State != CharacterState.Climbing && State != CharacterState.Sliding 
			&& State != CharacterState.Attacking && State != CharacterState.Victorious){

			switch (MoveDirection) {
			case Direction.Forward:
				State = CharacterState.Walking;
				break;
			case Direction.LeftForward:
				State = CharacterState.Walking;
				break;
			case Direction.RightForward:
				State = CharacterState.Walking;
				break;
			case Direction.Backward:
				State = CharacterState.WalkingBackwards;
				break;
			case Direction.LeftBackward:
				State = CharacterState.WalkingBackwards;
				break;
			case Direction.RightBackward:
				State = CharacterState.WalkingBackwards;
				break;
			case Direction.Left:
				State = CharacterState.StrafingLeft;
				break;
			case Direction.Right:
				State = CharacterState.StrafingRight;
				break;
			case Direction.Stationary:
				State = CharacterState.Idle;
				break;
			}

		}

		if(State == CharacterState.Walking && TP_Motor.instance.forwardSpeed == 5){
			State = CharacterState.Running;
		}
	}

	void ProcessCurrentCharacterState () {
		switch (State) {
		case CharacterState.Idle:
			Idle ();
			break;
		case CharacterState.Walking:
			Walking();
			break;
		case CharacterState.Running:
			Running ();
			break;
		case CharacterState.WalkingBackwards:
			WalkingBackwards ();
			break;
		case CharacterState.StrafingLeft:
			StrafingLeft ();
			break;
		case CharacterState.StrafingRight:
			StrafingRight ();
			break;
		case CharacterState.Jumping:
			break;
		case CharacterState.Falling:
			break;
		case CharacterState.Landing:
			break;
		case CharacterState.Climbing:
			break;
		case CharacterState.Sliding:
			break;
		case CharacterState.Using:
			Using ();
			break;
		case CharacterState.Attacking:
			Attacking ();
			break;
		case CharacterState.Victorious:
			Victorious ();
			break;
		case CharacterState.Dead:
			break;
		case CharacterState.ActionLocked:
			break;
		}
	}

	#endregion

	#region CHARACTER_STATE_FUNCTIONS

	void Idle () {
		//TODO: Implement me!
		anim.CrossFade("idle");
	}

	void Walking () {
		anim.CrossFade("walk");
	}

	void Running () {
		//TODO: Implement me!
		anim.CrossFade("run");
	}

	void WalkingBackwards () {
		//TODO: Implement me!
	}

	void StrafingLeft () {
		//TODO: Implement me!
	}
	void StrafingRight () {
		//TODO: Implement me!
	}

	void Using () {
		//TODO: Implement me!

		if(!anim.isPlaying){
			State = CharacterState.Idle;
			anim.CrossFade("idle");
		}

	}

	void Victorious () {
		if(!anim.isPlaying) {
			State = CharacterState.Idle;
			anim.CrossFade("idle");
		}
	}

	void Attacking () {
		if(!anim.isPlaying) {
			State = CharacterState.Idle;
			anim.CrossFade("idle");
		}
	}

	#endregion

	#region START_ACTION_METHOD

	public void Use () {
		State = CharacterState.Using;
		//TODO: Implement me!
		anim.CrossFade("charge");
	}

	public void Attack () {
		State = CharacterState.Attacking;
		//TODO: Implement me!
		anim.CrossFade("attack");
	}

	public void Victory () {
		State = CharacterState.Victorious;
		//TODO: Implement me!
		anim.CrossFade("victory");
	}

	#endregion
}
