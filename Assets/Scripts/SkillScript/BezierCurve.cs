using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    //베지어 곡선 포인트
    //몬스터 생성 후 움직임
    public List<Vector3> vecFirstList;
    public List<Vector3> vecSecondList;
    public List<Vector3> vecThirdList;
    public Vector3 P1_1, P1_2, P1_3,
                   P2_1, P2_2, P2_3,
                   P3_1, P3_2, P3_3,
                   P4_1, P4_2, P4_3,
                   P5_1, P5_2, P5_3;

    public void Start()
    {
        vecFirstList.Add(P1_1);
        vecFirstList.Add(P2_1);
        vecFirstList.Add(P3_1);
        vecFirstList.Add(P4_1);
        vecFirstList.Add(P5_1);

        vecSecondList.Add(P1_2);
        vecSecondList.Add(P2_2);
        vecSecondList.Add(P3_2);
        vecSecondList.Add(P4_2);
        vecSecondList.Add(P5_2);

        vecThirdList.Add(P1_3);
        vecThirdList.Add(P2_3);
        vecThirdList.Add(P3_3);
        vecThirdList.Add(P4_3);
        vecThirdList.Add(P5_3);
    }

    //베지어 곡선 구현(오버로드로 점 4개일 때, 점 6개일 때로 나눠놓음)
    public Vector3 Bezier(Vector3 p_1, Vector3 p_2, Vector3 p_3, float p_value)
    {
        //1번과 2번 점의 선형보간
        Vector3 A = Vector3.Lerp(p_1, p_2, p_value);
        //2번과 3번 점의 선형보간
        Vector3 B = Vector3.Lerp(p_2, p_3, p_value);

        //1번과 2번 점의 선형보간 이동 중인 점 A와 2번과 3번 점의 선형보간 이동 중인 점 B의 선형보간
        Vector3 C = Vector3.Lerp(A, B, p_value);
        //2번과 3번 점의 선형보간 이동 중인 점 B와 3번과 4번 점의 선형보간 이동 중인 점 D의 선형보간

        return C;
    }


}

//유니티 에디터
//씬에서 베지어 곡선을 그릴 점들을 직접 조절할 수 있고 DrawLine으로 이동경로를 그려주어 미리 볼 수 있다.
//주석 제거 시 Galaga씬의 Hierachy에 BezierCurve를 눌러 확인할 수 있습니다.
/*[CanEditMultipleObjects]
[CustomEditor(typeof(BezierCurve))]
public class BezierCurve_Editor : Editor
{
    private void OnSceneGUI()
    {
        BezierCurve Generator = (BezierCurve)target;

        //점들의 위치를 조정할 수 있게 함
        Generator.P1_1 = Handles.PositionHandle(Generator.P1_1, Quaternion.identity);
        Generator.P1_2 = Handles.PositionHandle(Generator.P1_2, Quaternion.identity);
        Generator.P1_3 = Handles.PositionHandle(Generator.P1_3, Quaternion.identity);


        Handles.DrawLine(Generator.P1_1, Generator.P1_2);
        Handles.DrawLine(Generator.P1_2, Generator.P1_3);

        Generator.P2_1 = Handles.PositionHandle(Generator.P2_1, Quaternion.identity);
        Generator.P2_2 = Handles.PositionHandle(Generator.P2_2, Quaternion.identity);
        Generator.P2_3 = Handles.PositionHandle(Generator.P2_3, Quaternion.identity);


        Handles.DrawLine(Generator.P2_1, Generator.P2_2);
        Handles.DrawLine(Generator.P2_2, Generator.P2_3);

        Generator.P3_1 = Handles.PositionHandle(Generator.P3_1, Quaternion.identity);
        Generator.P3_2 = Handles.PositionHandle(Generator.P3_2, Quaternion.identity);
        Generator.P3_3 = Handles.PositionHandle(Generator.P3_3, Quaternion.identity);


        Handles.DrawLine(Generator.P3_1, Generator.P3_2);
        Handles.DrawLine(Generator.P3_2, Generator.P3_3);

        Generator.P4_1 = Handles.PositionHandle(Generator.P4_1, Quaternion.identity);
        Generator.P4_2 = Handles.PositionHandle(Generator.P4_2, Quaternion.identity);
        Generator.P4_3 = Handles.PositionHandle(Generator.P4_3, Quaternion.identity);


        Handles.DrawLine(Generator.P4_1, Generator.P4_2);
        Handles.DrawLine(Generator.P4_2, Generator.P4_3);

        Generator.P5_1 = Handles.PositionHandle(Generator.P5_1, Quaternion.identity);
        Generator.P5_2 = Handles.PositionHandle(Generator.P5_2, Quaternion.identity);
        Generator.P5_3 = Handles.PositionHandle(Generator.P5_3, Quaternion.identity);


        Handles.DrawLine(Generator.P5_1, Generator.P5_2);
        Handles.DrawLine(Generator.P5_2, Generator.P5_3);

        //Detail은 선을 이을 점의 개수(많으면 많을 수록 에디터 상에 그려지는 곡선이 부드러워짐)
        int Detail = 100;
        for (float i = 0; i < Detail; i++)
        {
            //베지어 곡선을 그려주는 함수를 적용하여 현재 점과 그다음에 그려질 점을 그려 두 점을 DrawLine하면 씬에서 베지어 곡선이 그려지는걸 실시간 확인 가능
            float value_Before = i / Detail;
            Vector3 Before = Generator.Bezier(Generator.P1_1, Generator.P1_2, Generator.P1_3, value_Before);
            float value_After = (i + 1) / Detail;
            Vector3 After = Generator.Bezier(Generator.P1_1, Generator.P1_2, Generator.P1_3, value_After);
            Handles.DrawLine(Before, After);

            Before = Generator.Bezier(Generator.P2_1, Generator.P2_2, Generator.P2_3, value_Before);
            After = Generator.Bezier(Generator.P2_1, Generator.P2_2, Generator.P2_3, value_After);
            Handles.DrawLine(Before, After);

            Before = Generator.Bezier(Generator.P3_1, Generator.P3_2, Generator.P3_3, value_Before);
            After = Generator.Bezier(Generator.P3_1, Generator.P3_2, Generator.P3_3, value_After);
            Handles.DrawLine(Before, After);

            Before = Generator.Bezier(Generator.P4_1, Generator.P4_2, Generator.P4_3, value_Before);
            After = Generator.Bezier(Generator.P4_1, Generator.P4_2, Generator.P4_3, value_After);
            Handles.DrawLine(Before, After);

            Before = Generator.Bezier(Generator.P5_1, Generator.P5_2, Generator.P5_3, value_Before);
            After = Generator.Bezier(Generator.P5_1, Generator.P5_2, Generator.P5_3, value_After);
            Handles.DrawLine(Before, After);
        }
    }
}*/

