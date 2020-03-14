using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private CameraControls cameraControls;
	private Rigidbody2D rb;
	private AudioSource audioSource;
	private Animator anim;

	//facing
	private Vector2 worldMousePos;
	private Vector2 myFlatTransform;

	//walking
	private float moveInput;
	public float walkSpeed;

	//jumping
	public bool isJumping = false;
	public bool isGrounded = false;
	public LayerMask whatIsGround;
	public Transform feetPos;
	public float whiskerLength;
	[Range(1,10)]
	public float jumpVelocity;
	public float fallMultiplier;
	public float lowJumpMultiplier;
	public AudioClip jumpAudioClip;
	public AudioClip landAudioClip;
	public bool landed = false;
	public float jumpTime = 0;
	public float jumpTimeMax;
	public bool canJump = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		cameraControls = Camera.main.GetComponent<CameraControls>();
		//audioSource = GetComponent<AudioSource>();
		//anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		myFlatTransform = new Vector2(transform.position.x, transform.position.y);

		moveInput = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Jump"))
		{
			isJumping = true;
			
		} else if (Input.GetButtonUp("Jump"))
		{
			isJumping = false;
		}

		if (isJumping && jumpTime < jumpTimeMax)
		{
			jumpTime += Time.deltaTime;
			canJump = true;
		}
		else
		{
			canJump = false;
		}
	}

	private void FixedUpdate()
	{
		if (worldMousePos.x > myFlatTransform.x)
		{
			//look right
			transform.localScale = new Vector3(1f, 1f, 1);
			cameraControls.isFacingLeft = false;
		}
		else if (worldMousePos.x < myFlatTransform.x)
		{
			//look left
			transform.localScale = new Vector3(-1f, 1f, 1);
			cameraControls.isFacingLeft = true;
		}

		Debug.DrawRay(feetPos.position, feetPos.right * whiskerLength, Color.magenta);
		isGrounded = Physics2D.OverlapCircle(feetPos.position, whiskerLength, whatIsGround);
		//anim.SetBool("isGrounded", isGrounded);

		if (isGrounded && !landed)
		{
			landed = true;
			jumpTime = 0;
			//audioSource.PlayOneShot(landAudioClip);
		}

		if (!isGrounded)
		{
			landed = false;
		}

		rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
		//anim.SetFloat("Speed", Mathf.Abs(moveInput));

		if (canJump)
		{
			Vector2 halfwayVector = (new Vector2(rb.velocity.x, 0) + Vector2.up * jumpVelocity);
			rb.velocity = halfwayVector;
			//audioSource.PlayOneShot(jumpAudioClip);
		}

		//falling
		if (rb.velocity.y < 0)
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
			isJumping = false;
			canJump = false;

		} else if (rb.velocity.y > 0 && !isJumping){
			rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
		}

		//anim.SetFloat("vSpeed", rb.velocity.y);
	}
}
