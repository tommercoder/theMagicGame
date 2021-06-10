using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class MainProceduralController : MonoBehaviour
{
    //with this script also using dontmovewithparent.cs
    [Header("OBJECTS")]
    public Transform targetRight = null;
    public Transform targetLeft = null;
    public Transform baksBot = null;

    
    public LayerMask mask;
    [Header("STEP SETTINGS")]
    public bool Moving;
    public Transform stepTargetLeft;
    public Transform stepTargetRight;
    public List<Transform> stepTargets = new List<Transform>(2);
    public float wantStepAtDistance = 0.45f;
    public float timeToMakeStep = 0.125f;
    public bool waited = false;
    public DontMoveWithParent dontMoveWithParentRight;
    public DontMoveWithParent dontMoveWithParentLeft;
    public Transform target;
    private List<Transform> footIKTargets = new List<Transform>(2);
    public Transform head;
    public float heightOfLegUp = 1.5f;
    //enable when collide
    public void OnCollisionEnter(Collision collision)
    {
        //when collide they are visible
        stepTargetLeft.gameObject.GetComponent<Renderer>().enabled = true;
        stepTargetRight.gameObject.GetComponent<Renderer>().enabled = true;
    }
    //hide in game steptargets
    public void Awake()
    {
        //for not to see target spheres on game menu
        stepTargetLeft.gameObject.GetComponent<Renderer>().enabled = false;
        stepTargetRight.gameObject.GetComponent<Renderer>().enabled = false;
    }
    public void Start()
    {
        if (GetComponentInParent<navmeshPatrol>() != null)
        {
            GetComponentInParent<navmeshPatrol>().enabled = false;
            GetComponentInParent<navmeshPatrol>().enabled = true;
           
        }
        footIKTargets.Add(targetLeft);
        footIKTargets.Add(targetRight);

        stepTargets.Add(stepTargetLeft);
        stepTargets.Add(stepTargetRight);
    }

    
    IEnumerator wait()
    {
        //czeka krok jednej nogi
        waited = false;
        yield return new WaitForSeconds(timeToMakeStep);
        waited = true;
    }

   

    public void Update()
    {
        //Nie przesuwa się jeśli otwarte jest menu.
        if (pauseMenu.instance.menuIsOpened)
            return;
        RaycastHit hit;
        //funkcja która sprawdza ziemie dla każdego z "celi" i przesuwa je w góre i dół.
        stepTargetIk(0);
        stepTargetIk(1);
        //dystant między "step target" i kulką która jest zaczepiona w nodze.
        float distanceRight = Vector3.Distance(footIKTargets[0].position, stepTargets[0].position);
        float distanceLeft = Vector3.Distance(footIKTargets[1].position, stepTargets[1].position);
        //robi krok prawą nogą jeśli dystans jest większy niż musi być.
        if (distanceRight > wantStepAtDistance)
        {
           //Otrzymuje pozycje końcową kroku.
            if (GetGroundedEndPosition(out Vector3 endPos, out Vector3 endNormal, 0))
            {
                //Przesuwa ciało do góry jeśli okazało się że noga staje w pozycji wyżej niż była.
                if (endPos.y >= transform.position.y)
                {
                    transform.Translate(Vector3.up * 2 * Time.deltaTime/ 1.6f);
                }//Przesuwa ciało do dołu jeśli okazało się że noga staje w pozycji niżej niż  była.
                else if(endPos.y <= transform.position.y)
                {
                    transform.Translate(Vector3.down * 2 * Time.deltaTime/ 1.6f);
                }


                //Wylicza końcową rotację.
                Quaternion endRot = Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(stepTargets[0].forward, endNormal),
                    endNormal
                );
                //Przesuwa w wyliczoną pozycje i rotacje.
                StartCoroutine(
                    MoveToPoint(endPos, endRot, timeToMakeStep, 0));
                //Czeka dopóki krok zakończy i potem zacznie nowy.
                StartCoroutine(wait());
                
                
            }
        }
        //Robi krok lewą nogą jeśli dystans jest większy niż musi być.
        if (distanceLeft > wantStepAtDistance && waited)
        {
            //Otrzymuje pozycje końcową kroku.
            if (GetGroundedEndPosition(out Vector3 endPos, out Vector3 endNormal, 1))
            {
                //To samo co z prawą nogą.
                if (endPos.y >= transform.position.y)
                {
                    transform.Translate(Vector3.up* 2* Time.deltaTime/ 1.6f);
                }
                else if (endPos.y <= transform.position.y)
                {
                    transform.Translate(Vector3.down * 2 * Time.deltaTime/1.6f);
                }
                Quaternion endRot = Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(stepTargets[1].forward, endNormal),
                    endNormal
                );

                StartCoroutine(
                    MoveToPoint(endPos, endRot, timeToMakeStep, 1));
                StartCoroutine(wait());
            }
        }      
        //moveForward();
    }

    
    //Funkcja wylicza końcową pozycje kroku.
    bool GetGroundedEndPosition(out Vector3 position, out Vector3 normal, int index)
    {
        //Tworzy "linie" wektor w góre od "kulki".
        Vector3 raycastOrigin = stepTargets[index].position + stepTargets[index].up * 2f;

        //Tworzy "Raycast" i szuka punkt uderzenia(hit point).
        if (Physics.Raycast(
            raycastOrigin,
            -stepTargets[index].up,
            out RaycastHit hit,
            Mathf.Infinity,
            mask
        ))
        {
            //Mówi że pozycja dla kroku jest pozycją "hit point".
            position = hit.point;  
            normal = hit.normal;
            return true;
        }
        //Jeśli nie znalazło się takiego punktu,wszystkie punkty wyzerują się i zwraca false.
        position = Vector3.zero;
        normal = Vector3.zero;
        return false;
    }
    //Funkcja służy dla tego żeby "kulki" przesuwały się tylko po ziemi i nigdzie nie spadały itd.
    void stepTargetIk(int index)
    {

        float distance = 100f;
        RaycastHit hit;
        //Tworzy "Raycast"(linie) od kulki do dołu."mask" jest tą warstwą na której ten kod będzie działać.
        if (Physics.Raycast(stepTargets[index].position, Vector3.down, out hit, distance, mask))
        {

            Vector3 targetLocation = hit.point;
            //Wylicza rotacje "kulki" do ziemi.
            var slopeRotation = Quaternion.FromToRotation(stepTargets[index].up, hit.normal);
            //Obraca "kulkę".
            stepTargets[index].rotation = Quaternion.Slerp(stepTargets[index].rotation, slopeRotation * stepTargets[index].rotation, 10 * Time.deltaTime);
            //Wylicza pozycje kulki jako "hit point".
            targetLocation += new Vector3(0, stepTargets[index].localScale.y / 2, 0);
            //Wstawia pozycje kulce.
            stepTargets[index].position = targetLocation;
        }
   
    } 
 
    //Zmienia pozycje "Target" na "StepTarget".
    IEnumerator MoveToPoint(Vector3 endPoint, Quaternion endRot, float moveTime, int index)
    {

        Moving = true;
        //W zależności od indeksu mówi jaka noga musi stać w miejscu.
        if (index == 0)
        {
            dontMoveWithParentLeft.dontMoveWithParent = false;
        }
        if (index == 1)
        {
            dontMoveWithParentRight.dontMoveWithParent = false;
        }
        //Pozycja kulki przyciepionej do nogi.
        Vector3 startPoint = footIKTargets[index].position;
        //Jej rotacja.
        Quaternion startRot = footIKTargets[index].rotation;
        //Deklaruje że "endPoint" jest trochy wyżej niż pozycja kulki.
        endPoint += footIKTargets[index].up * 0.2f;
        //Wylicza punkt centralny między punktem początku i końca.
        Vector3 centerPoint = (startPoint + endPoint) / 2;
        //W punkcie centralnym podnosi nogę do góry.
        centerPoint += (footIKTargets[index].up * heightOfLegUp) * Vector3.Distance(startPoint, endPoint) / 2f;


        float timeElapsed = 0;

        // do while przesiwa nogę z punktu początkowego do centralnego,i stąd do końcowego,także zmienia rotacje.
        do
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / moveTime;
            //Easing robi to wszystko bardziej "gładkim".
            normalizedTime = Easing.EaseInOutCubic(normalizedTime);

            footIKTargets[index].position =
                Vector3.Lerp(
                    Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                    Vector3.Lerp(centerPoint, endPoint, normalizedTime),
                    normalizedTime
                );
            
            footIKTargets[0].rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

            
            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveTime);

        //Robi nogę która zrobiła krok zaczepioną w miejscu.
        Moving = false;
        if (index == 0)
        {
            dontMoveWithParentLeft.savedPosition = endPoint;
            dontMoveWithParentLeft.dontMoveWithParent = true;

        }
        if (index == 1)
        {
            dontMoveWithParentRight.savedPosition = endPoint;
            dontMoveWithParentRight.dontMoveWithParent = true;

        }
    }

}