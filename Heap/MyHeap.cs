using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hypergryph
{

    internal class MyPriorityQueue<TElement, TPriority>
    {
        private struct Node
        {
            public TElement element;                           //값
            public TPriority priority;                            //순위 값
        }

        private IComparer<TPriority> comparer;        //순위값을 비교할 수 있는 값으로 만듦

        private List<Node> nodes;

        public MyPriorityQueue()
        {
            this.nodes = new List<Node>();
            this.comparer = Comparer<TPriority>.Default;
        }

        public int Count { get { return nodes.Count; } }                                       //우선순위큐에 있는 값의 갯수를 알려주는 함수
        public IComparer<TPriority> Comparer { get { return comparer; } }        //순위값들의 비교 결과를 알려주는 함수

        public TElement Peek()                                 //우선순위큐의 맨윗값을 알려주는 함수
        {
            if (nodes.Count == 0)
                throw new InvalidOperationException();

            return nodes[0].element;                         //맨윗값 호출
        }

        public void Enqueue(TElement element, TPriority priority)                  //우선순위큐에 값을 추가하는 함수
        {
            Node newNode = new Node() { element = element, priority = priority };

            nodes.Add(newNode);                                                                     //받은 값으로 새로운 노드 생성
            int newNodeIndex = nodes.Count - 1;                                              //노드가 늘어났기 때문에 늘어난 카운트에 하나를 뺀 값을 자릿값으로 줌

            while (newNodeIndex > 0)                                                                //새로운 값이 들어가기 전 큐가 비어있지 않다면
            {
                int parentIndex = GetParentIndex(newNodeIndex);                   //새로 들어온 값으로 부모의 자릿값을 계산
                Node parentNode = nodes[parentIndex];                                    //부모의 값을 불러옴
                if (comparer.Compare(newNode.priority, parentNode.priority) < 0)    // 부모의 값과 새로 들어온 값을 비교해서 새로 들어온 값이 크다면
                {
                    nodes[newNodeIndex] = parentNode;                                     //부모와 새로 들어온 값의 자리를 바꾼다.
                    newNodeIndex = parentIndex;
                }
                else                                                                                             //부모의 우선순위가 더 높다면 
                {
                    break;                                                                                     //바꾸지 않고 끝낸다.
                }
            }
            nodes[newNodeIndex] = newNode;                                                 //새로 들어온 값을 알맞는 자리에 놓는다.
        }

        public void Dequeue()                                             //우선순위큐의 가장 우선순위 값을 지우는 함수
        {
            Node lastNode = nodes[nodes.Count - 1];           //우선순위가 가장 낮은 값을 따로 저장                        
            nodes.RemoveAt(nodes.Count - 1);                     //우선순위가 가장 낮은 값을 삭제

            int index = 0;
            while (index < nodes.Count)                                                    //재정렬이 다될때 까지 반복
            {
                int leftChildIndex = GetLeftChildIndex(index);                //왼쪽값의 인덱스값 저장
                int rightChildIndex = GetRightChildIndex(index);             //오른쪽의 인덱스값 저장
                if (rightChildIndex < nodes.Count)                                     //오른쪽에 값이 있을 경우
                {
                    int lessChildIndex = comparer.Compare(nodes[leftChildIndex].priority, nodes[rightChildIndex].priority) < 0 ?
                    leftChildIndex : rightChildIndex;                                  //왼쪽 값과 오른쪽 값의 우선순위 비교해서 둘중에 더 큰 값 lessChildIndex로저장
                    if (comparer.Compare(nodes[lessChildIndex].priority, lastNode.priority) < 0)           //lessChildIndex와 가장 작은 값 비교
                    {
                        nodes[index] = nodes[lessChildIndex];                                                                   //lessChildIndex값의 우선순위가 클 경우 둘의 자리 바꿈
                        index = lessChildIndex;
                    }
                    else
                    {
                        nodes[index] = lastNode;                                                                                        //lessChildIndex값의 우선순위가 작을 경우 그대로 둠
                        break;
                    }
                }
                else if (leftChildIndex < nodes.Count)                                                                           //왼쪽 값만 있을 경우
                {
                    if (comparer.Compare(nodes[leftChildIndex].priority, lastNode.priority) < 0)          //왼쪽 값과 마지막 값의 우선순위 비교
                    {
                        nodes[index] = nodes[leftChildIndex];                      //왼쪽 값의 우선순위가 더 클 경우 서로 자리 바꿈
                        nodes[leftChildIndex] = lastNode;
                        index = leftChildIndex;
                    }
                    else
                    {
                        nodes[index] = lastNode;                                          //마지막 값의 우선순위가 더 클 경우 그대로 끝냄.
                        break;
                    }
                }
                else
                {
                    nodes[index] = lastNode;                                              //child값이 없는 경우 종료
                    break;
                }
            }
        }
        private int GetParentIndex(int childIndex)                            //부모값의 자릿값 찾는 함수
        {
            return (childIndex - 1) / 2;
        }
        private int GetLeftChildIndex(int parentIndex)                    //child중 왼쪽값의 자릿값 찾는 함수
        {
            return parentIndex * 2 + 1;
        }

        private int GetRightChildIndex(int parentIndex)                    //child중 오른쪽값의 자릿값 찾는 함수
        {
            return parentIndex * 2 + 2;
        }
    }
}
/*힙 : 힙은 완전 이진 트리 구조를 가진 구조입니다.
 * 이 힙의 특성을 이용하여 정렬하는 것이 힙정렬입니다.
 * 최소값 혹은 최대값을 빠르게 가져오기 위해 고안되었습니다.
 * 형제노드 사이에서는 아무런 대소 관계가 정해져 있지 않습니다.
 * 
 * 추가를 할 경우 대상을 좌측 최하단의 자식노드를 기점으로 위의 부모노드와 비교연산하여
 * 조건이 만족한다면 부모노드와 자리를 바꾸며 자신의 자리를 탐색 후 삽입합니다.
 * 
 * 삭제를 진행 할 경우 삭제대상의 자식노드의 마지막 레벨값을 지운위치로 올린 후
 * 자식노드들과 비교하여 위에서 아래로 순서를 바꾸며 힙정렬의 규칙이 맞아 떨어질
 * 때 까지 재정렬을 시행합니다.
 * 
 * 완전이진트리는 이진트리 구조로 되어있는 자식 노드를 제외한 모든 부모 노드가
 * 자식 노드를 2개씩 가지고 있는 것을 완전이진트리구조라고 합니다.*/