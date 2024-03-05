namespace AulasAI.Collections;

public class Graph<T> : Node<T>
{
    public Graph
    (
        T value = default(T),
        List<Node<T>> neighbours = null! 
    )
    : base (value, neighbours) {}
}