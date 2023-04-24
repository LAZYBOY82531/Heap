namespace Heap
{
    internal class Program
    {
        /******************************************************
		 * 힙 (Heap)
		 * 
		 * 부모 노드가 항상 자식노드보다 우선순위가 높은 속성을 만족하는 트리기반의 자료구조
		 * 많은 자료 중 우선순위가 가장 높은 요소를 빠르게 가져오기 위해 사용
		 ******************************************************/

        static void PriorityQueue()
        {
            PriorityQueue<string, int> acsendingpq = new PriorityQueue<string, int>();

            acsendingpq.Enqueue("감자", 3);
            acsendingpq.Enqueue("양파", 5);
            acsendingpq.Enqueue("당근", 1);
            acsendingpq.Enqueue("토마토", 2);
            acsendingpq.Enqueue("마늘", 4);

            while (acsendingpq.Count > 0)
            {
                Console.WriteLine(acsendingpq.Dequeue());  //우선순위가 높은 순서대로 데이터 출력
            }
            PriorityQueue<string, int> desendingpq 
                = new PriorityQueue<string, int>(Comparer<int>.Create((a,b) => b-a));

            desendingpq.Enqueue("왼쪽", 70);
            desendingpq.Enqueue("위쪽", 100);
            desendingpq.Enqueue("오른쪽", 10);
            desendingpq.Enqueue("아래쪽", 20);

            string nextDir = desendingpq.Dequeue();
            Console.WriteLine(nextDir);
            desendingpq.Clear();
        }

        //시간복잡도
        //탐색(가장우선순위높은)        추가           삭제
        //               O(1)                   O(logN)      O(logN)
        static void Main(string[] args)
        {
            PriorityQueue();
        }
    }
}