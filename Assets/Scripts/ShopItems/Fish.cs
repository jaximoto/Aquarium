
using Tank;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : Item
{
    public float minX = -8.5f; // Minimum X (can be adjusted)
    public float maxX = 7.5f;  // Maximum X (can be adjusted)
    public float minY = -4.5f; // Minimum Y (can be adjusted)
    public float maxY = 4.5f;  // Maximum Y (can be adjusted)
    public float minForce = 5f;
    public float maxForce = 20f;
    public float moveSpeed = 5f;
    public float timeLastMoved;
    public float timeBetweenMoves = 3f;
    public bool isMoving = false;
    public Vector2 moveTarget;
    public float health, hunger;
    public float pTemp, pPH, pC02, pAlgaeContent, pWaste;
    public float outTemp, outPH, outC02, outAlgaeContent, outWaste;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    //go over fish stats and preferences.
    //make an algorithm
    public float fishStatus = 0.65f;
    enum Status
    {
        dead, //fish is dead, fish health points == 0
        dying, //fish is losing health, fishStatus == [0,0.25]
        unhealthy, //fish is holdin steady & give 1/2 gold, fishStatus == [0.25,0.5]
        healthy, //fish is healin & give 100% gold, fishStatus == [0.5,0.75]
        plusUltra //fish is healin & givin bonus resources == [0.75,1]

    }


    public override void UpdateTank(ref TankModel tankModel)
    {
        tankModel.IncrementStat("Temp", this.outTemp);
        tankModel.IncrementStat("CO2", this.outC02);
        tankModel.IncrementStat("Waste", this.outWaste);
        tankModel.IncrementStat("Algae", this.outAlgaeContent);
        tankModel.IncrementStat("PH", this.outPH);
    }
    public void CalcFishStatus()
    {
        // C02 0 - 1000
        // Temp 0 - 50
        // Waste 0 - 100
        // PH 0 - 14
        // Algae 0 - 100

    }

    public void C02Calc()
    {

    }
    public void UpdateFishStatus()
    {

    }
    public virtual void WhenToMove()
    {
        if (!isMoving)
        {
            float timeSinceLastMove = Time.time - timeLastMoved;
            if (timeSinceLastMove >= timeBetweenMoves) 
            {
                moveTarget = WhereToMove();
                isMoving = true;
            }
        }
        
    }

    public virtual Vector2 WhereToMove()
    {
        // Generate random X and Y values within the bounding box
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        // Return the random position as a Vector2
        return new Vector2(randomX, randomY);
    }
    public virtual void Move()
    {
        // Function to move fish
        if (isMoving)
        {
            // Apply force towards the target direction (continuously moving)
            Vector2 direction = moveTarget - (Vector2)transform.position; // Target direction
            direction.Normalize(); // Normalize to ensure consistent speed
            rb.AddForce(direction * moveSpeed, ForceMode2D.Force); // Apply force

            //Debug.Log(Vector2.Distance(transform.position, moveTarget));
            // Check if the fish has reached the target (within a small threshold)
            if (Vector2.Distance(transform.position, moveTarget) < 0.1f)
            {

                isMoving = false; // Stop moving once we reach the target
                
            }
        }

        if (rb.linearVelocity == Vector2.zero)
        {
            isMoving = false;
        }
    }

    // Flip the sprite based on the direction of movement
    void FlipSprite()
    {
        // Check if the fish is moving left or right by inspecting the x component of velocity
        if (rb.linearVelocity.x < 0) // Moving to the right
        {
            if (spriteRenderer.flipX) // If the sprite is flipped, reset it
            {
                spriteRenderer.flipX = false;
            }
        }
        else if (rb.linearVelocity.x > 0) // Moving to the left
        {
            if (!spriteRenderer.flipX) // If the sprite is not flipped, flip it
            {
                spriteRenderer.flipX = true;
            }
        }
    }
    public virtual void Start()
    {
        timeLastMoved = Time.time;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        WhenToMove();
        FlipSprite();
    }

    public virtual void FixedUpdate()
    {
        Move();
    }
}
