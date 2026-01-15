using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class FishingLineTest : MonoBehaviour
{
    private Spline FishingLineSpline;
    public Transform HookTransform;
    public Transform LineBeginTransform;
    BezierKnot lineBeginKnot;
    BezierKnot lineEndKnot;
    float lineBendAmount = 2;
    SplineExtrude splineExtrude;
    void Start()
    {
        FishingLineSpline = GetComponent<SplineContainer>().Spline;
        splineExtrude = GetComponent<SplineExtrude>();
    }

    void Update()
    {
        UpdateFishingLine(HookTransform.localPosition, LineBeginTransform.localPosition);

    }

    void UpdateFishingLine(Vector3 hookPosition, Vector3 lineBeginPosition)
    {
        int hookKnotIndex = FishingLineSpline.Count - 1;

        lineBeginKnot.TangentOut = new float3(0f, lineBendAmount, 1f);
        lineEndKnot.TangentIn = new float3(0f, lineBendAmount, -1f);

        lineEndKnot = FishingLineSpline[hookKnotIndex];
        lineEndKnot.Position =  hookPosition;
        lineBeginKnot = FishingLineSpline[0];
        lineBeginKnot.Position = lineBeginPosition;
        FishingLineSpline[hookKnotIndex] = lineEndKnot;
        FishingLineSpline[0] = lineBeginKnot;
        splineExtrude.Rebuild();

    }


}
