using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask worldMask;
    Ray ray;
    RaycastHit hit;
    Rigidbody rb;
    float baseThrowStrength = 1;
    float dipForce = 15;
    float biteMinTime = 6;
    float biteMaxTime = 12;
    float timeToBite;
    float timeAfterThrow;
    Vector3 startPos = new Vector3();
    bool firstSplash = true;
    bool inWater = false;
    bool hookThrown = false;
    bool fishOnHook = false;
    public bool fishingGameFail = false;
    bool callOnce = true;

    private FishingMinigame2 _fishingMinigame;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        _fishingMinigame = FindFirstObjectByType<FishingMinigame2>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        startPos = new Vector3(-8.305f, 2.83f, -1.479f);
    }

    void Update()
    {
        HookFishInTime();
        CastHook();
        StartReelingAudio();
        StopReelingAudio();
        IfFishingGameFail();

        timeAfterThrow += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.L))
        {
            fishOnHook = false;
        }

    }

    void IfFishingGameFail()
    {
        if (fishingGameFail)
        {
            ResetHook();
            fishingGameFail = false;
            fishOnHook = false;
        }
    }
    public void ThrowHook()
    {
        // throw animation
        if (!hookThrown && !UIActions.instance.SettingsUI.activeInHierarchy && !UIActions.instance.InventoryUI.activeInHierarchy && !UIActions.instance.ShopUI.activeInHierarchy && !UIActions.instance.PausedGameUI.activeInHierarchy)
        {
            rb.linearDamping = 0.2f;
            Vector3 throwDirection = Vector3.zero;
            if (MousePosOnLake(ref throwDirection))
            {
                hookThrown = true;
                audioManager.PlaySFX(audioManager.Casting);
                rb.useGravity = true;
                float throwStrenght = 0;

                throwDirection -= transform.position;
                throwStrenght = throwDirection.magnitude * 0.7f;
                throwDirection.Normalize();
                throwDirection += Vector3.up;
                throwDirection.Normalize();

                timeToBite = Random.Range(biteMinTime, biteMaxTime);
                timeAfterThrow = 0;

                rb.AddForce(throwDirection * (throwStrenght + baseThrowStrength), ForceMode.Impulse);

            }
        }
    }

    IEnumerator ThrowHookWait()
    {
        yield return new WaitForSeconds(0); // was suppose to be animation thats why corutine but we didnt add it i think
    }


    public void ResetHook()
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.linearDamping = 10000;
        rb.position = startPos;

        dipForce = 15;
        //rb.linearDamping = 0.2f;
        firstSplash = true;
        timeToBite = Mathf.Infinity;
        if (hookThrown)
        {
            StopAllCoroutines();
            StartCoroutine(WaitToCastAgain());
        }

    }
    IEnumerator WaitToCastAgain()
    {
        yield return new WaitForSeconds(1);
        hookThrown = false;
    }

    bool MousePosOnLake(ref Vector3 posOnLake)
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        ray = mainCamera.ScreenPointToRay(mouseScreenPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, worldMask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                posOnLake = hit.point;
                return true;
            }
        }
        return false;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 100);
        Gizmos.DrawSphere(hit.point, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water") && firstSplash)
        {
            // spalsh animation

            firstSplash = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            inWater = true;
        }

    }

    void HookFishInTime()
    {
        if (inWater && !fishOnHook)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && timeToBite > timeAfterThrow && dipForce != 15)
            {
                ResetHook();
            }
            if (timeToBite < timeAfterThrow)
            {
                if (timeToBite < timeAfterThrow && dipForce != 15)
                {
                    audioManager.PlaySFX(audioManager.FishOnHook);
                    rb.AddForce(Vector3.down * 8);
                }
                dipForce = 15;
                rb.useGravity = true;
                if (timeToBite > timeAfterThrow - 3 && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _fishingMinigame.StartGame();
                    fishOnHook = true;
                }
                else if (timeToBite < timeAfterThrow - 2)
                {
                    timeAfterThrow = 0;
                    timeToBite = Random.Range(biteMinTime, biteMaxTime);
                }



            }

        }
    }

    void CastHook()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        ray = mainCamera.ScreenPointToRay(mouseScreenPos);
        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out hit, Mathf.Infinity, worldMask))
        {
            ThrowHook();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ResetHook();
        }
    }

    void StartReelingAudio()
    {
        if (fishOnHook && callOnce == true)
        {
            audioManager.PlayReelingSound();
            callOnce = false;
        }
    }
    void StopReelingAudio()
    {
        if (!fishOnHook && callOnce == false)
        {
            audioManager.StopReelingSound();
            callOnce = true;
        }
    }


    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {

            rb.linearDamping = 2;
            rb.AddForce(Vector3.up * dipForce);

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            dipForce -= 1;
            if (dipForce < 13)
            {
                dipForce = 0;
                rb.useGravity = false;
                rb.linearVelocity = Vector3.zero;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            rb.linearVelocity = Vector3.zero;
            ResetHook();
        }
    }
}
