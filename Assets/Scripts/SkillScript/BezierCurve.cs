using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    //������ � ����Ʈ
    //���� ���� �� ������
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

    //������ � ����(�����ε�� �� 4���� ��, �� 6���� ���� ��������)
    public Vector3 Bezier(Vector3 p_1, Vector3 p_2, Vector3 p_3, float p_value)
    {
        //1���� 2�� ���� ��������
        Vector3 A = Vector3.Lerp(p_1, p_2, p_value);
        //2���� 3�� ���� ��������
        Vector3 B = Vector3.Lerp(p_2, p_3, p_value);

        //1���� 2�� ���� �������� �̵� ���� �� A�� 2���� 3�� ���� �������� �̵� ���� �� B�� ��������
        Vector3 C = Vector3.Lerp(A, B, p_value);
        //2���� 3�� ���� �������� �̵� ���� �� B�� 3���� 4�� ���� �������� �̵� ���� �� D�� ��������

        return C;
    }


}

//����Ƽ ������
//������ ������ ��� �׸� ������ ���� ������ �� �ְ� DrawLine���� �̵���θ� �׷��־� �̸� �� �� �ִ�.
//�ּ� ���� �� Galaga���� Hierachy�� BezierCurve�� ���� Ȯ���� �� �ֽ��ϴ�.
/*[CanEditMultipleObjects]
[CustomEditor(typeof(BezierCurve))]
public class BezierCurve_Editor : Editor
{
    private void OnSceneGUI()
    {
        BezierCurve Generator = (BezierCurve)target;

        //������ ��ġ�� ������ �� �ְ� ��
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

        //Detail�� ���� ���� ���� ����(������ ���� ���� ������ �� �׷����� ��� �ε巯����)
        int Detail = 100;
        for (float i = 0; i < Detail; i++)
        {
            //������ ��� �׷��ִ� �Լ��� �����Ͽ� ���� ���� �״����� �׷��� ���� �׷� �� ���� DrawLine�ϸ� ������ ������ ��� �׷����°� �ǽð� Ȯ�� ����
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

