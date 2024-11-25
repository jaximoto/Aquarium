
using NUnit.Framework.Constraints;
using System.Data;
using Tank;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : Item
{
    public bool debug = false;
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
    public float maxHealth = 10f;
    public float health, hunger;
    public float pTemp, pPH, pC02, pAlgaeContent, pWaste;
    public float outTemp, outPH, outC02, outAlgaeContent, outWaste, outMoney, outFishOil, outCoral;
    public GameObject myBubble;
    public GameObject goldBubble;
    public float bubbleOffset = 1.5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    //go over fish stats and preferences.
    //make an algorithm
    public float fishStatus = 0.65f;
    public Status currentStatus = Status.healthy;
    public enum Status
    {
        dead, //fish is dead, fish health points == 0
        dying, //fish is losing health, fishStatus == [0,0.25]
        unhealthy, //fish is holdin steady & give 1/2 gold, fishStatus == [0.25,0.5]
        healthy, //fish is healin & give 100% gold, fishStatus == [0.5,0.75]
        plusUltra //fish is healin & givin bonus resources == [0.75,1]

    }

    //every fish finds the player controller on awake and calls the player controller increment stat for each of their resources
    public override void UpdateTank(ref TankModel tankModel)
    {
        tankModel.IncrementStat("Temp", this.outTemp);
        tankModel.IncrementStat("CO2", this.outC02);
        tankModel.IncrementStat("Waste", this.outWaste);
        tankModel.IncrementStat("Algae", this.outAlgaeContent);
        tankModel.IncrementStat("PH", this.outPH);

        
        

    }
    public void MakeGoldBubble()
    {
        Vector2 tmp = transform.position;
        tmp.y += bubbleOffset;
        myBubble = Instantiate(goldBubble, tmp, Quaternion.identity);
        myBubble.transform.SetParent(transform, true);
    }
    public void DestroyBubble()
    {
        Destroy(myBubble);
        myBubble = null;
    }
    public override void UpdateSelf(ref TankModel tankModel)
    {
        CalcFishStatus(ref tankModel);
        UpdateStatus(fishStatus);
    }
   
    public void UpdateHealth()
    {
        if (currentStatus == Status.dying)
        {
            health -= 1;
            if (health <= 0)
            {
                currentStatus = Status.dead;
                rb.constraints = RigidbodyConstraints2D.None;
            }
                
        }

        if (currentStatus == Status.healthy || currentStatus == Status.plusUltra)
        {
            health += 1;
            if (health < maxHealth)
                health = maxHealth;
        }

        if (currentStatus == Status.dead)
        {
            rb.AddForce(Vector2.up);
        }
    }
    public void UpdateStatus(float fishStatus)
    {
        if (fishStatus < .25)
        {
            currentStatus = Status.dying;
        }
        else if (fishStatus > .25 && fishStatus < .5)
        {
            currentStatus = Status.unhealthy;
        }
        else if (fishStatus > .5 && fishStatus < .75)
        {
            currentStatus = Status.healthy;
        }
        else if (fishStatus > .75)
        {
            currentStatus = Status.plusUltra;
        }
        
        if (currentStatus != Status.dead && currentStatus != Status.dying)
        {
            if(myBubble == null)
                MakeGoldBubble();
        }

        UpdateHealth();

    }
    public void CalcFishStatus(ref TankModel tankModel)
    {
        /*if (debug == true)
        {
            C02Calc(tankModel.GetStat("C02"));
            TempCalc(tankModel.GetStat("Temp"));
            WasteCalc(tankModel.GetStat("Waste"));
            PHCalc(tankModel.GetStat("PH"));
            AlgaeCalc(tankModel.GetStat("Algae"));
        }
        else
        {
            // C02 0 - 1000

            StatCalc(300, tankModel.GetStat("CO2"), pC02);
            // Temp 0 - 50

            StatCalc(10, tankModel.GetStat("Temp"), pTemp);
            // Waste 0 - 100

            StatCalc(20, tankModel.GetStat("Waste"), pWaste);
            // PH 0 - 14

            StatCalc(5, tankModel.GetStat("PH"), pPH);
            // Algae 0 - 100

            StatCalc(30, tankModel.GetStat("Algae"), pAlgaeContent);
        }
        */
        C02Calc(tankModel.GetStat("CO2"));
        TempCalc(tankModel.GetStat("Temp"));
        WasteCalc(tankModel.GetStat("Waste"));
        PHCalc(tankModel.GetStat("PH"));
        AlgaeCalc(tankModel.GetStat("Algae"));

        if (hunger < 5)
        {
            fishStatus -= .1f;
            Mathf.Clamp01(fishStatus);
        }
    }

    void StatCalc(float tolerance, float tankValue, float preference)
    {
        ;
        // Do TankTemp < tolerance (15+- pTemp)
        // Calculate the absolute difference between the tank's temperature and the preferred temperature
        float C02Difference = Mathf.Abs(tankValue - preference);

        // Normalize the difference to a range of -1 to 1 based on tolerance
        // 0 means the temperature is exactly at the preferred value, which will return 0 (neutral)
        // As the temperature moves away from the preferred value, the result moves toward -1 or 1.
        float normalizedTemp = 1f - (C02Difference / tolerance);
        normalizedTemp = Mathf.Clamp(normalizedTemp, -1f, 1f); // Clamp the value between -1 and 1

        // Add the normalized temperature difference to the fishStatus to adjust the health
        fishStatus += normalizedTemp * 0.05f; // Adjust the multiplier (0.05f) to control how much it affects health

        // Ensure fishStatus stays within the [0, 1] range
        fishStatus = Mathf.Clamp01(fishStatus);
    }
    #region temp
    public void TempCalc(float tankTemp)
    {
        float tolerance = 10;
        // Do TankTemp < tolerance (15+- pTemp)
        // Calculate the absolute difference between the tank's temperature and the preferred temperature
        float temperatureDifference = Mathf.Abs(tankTemp - pTemp);

        // Normalize the difference to a range of -1 to 1 based on tolerance
        // 0 means the temperature is exactly at the preferred value, which will return 0 (neutral)
        // As the temperature moves away from the preferred value, the result moves toward -1 or 1.
        float normalizedTemp = 1f - (temperatureDifference / tolerance);
        normalizedTemp = Mathf.Clamp(normalizedTemp, -1f, 1f); // Clamp the value between -1 and 1

        // Add the normalized temperature difference to the fishStatus to adjust the health
        fishStatus += normalizedTemp * 0.05f; // Adjust the multiplier (0.05f) to control how much it affects health

        // Ensure fishStatus stays within the [0, 1] range
        fishStatus = Mathf.Clamp01(fishStatus);
        // Optionally, log the normalized value for debugging purposes
        Debug.Log($"Temperature Difference: {temperatureDifference}, Normalized Temp Value: {normalizedTemp}, Updated Fish Status: {fishStatus}");
    }
    #endregion

    #region C02
    public void C02Calc(float tankC02)
    {
        float tolerance = 150;
        // Do TankTemp < tolerance (15+- pTemp)
        // Calculate the absolute difference between the tank's temperature and the preferred temperature
        float C02Difference = Mathf.Abs(tankC02 - pC02);

        // Normalize the difference to a range of -1 to 1 based on tolerance
        // 0 means the temperature is exactly at the preferred value, which will return 0 (neutral)
        // As the temperature moves away from the preferred value, the result moves toward -1 or 1.
        float normalizedTemp = 1f - (C02Difference / tolerance);
        normalizedTemp = Mathf.Clamp(normalizedTemp, -1f, 1f); // Clamp the value between -1 and 1

        // Add the normalized temperature difference to the fishStatus to adjust the health
        fishStatus += normalizedTemp * 0.05f; // Adjust the multiplier (0.05f) to control how much it affects health

        // Ensure fishStatus stays within the [0, 1] range
        fishStatus = Mathf.Clamp01(fishStatus);
        // Optionally, log the normalized value for debugging purposes
        Debug.Log($"C02 Difference: {C02Difference}, Normalized C02 Value: {normalizedTemp}, Updated Fish Status: {fishStatus}");
    }
    #endregion

    #region Waste
    public void WasteCalc(float tankWaste)
    {
        float tolerance = 20;
        // Do TankTemp < tolerance (15+- pTemp)
        // Calculate the absolute difference between the tank's temperature and the preferred temperature
        float WasteDifference = Mathf.Abs(tankWaste - pWaste);

        // Normalize the difference to a range of -1 to 1 based on tolerance
        // 0 means the temperature is exactly at the preferred value, which will return 0 (neutral)
        // As the temperature moves away from the preferred value, the result moves toward -1 or 1.
        float normalizedTemp = 1f - (WasteDifference / tolerance);
        normalizedTemp = Mathf.Clamp(normalizedTemp, -1f, 1f); // Clamp the value between -1 and 1

        // Add the normalized temperature difference to the fishStatus to adjust the health
        fishStatus += normalizedTemp * 0.05f; // Adjust the multiplier (0.05f) to control how much it affects health

        // Ensure fishStatus stays within the [0, 1] range
        fishStatus = Mathf.Clamp01(fishStatus);
        // Optionally, log the normalized value for debugging purposes
        Debug.Log($"Waste Difference: {WasteDifference}, Normalized Waste Value: {normalizedTemp}, Updated Fish Status: {fishStatus}");
    }
    #endregion

    #region PH
    public void PHCalc(float tankPH)
    {
        float tolerance = 4;
        // Do TankTemp < tolerance (15+- pTemp)
        // Calculate the absolute difference between the tank's temperature and the preferred temperature
        float C02Difference = Mathf.Abs(tankPH - pPH);

        // Normalize the difference to a range of -1 to 1 based on tolerance
        // 0 means the temperature is exactly at the preferred value, which will return 0 (neutral)
        // As the temperature moves away from the preferred value, the result moves toward -1 or 1.
        float normalizedTemp = 1f - (C02Difference / tolerance);
        normalizedTemp = Mathf.Clamp(normalizedTemp, -1f, 1f); // Clamp the value between -1 and 1

        // Add the normalized temperature difference to the fishStatus to adjust the health
        fishStatus += normalizedTemp * 0.05f; // Adjust the multiplier (0.05f) to control how much it affects health

        // Ensure fishStatus stays within the [0, 1] range
        fishStatus = Mathf.Clamp01(fishStatus);
        // Optionally, log the normalized value for debugging purposes
        Debug.Log($"PH Difference: {C02Difference}, Normalized PH Value: {normalizedTemp}, Updated Fish Status: {fishStatus}");
    }
    #endregion

    #region Algae
    public void AlgaeCalc(float tankAlgae)
    {
        float tolerance = 150;
        // Do TankTemp < tolerance (15+- pTemp)
        // Calculate the absolute difference between the tank's temperature and the preferred temperature
        float C02Difference = Mathf.Abs(tankAlgae - pAlgaeContent);

        // Normalize the difference to a range of -1 to 1 based on tolerance
        // 0 means the temperature is exactly at the preferred value, which will return 0 (neutral)
        // As the temperature moves away from the preferred value, the result moves toward -1 or 1.
        float normalizedTemp = 1f - (C02Difference / tolerance);
        normalizedTemp = Mathf.Clamp(normalizedTemp, -1f, 1f); // Clamp the value between -1 and 1

        // Add the normalized temperature difference to the fishStatus to adjust the health
        fishStatus += normalizedTemp * 0.05f; // Adjust the multiplier (0.05f) to control how much it affects health

        // Ensure fishStatus stays within the [0, 1] range
        fishStatus = Mathf.Clamp01(fishStatus);
        // Optionally, log the normalized value for debugging purposes
        Debug.Log($"Algae Difference: {C02Difference}, Normalized Algae Value: {normalizedTemp}, Updated Fish Status: {fishStatus}");
    }
    #endregion

    
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
        if (currentStatus != Status.dead)
            WhenToMove();
        FlipSprite();
    }

    public virtual void FixedUpdate()
    {
        Move();
    }
}
